using System.Collections;
using GarageKept.OutlookAlarm.Alarm.AlarmManager;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

public partial class AlarmForm : BaseForm, IAlarmForm
{
    private readonly IMediaPlayer _mediaPlayerPlayer;
    private bool _playSound = true;
    private Timer? _refreshTimer;

    public AlarmForm(IMediaPlayer mediaPlayer) : base(false)
    {
        InitializeComponent();

        _mediaPlayerPlayer = mediaPlayer;

        ShowInTaskbar = true;

        ActionSelector.Items.Clear();
        var dataSource = Enum.GetValues(typeof(AlarmAction)).Cast<AlarmAction>().Select(s => new { Value = s, Text = AlarmActionHelpers.GetEnumDisplayValue(s) }).Where(a => a.Text != "Dismissed").ToList();
        ActionSelector.DisplayMember = "Text";
        ActionSelector.ValueMember = "Value";

        ActionSelector.DataSource = dataSource;

        Move += (_, _) =>
        {
            Program.AppSettings.Alarm.Left = Left;
            Program.AppSettings.Alarm.Top = Top;
        };
    }

    private IAlarm? Alarm { get; set; }

    public void Show(IAlarm alarm)
    {
        Alarm = alarm ?? throw new ArgumentNullException(typeof(IAlarm).ToString());

        var tooLAteForAudio = DateTime.Now - Alarm.Start > TimeSpan.FromMinutes(Program.AppSettings.Audio.TurnOffAlarmAfterStart);

        if (Alarm.IsAudible && !tooLAteForAudio)
        {
            if (Alarm.HasCustomSound)
                _mediaPlayerPlayer.PlaySound(Alarm.CustomSound, true);
            else
                _mediaPlayerPlayer.PlaySound(Program.AppSettings.Audio.DefaultSound, true);
        }

        SubjectLabel.Text = Alarm.Name;
        TimeRight.Text = Alarm.ReminderTime.ToString(Program.AppSettings.Alarm.AlarmStartStringFormat);
        TimeLeft.Text = string.Format(Program.AppSettings.Alarm.TimeLeftStringFormat, DateTime.Now - Alarm.Start);

        FormRefresh(null, null);

        UpdateDropdown();

        _refreshTimer = new Timer
        {
            Interval = 1000 // 1000 ms = 1 second
        };

        _refreshTimer.Tick += FormRefresh;
        _refreshTimer.Start();

        // Subscribe to MouseEnter and MouseLeave events for each child control
        SetDraggable(this);

        Location = new Point(Program.AppSettings.Alarm.Left, Program.AppSettings.Alarm.Top);

        SetBackGroundColor(Alarm.AlarmColor);

        if (!IsDisposed)
            base.Show();
    }

    public new void Close() { Dispose(); }

    private void DismissButton_Click(object sender, EventArgs e)
    {
        if (Alarm == null) return;

        Program.AlarmManager.ChangeAlarmState(Alarm, AlarmAction.Dismiss);

        Close();
    }

    public new void Dispose()
    {
        _refreshTimer?.Stop();
        _refreshTimer?.Dispose();
        _refreshTimer = null;
        _mediaPlayerPlayer.StopSound();

        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void FormRefresh(object? sender, EventArgs? e)
    {
        TimeLeft.Text = string.Format(Program.AppSettings.Alarm.TimeLeftStringFormat, DateTime.Now - Alarm?.Start);

        if (_playSound && Program.AppSettings.Audio.TurnOffAlarmAfterStart >= 0 && DateTime.Now - Alarm?.Start > TimeSpan.FromMinutes(Program.AppSettings.Audio.TurnOffAlarmAfterStart))
        {
            _mediaPlayerPlayer.StopSound();
            _playSound = false;
        }

        if (DateTime.Now > Alarm?.End)
        {
            Program.AlarmManager.ChangeAlarmState(Alarm, AlarmAction.Dismiss);
            Close();
        }

        if (DateTime.Now > Alarm?.Start)
            SetBackGroundColor(Program.AppSettings.Color.AlarmPastStartColor);

        UpdateDropdown();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _mediaPlayerPlayer.StopSound();

        base.OnFormClosing(e);
    }

    private void RemoveActionSelectorItem(AlarmAction action)
    {
        // Retrieve the current data source as a list of anonymous objects
        var dataSource = ((IEnumerable)ActionSelector.DataSource).Cast<dynamic>().Select(item => new { Value = (AlarmAction)item.Value, item.Text }).ToList();

        if (dataSource.All(i => i.Value != action)) return;

        dataSource = dataSource.Where(i => i.Value != action).ToList();

        // Set the modified data source back to the ActionSelector ComboBox
        ActionSelector.DataSource = null; // Reset the data source
        ActionSelector.DataSource = dataSource.ToList(); // Set the updated data source
        ActionSelector.DisplayMember = "Text";
        ActionSelector.ValueMember = "Value";
    }

    private void SetBackGroundColor(Color backGroundColor)
    {
        BackColor = DateTime.Now > Alarm?.Start ? Program.AppSettings.Color.AlarmPastStartColor : backGroundColor;
        ForeColor = DetermineTextColor(backGroundColor);

        // Exclude buttons from text color adjustment
        foreach (Control control in Controls)
            if (control is Button)
                control.ForeColor = SystemColors.ControlText;
    }

    private void SnoozeButton_Click(object sender, EventArgs e)
    {
        if (Alarm == null) return;

        var selectedAction = (AlarmAction)(ActionSelector.SelectedValue ?? AlarmAction.FiveMinBefore);

        Program.AlarmManager.ChangeAlarmState(Alarm, selectedAction);

        Close();
    }

    private void UpdateDropdown()
    {
        if ((Alarm?.Start - DateTime.Now)!.Value.TotalMinutes < TimeSpan.FromMinutes(5).TotalMinutes)
            RemoveActionSelectorItem(AlarmAction.FiveMinBefore);

        if (DateTime.Now > Alarm?.Start)
            RemoveActionSelectorItem(AlarmAction.ZeroMinBefore);

        //if (ActionSelector.SelectedIndex <= 0) ActionSelector.SelectedIndex = 1;
    }
}