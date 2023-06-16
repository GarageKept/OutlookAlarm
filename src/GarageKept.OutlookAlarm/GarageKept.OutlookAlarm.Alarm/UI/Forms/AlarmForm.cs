using GarageKept.OutlookAlarm.Alarm.AlarmManager;
using GarageKept.OutlookAlarm.Alarm.Audio;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

public partial class AlarmForm : BaseForm, IAlarmForm
{
    private readonly Media _mediaPlayer = new();
    private Timer? _refreshTimer;

    public AlarmForm() : base(true)
    {
        InitializeComponent();

        ShowInTaskbar = true;

        ActionSelector.Items.Clear();
        ActionSelector.DataSource = Enum.GetValues(typeof(AlarmAction))
            .Cast<AlarmAction>()
            .Select(s => new
            {
                Value = s,
                Text = AlarmActionHelpers.GetEnumDisplayValue(s)
            }).Where(a => a.Text != "Dismissed")
            .ToList();
        ActionSelector.DisplayMember = "Text";
        ActionSelector.ValueMember = "Value";
    }

    private IAlarm? Alarm { get; set; }

    private void UpdateDropdown()
    {
        if (DateTime.Now - Alarm?.Start > TimeSpan.FromMinutes(5))
            ActionSelector.Items.Remove(AlarmAction.FiveMinBefore);

        if (DateTime.Now - Alarm?.Start > TimeSpan.FromMinutes(0))
            ActionSelector.Items.Remove(AlarmAction.ZeroMinBefore);

        if (ActionSelector.SelectedIndex <= 0) ActionSelector.SelectedIndex = 0;
    }

    private void FormRefresh(object? sender, EventArgs? e)
    {
        TimeLeft.Text = string.Format(Program.AppSettings.TimeLeftStringFormat, DateTime.Now - Alarm?.Start);

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

    public void Show(IAlarm alarm)
    {
        Alarm = alarm ?? throw new ArgumentNullException(typeof(IAlarm).ToString());

        if (Alarm.IsAudible)
            if (Alarm.HasCustomSound)
                _mediaPlayer.PlaySound(Alarm.CustomSound);
            else
                _mediaPlayer.PlaySound(Program.AppSettings.DefaultSound);

        SubjectLabel.Text = Alarm.Name;
        TimeRight.Text = Alarm.ReminderTime.ToString("hh:mm");
        TimeLeft.Text = string.Format(Program.AppSettings.TimeLeftStringFormat, DateTime.Now - Alarm.Start);

        UpdateDropdown();

        _refreshTimer = new Timer
        {
            Interval = 1000 // 1000 ms = 1 second
        };

        _refreshTimer.Tick += FormRefresh;
        _refreshTimer.Start();

        // Subscribe to MouseEnter and MouseLeave events for each child control
        SetDraggable(this);

        this.Location = new Point(0, 0);

        base.Show();
    }
}