﻿using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarms.UI.Controls;

public partial class AlarmControl : UserControl, IAlarmControl
{
    private readonly ContextMenuStrip _appointmentContextMenuStrip = new();

    public AlarmControl()
    {
        InitializeComponent();

        RefreshTimer.Tick += Refresh_Tick;
        RefreshTimer.Start();

        AddContextMenu();

        UpdateDisplay();
    }

    private Timer RefreshTimer { get; } = new() { Interval = 1000 };

    public ISettings? AppSettings { get; set; }

    public IAlarm? Alarm { get; set; }

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

    private void DismissAppointment(object? sender, EventArgs e)
    {
        if (Alarm is null) return;

        if (Parent is not IAlarmContainerControl) return;

        var parent = Parent as IAlarmContainerControl;
        parent?.DismissAlarm(Alarm);
    }

    private void RemoveAppointment(object? sender, EventArgs e)
    {
        if (Alarm is null) return;

        if (Parent is not IAlarmContainerControl) return;

        var parent = Parent as IAlarmContainerControl;
        parent?.RemoveAlarm(Alarm);
    }

    private void Control_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right) _appointmentContextMenuStrip.Show(this, e.Location);
    }

    private void Refresh_Tick(object? sender, EventArgs e)
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (Alarm == null) return;

        SetSubjectLabel();
        SetTimeLabels();
        SetProgressBar();
        SetBackgroundColor();
    }

    private void SetSubjectLabel()
    {
        if (Alarm is null) return;

        SubjectLabel.Text = Alarm.Name;
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

        TimeLeft.Text = string.Format(AppSettings?.TimeLeftStringFormat ?? "h:mm tt", timeLeft);
    }

    private void SetTimeUntilMeetingLabel()
    {
        if (Alarm is null) return;

        var timeUntilMeeting = Alarm.Start - DateTime.Now;

        var formatString = timeUntilMeeting.Hours > 0 ? "{0:hh}h {0:mm}m {0:ss}s" : "{0:mm}m {0:ss}s";

        TimeLeft.Text = string.Format(formatString, timeUntilMeeting);
    }

    private void SetTimeRightLabel(string text)
    {
        TimeRight.Text = text;
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
        progressBar.Value = progress;
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

    private void SetBackgroundColor()
    {
        if (Alarm is null) return;

        BackColor = Alarm.AlarmColor;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Draw the bottom border
        using Pen borderPen = new(Color.Black);
        e.Graphics.DrawLine(borderPen, 0, Height - 1, Width, Height - 1);
    }
}