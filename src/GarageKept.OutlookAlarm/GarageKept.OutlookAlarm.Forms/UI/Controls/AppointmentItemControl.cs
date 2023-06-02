using GarageKept.OutlookAlarm.Forms.Outlook;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Forms.UI.Controls;

public partial class AppointmentItemControl : UserControl
{
    public AppointmentItemControl(Appointment appointment)
    {
        RefreshTimer.Tick += Refresh_Tick;
        RefreshTimer.Start();

        InitializeComponent();

        Appointment = appointment;

        UpdateDisplay();
    }

    public Timer RefreshTimer { get; set; } = new() { Interval = 1000 };

    private Appointment Appointment { get; }

    private void Refresh_Tick(object? sender, EventArgs e)
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        SetSubjectLabel();
        SetTimeLabels();
        SetProgressBar();
        SetBackgroundColor();
    }

    private void SetSubjectLabel()
    {
        SubjectLabel.Text = Appointment.Subject;
    }

    private void SetTimeLabels()
    {
        try
        {
            if (Appointment.Start < DateTime.Now && Appointment.End > DateTime.Now)
            {
                SetTimeLeftInAppointment();
                SetTimeRightLabel(Appointment.End.ToString("hh:mm tt"));
            }
            else if (Appointment.Start > DateTime.Now)
            {
                SetTimeUntilMeetingLabel();
                SetTimeRightLabel(Appointment.Start.ToString("h:mm tt"));
            }
        }
        catch
        {
            // Outlook probably restarted.
        }
    }

    private void SetTimeLeftInAppointment()
    {
        var timeLeft = Appointment.End - DateTime.Now;
        if (timeLeft.TotalMinutes < 0) timeLeft = TimeSpan.Zero;

        TimeLeft.Text = string.Format(Program.ApplicationSettings.TimeLeftStringFormat, timeLeft);
    }

    private void SetTimeUntilMeetingLabel()
    {
        var timeUntilMeeting = Appointment.Start - DateTime.Now;

        var formatString = timeUntilMeeting.Hours > 0 ? "{0:hh}h {0:mm}m {0:ss}s" : "{0:mm}m {0:ss}s";

        TimeLeft.Text = string.Format(formatString, timeUntilMeeting);
    }

    private void SetTimeRightLabel(string text)
    {
        TimeRight.Text = text;
    }

    private void SetProgressBar()
    {
        if (Appointment.Start < DateTime.Now && Appointment.End > DateTime.Now)
            SetProgressBarForAppointmentInProgress();
        else if (Appointment.Start > DateTime.Now) SetProgressBarForFutureAppointment();
    }

    private void SetProgressBarForAppointmentInProgress()
    {
        var timeLeft = Appointment.End - DateTime.Now;

        var progress = (int)((1 - timeLeft.TotalMinutes / Appointment.Duration) * 100);
        progressBar.Value = progress;
    }

    private void SetProgressBarForFutureAppointment()
    {
        var timeUntilMeeting = Appointment.Start - DateTime.Now;

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

    private void SetBackgroundColor()
    {
        if (Appointment.Start < DateTime.Now && Appointment.End > DateTime.Now)
            SetBackgroundColorForAppointmentInProgress();
        else if (Appointment.Start > DateTime.Now) SetBackgroundColorForFutureAppointment();
        else
            BackColor = Color.Green;
    }

    private void SetBackgroundColorForAppointmentInProgress()
    {
        BackColor = Color.Red;

        var timeLeft = Appointment.End - DateTime.Now;
        if (
            timeLeft.TotalMinutes < 15)
            BackColor = Program.ApplicationSettings.YellowColor;
    }

    private void SetBackgroundColorForFutureAppointment()
    {
        BackColor = Color.Green;

        var timeUntilMeeting = Appointment.Start - DateTime.Now;

        if (timeUntilMeeting.TotalMinutes < 15) BackColor = Program.ApplicationSettings.YellowColor;
    }
}