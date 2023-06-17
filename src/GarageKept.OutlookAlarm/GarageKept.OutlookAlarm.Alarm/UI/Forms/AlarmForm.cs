using System.Collections;
using GarageKept.OutlookAlarm.Alarm.AlarmManager;
using GarageKept.OutlookAlarm.Alarm.Audio;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

public partial class AlarmForm : BaseForm, IAlarmForm
{
    private readonly Media _mediaPlayer = new();
    private Timer? _refreshTimer;
    private bool _playSound = true;

    public AlarmForm() : base(false)
    {
        InitializeComponent();

        ShowInTaskbar = true;

        ActionSelector.Items.Clear();
        var dataSource = Enum.GetValues(typeof(AlarmAction))
            .Cast<AlarmAction>()
            .Select(s => new
            {
                Value = s,
                Text = AlarmActionHelpers.GetEnumDisplayValue(s)
            }).Where(a => a.Text != "Dismissed")
            .ToList();
        ActionSelector.DisplayMember = "Text";
        ActionSelector.ValueMember = "Value";

        ActionSelector.DataSource = dataSource;
        
        Move += (_, args) =>
        {
            Program.AppSettings.Alarm.Left = Left;
            Program.AppSettings.Alarm.Top = Top;
            Program.AppSettings.Save();
        };
    }

    private IAlarm? Alarm { get; set; }

    private void UpdateDropdown()
    {
        if ((Alarm?.Start -DateTime.Now)!.Value.TotalMinutes < TimeSpan.FromMinutes(5).TotalMinutes)
            RemoveActionSelectorItem(AlarmAction.FiveMinBefore);

        if (DateTime.Now > Alarm?.Start)
            RemoveActionSelectorItem(AlarmAction.ZeroMinBefore);

        //if (ActionSelector.SelectedIndex <= 0) ActionSelector.SelectedIndex = 1;
    }

    private void RemoveActionSelectorItem(AlarmAction action)
    {
        // Retrieve the current data source as a list of anonymous objects
        var dataSource = ((IEnumerable)ActionSelector.DataSource).Cast<dynamic>()
            .Select(item => new
            {
                Value = (AlarmAction)item.Value, item.Text
            })
            .ToList();

        if (dataSource.All(i => i.Value != action)) return;

        dataSource = dataSource.Where(i => i.Value != action).ToList();

        // Set the modified data source back to the ActionSelector ComboBox
        ActionSelector.DataSource = null; // Reset the data source
        ActionSelector.DataSource = dataSource.ToList(); // Set the updated data source
        ActionSelector.DisplayMember = "Text";
        ActionSelector.ValueMember = "Value";
    }

    private void FormRefresh(object? sender, EventArgs? e)
    {
        TimeLeft.Text = string.Format(Program.AppSettings.Alarm.TimeLeftStringFormat, DateTime.Now - Alarm?.Start);

        if (_playSound && Program.AppSettings.Audio.TurnOffAlarmAfterStart >= 0 && Alarm?.Start - DateTime.Now > TimeSpan.FromMinutes(Program.AppSettings.Audio.TurnOffAlarmAfterStart))
        {
            _mediaPlayer.Stop();
            _playSound = false;
        }

        if(DateTime.Now > Alarm?.Start)
            SetBackGroundColor(Program.AppSettings.Color.AlarmPastStartColor);

        UpdateDropdown();
    }

    private void SnoozeButton_Click(object sender, EventArgs e)
    {
        if (Alarm == null) return;

        var selectedAction = (AlarmAction)(ActionSelector.SelectedValue ?? AlarmAction.FiveMinBefore);

        Program.AlarmManager.ChangeAlarmState(Alarm, selectedAction);

        Close();
    }

    private void DismissButton_Click(object sender, EventArgs e)
    {
        if (Alarm == null) return;

        Program.AlarmManager.ChangeAlarmState(Alarm, AlarmAction.Dismiss);

        Close();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _mediaPlayer.Stop();

        base.OnFormClosing(e);
    }

    private void SetBackGroundColor(Color backGroundColor)
    {
        BackColor = backGroundColor;
        ForeColor = DetermineTextColor(backGroundColor);

        // Exclude buttons from text color adjustment
        foreach (Control control in this.Controls)
        {
            if (control is Button)
            {
                control.ForeColor = SystemColors.ControlText;
            }
        }
    }

    public void Show(IAlarm alarm)
    {
        Alarm = alarm ?? throw new ArgumentNullException(typeof(IAlarm).ToString());

        if (Alarm.IsAudible)
            if (Alarm.HasCustomSound)
                _mediaPlayer.PlaySound(Alarm.CustomSound);
            else
                _mediaPlayer.PlaySound(Program.AppSettings.Audio.DefaultSound);

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

        this.Location = new Point(Program.AppSettings.Alarm.Left, Program.AppSettings.Alarm.Top);

        SetBackGroundColor(Alarm.AlarmColor);

        base.Show();
    }
}