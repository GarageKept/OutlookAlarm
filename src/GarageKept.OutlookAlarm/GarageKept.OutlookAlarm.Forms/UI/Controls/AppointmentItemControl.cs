using Microsoft.Office.Interop.Outlook;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Forms.UI.Controls;

public partial class AppointmentItemControl : UserControl
{
    public AppointmentItemControl(AppointmentItem appointment)
    {
        RefreshTimer.Tick += Refresh_Tick;
        RefreshTimer.Start();

        InitializeComponent();

        Appointment = appointment;

        UpdateDisplay();
    }

    public Timer RefreshTimer { get; set; } = new() { Interval = 1000 };

    private AppointmentItem Appointment { get; }

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

    private void SetTimeLeftInAppointment()
    {
        var timeLeft = Appointment.End - DateTime.Now;
        if (timeLeft.TotalMinutes < 0) timeLeft = TimeSpan.Zero;

        TimeLeft.Text = string.Format(Program.ApplicationSettings.TimeLeftStringFormat, timeLeft);
    }

    private void SetTimeUntilMeetingLabel()
    {
        var timeUntilMeeting = Appointment.Start - DateTime.Now;

        TimeLeft.Text = (timeUntilMeeting.Hours > 0 ? timeUntilMeeting.Hours + ":" : "")
                        + timeUntilMeeting.Minutes + ":"
                        + timeUntilMeeting.Seconds;
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
            BackColor = Program.ApplicationSettings?.YellowColor ?? Color.Yellow;
    }

    private void SetBackgroundColorForFutureAppointment()
    {
        BackColor = Color.Green;

        var timeUntilMeeting = Appointment.Start - DateTime.Now;

        if (timeUntilMeeting.TotalMinutes < 15) BackColor = Program.ApplicationSettings?.YellowColor ?? Color.Yellow;
    }

    // Linearly interpolate between two colors based on a percentage
    //private static Color InterpolateColors(Color color1, Color color2, float percent)
    //{
    //    var r = (int)(color1.R + (color2.R - color1.R) * percent);
    //    var g = (int)(color1.G + (color2.G - color1.G) * percent);
    //    var b = (int)(color1.B + (color2.B - color1.B) * percent);
    //    return Color.FromArgb(r, g, b);
    //}

    private void AppointmentItemControl_Load(object sender, EventArgs e)
    {
    }
}