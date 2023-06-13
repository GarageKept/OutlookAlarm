using GarageKept.OutlookAlarm.Forms.Outlook;
using GarageKept.OutlookAlarm.Forms.Common;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Forms.UI.Controls;

public partial class AppointmentItemControl : UserControl
{
    public AppointmentItemControl(Appointment? appointment)
    {
        InitializeComponent();

        if (DesignMode) return;

        if (appointment == null) return;

        RefreshTimer.Tick += Refresh_Tick;
        RefreshTimer.Start();

        Appointment = appointment;
        
        AddContextMenu();

        UpdateDisplay();
    }

    public Timer RefreshTimer { get; set; } = new() { Interval = 1000 };

    private Appointment? Appointment { get; }

    private readonly ContextMenuStrip _appointmentContextMenuStrip = new();

    private void AddContextMenu()
    {
        _appointmentContextMenuStrip.Items.Clear();

        var remove = new ToolStripMenuItem("Remove");
        remove.Click += RemoveAppointment;

        var dismiss = new ToolStripMenuItem("Dismiss");
        dismiss.Click += DismissAppointment;

        _appointmentContextMenuStrip.Items.Add(remove);
        _appointmentContextMenuStrip.Items.Add(dismiss);
        _appointmentContextMenuStrip.Items.Add(new ToolStripSeparator());

        ContextMenuStrip = _appointmentContextMenuStrip;

        MouseDown += Control_MouseDown;
    }

    private static void DismissAppointment(object? sender, EventArgs e)
    {
        var control = ((sender as ToolStripMenuItem)?.Owner as ContextMenuStrip)?.SourceControl;

        var appointment = (control as AppointmentItemControl)?.Appointment;
        
        if (appointment?.Id != null) AlarmManager.DismissAlarm(appointment.Id);
    }

    private static void RemoveAppointment(object? sender, EventArgs e)
    {
        var control = ((sender as ToolStripMenuItem)?.Owner as ContextMenuStrip)?.SourceControl;

        var appointment = (control as AppointmentItemControl)?.Appointment;

        AppointmentManager.Remove(appointment);
        if (appointment?.Id != null) AlarmManager.RemoveAlarm(appointment.Id);
    }

    private void Control_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            _appointmentContextMenuStrip.Show(this, e.Location);
        }
    }

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
        if (Appointment == null) return;

        SubjectLabel.Text = Appointment.Subject;
    }

    private void SetTimeLabels()
    {
        if (Appointment == null) return;

        if (Appointment.Start < DateTime.Now && Appointment.End > DateTime.Now)
        {
            SetTimeLeftInAppointment();
            SetTimeRightLabel(Appointment.End.ToString("hh:mm tt"));
        }
        else if (Appointment?.Start > DateTime.Now)
        {
            SetTimeUntilMeetingLabel();
            SetTimeRightLabel(Appointment.Start.ToString("h:mm tt"));
        }
    }

    private void SetTimeLeftInAppointment()
    {
        if (Appointment == null) return;

        var timeLeft = Appointment.End - DateTime.Now;
        if (timeLeft.TotalMinutes < 0) timeLeft = TimeSpan.Zero;

        TimeLeft.Text = string.Format(Program.ApplicationSettings.TimeLeftStringFormat, timeLeft);
    }

    private void SetTimeUntilMeetingLabel()
    {
        if (Appointment == null) return;

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
        if (Appointment == null) return;

        if (Appointment.Start < DateTime.Now && Appointment.End > DateTime.Now)
            SetProgressBarForAppointmentInProgress();
        else if (Appointment.Start > DateTime.Now) SetProgressBarForFutureAppointment();
    }

    private void SetProgressBarForAppointmentInProgress()
    {
        if (Appointment == null) return;

        var timeLeft = Appointment.End - DateTime.Now;

        var progress = (int)((1 - timeLeft.TotalMinutes / Appointment.Duration) * 100);
        progressBar.Value = progress;
    }

    private void SetProgressBarForFutureAppointment()
    {
        if (Appointment == null) return;

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
        if (Appointment == null) return;

        BackColor = Appointment.CategoryColor;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Draw the bottom border
        using Pen borderPen = new(Color.Black);
        e.Graphics.DrawLine(borderPen, 0, Height - 1, Width, Height - 1);
    }
}