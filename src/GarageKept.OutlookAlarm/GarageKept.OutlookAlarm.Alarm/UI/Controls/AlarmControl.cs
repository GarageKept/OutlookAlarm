using System.Diagnostics;
using GarageKept.OutlookAlarm.Alarm.AlarmManager;
using GarageKept.OutlookAlarm.Alarm.Interfaces;

namespace GarageKept.OutlookAlarm.Alarm.UI.Controls;

public partial class AlarmControl : UserControl, IAlarmControl
{
    private IAlarm? _alarm;

    public AlarmControl(IAlarmManager alarmManager, ISettings settings)
    {
        AlarmManager = alarmManager;
        Settings = settings;

        InitializeComponent();
    }

    private IAlarmManager AlarmManager { get; }
    private ISettings Settings { get; }

    public IAlarm? Alarm
    {
        get => _alarm;
        set
        {
            if (value is null)
                RefreshTimer.Enabled = false;
            else
                RefreshTimer.Enabled = true;

            _alarm = value;

            if (InvokeRequired)
                Invoke(UpdateDisplay);
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Draw the bottom border
        using Pen borderPen = new(Color.Black);
        e.Graphics.DrawLine(borderPen, 0, Height - 1, Width, Height - 1);
    }

    private void OnTeamsLinkLabelOnLinkClicked(object? sender, LinkLabelLinkClickedEventArgs linkLabelLinkClickedEventArgs)
    {
        if (Alarm is null) return;

        Process.Start(new ProcessStartInfo
        {
            FileName = Alarm?.TeamsMeetingUrl, //"msteams://meetingjoin?url=" + 
            UseShellExecute = true
        });
    }

    private void Refresh_Tick(object? sender, EventArgs e)
    {
        if (Alarm is null) return;

        if (Alarm.End < DateTime.Now)
            AlarmManager.ChangeAlarmState(Alarm, AlarmAction.Dismiss);

        UpdateDisplay();
    }

    private void RightClickMenuDismiss_Click(object sender, EventArgs e)
    {
        if (Alarm is null) return;

        AlarmManager.ChangeAlarmState(Alarm, AlarmAction.Dismiss);
    }

    private void RightClickMenuRemove_Click(object sender, EventArgs e)
    {
        if (Alarm is null) return;

        AlarmManager.ChangeAlarmState(Alarm, AlarmAction.Remove);
    }

    private void SetBackgroundColor()
    {
        if (Alarm is null) return;

        BackColor = Alarm.AlarmColor;
    }

    private void SetHeaderLabels()
    {
        if (Alarm is null) return;

        SubjectLabel.Text = Alarm.Name;
        OrganizerLabel.Text = Alarm.Organizer;
        CategoryLabel.Text = string.Join(", ", Alarm.Categories);

        if (string.IsNullOrWhiteSpace(Alarm.TeamsMeetingUrl)) return;

        TeamsLinkLabel.Visible = true;
    }

    private void SetProgressBar()
    {
        if (Alarm is null) return;

        if (Alarm.Start < DateTime.Now && Alarm.End > DateTime.Now)
            SetProgressBarForAppointmentInProgress();
        else if (Alarm.Start > DateTime.Now) SetProgressBarForFutureAppointment();
    }

    private void SetProgressBarForAppointmentInProgress()
    {
        if (Alarm is null) return;

        var timeLeft = Alarm.End - DateTime.Now;

        var progress = (int)((1 - timeLeft.TotalMinutes / (Alarm.End - Alarm.Start).Minutes) * 100);


        progressBar.Value = progress < 0 ? 0 : progress;
    }

    private void SetProgressBarForFutureAppointment()
    {
        if (Alarm is null) return;

        var timeUntilMeeting = Alarm.Start - DateTime.Now;

        if (timeUntilMeeting.TotalMinutes < 60)
        {
            var progress = (int)((1 - timeUntilMeeting.TotalMinutes / 60) * 100);
            progressBar.Value = progress;
        }
        else
        {
            progressBar.Value = 0;
        }
    }

    private void SetTimeLabels()
    {
        if (Alarm is null) return;

        if (Alarm.Start < DateTime.Now && Alarm.End > DateTime.Now)
        {
            SetTimeLeftInAppointment();
            SetTimeRightLabel(Alarm.End.ToString("hh:mm tt"));
        }
        else if (Alarm.Start > DateTime.Now)
        {
            SetTimeUntilMeetingLabel();
            SetTimeRightLabel(Alarm.Start.ToString("h:mm tt"));
        }
    }

    private void SetTimeLeftInAppointment()
    {
        if (Alarm is null) return;

        var timeLeft = Alarm.End - DateTime.Now;

        if (timeLeft.TotalMinutes < 0) timeLeft = TimeSpan.Zero;

        TimeLeft.Text = string.Format(Settings.Alarm.TimeLeftStringFormat, timeLeft);
    }

    private void SetTimeRightLabel(string text) { TimeRight.Text = text; }

    private void SetTimeUntilMeetingLabel()
    {
        if (Alarm is null) return;

        var timeUntilMeeting = Alarm.Start - DateTime.Now;

        var formatString = timeUntilMeeting.Hours > 0 ? "{0:hh}h {0:mm}m {0:ss}s" : "{0:mm}m {0:ss}s";

        TimeLeft.Text = string.Format(formatString, timeUntilMeeting);
    }

    public void UpdateDisplay()
    {
        Visible = Alarm is not null;

        if (Alarm == null) return;

        SetHeaderLabels();
        SetTimeLabels();
        SetProgressBar();
        SetBackgroundColor();
    }

    private void RightClick_15Min_Click(object sender, EventArgs e)
    {
        AlarmManager.ChangeAlarmState(Alarm!, AlarmAction.FifteenMinBefore);
    }

    private void RightClick_10Min_Click(object sender, EventArgs e)
    {

        AlarmManager.ChangeAlarmState(Alarm!, AlarmAction.TenMinBefore);
    }

    private void RightClick_5Min_Click(object sender, EventArgs e)
    {

        AlarmManager.ChangeAlarmState(Alarm!, AlarmAction.FiveMinBefore);
    }

    private void RightClick_0Min_Click(object sender, EventArgs e)
    {
        
        AlarmManager.ChangeAlarmState(Alarm!, AlarmAction.ZeroMinBefore);
    }
}