using GarageKept.OutlookAlarm.Alarm.AlarmManager;
using GarageKept.OutlookAlarm.Alarm.Audio;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

public partial class AlarmForm : BaseForm, IAlarmForm
{
    private readonly Media _mediaPlayer = new();
    private readonly Timer _refreshTimer = new() { Interval = 1000 };
    private AlarmAction _alarmAction = AlarmAction.Dismiss;

    public AlarmForm(IAlarm alarm, Action<AlarmAction, string> alarmFormCallback)
    {
        InitializeComponent();

        ShowInTaskbar = false;

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

        if (alarm.IsAudible)
            if (alarm.HasCustomSound)
                _mediaPlayer.PlaySound(alarm.CustomSound);
        //else
        //    _mediaPlayer.PlaySound(Program.ApplicationSettings.DefaultSound);
        MyCallBack = alarmFormCallback;

        Alarm = alarm;

        SubjectLabel.Text = alarm.Name;
        TimeRight.Text = alarm.ReminderTime.ToString("hh:mm");
//        TimeLeft.Text = string.Format(Program.ApplicationSettings.TimeLeftStringFormat, DateTime.Now - Alarm.Start);

        UpdateDropdown();

        _refreshTimer.Tick += FormRefresh;
        _refreshTimer.Start();

        // Subscribe to MouseEnter and MouseLeave events for each child control
        SetDraggable(this);
    }

    public IAlarm Alarm { get; set; }

    public Action<AlarmAction, string> MyCallBack { get; set; }

    private void UpdateDropdown()
    {
        if (DateTime.Now - Alarm.Start > TimeSpan.FromMinutes(5))
            ActionSelector.Items.Remove(AlarmAction.FiveMinBefore);

        if (DateTime.Now - Alarm.Start > TimeSpan.FromMinutes(0))
            ActionSelector.Items.Remove(AlarmAction.ZeroMinBefore);

        if (ActionSelector.SelectedIndex <= 0) ActionSelector.SelectedIndex = 0;
    }

    private void FormRefresh(object? sender, EventArgs e)
    {
//        TimeLeft.Text = string.Format(Program.ApplicationSettings.TimeLeftStringFormat, DateTime.Now - Alarm.Start);

        UpdateDropdown();
    }

    private void AlarmWindowForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        MyCallBack(_alarmAction, Alarm.Id);
    }

    private void SnoozeButton_Click(object sender, EventArgs e)
    {
        _alarmAction = (AlarmAction)(ActionSelector.SelectedValue ?? AlarmAction.FiveMinBefore);

        Close();
    }

    private void DismissButton_Click(object sender, EventArgs e)
    {
        _alarmAction = AlarmAction.Dismiss;

        Close();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _mediaPlayer.Stop();

        base.OnFormClosing(e);
    }
}