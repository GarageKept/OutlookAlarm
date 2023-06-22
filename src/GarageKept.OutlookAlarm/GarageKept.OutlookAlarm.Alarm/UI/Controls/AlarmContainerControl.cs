using GarageKept.OutlookAlarm.Alarm.AlarmManager;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarm.UI.Controls;

public partial class AlarmContainerControl : TableLayoutPanel, IAlarmContainerControl
{
    private readonly IAlarmControl?[] _alarmControls = new IAlarmControl?[Program.AppSettings.Alarm.MaxAlarmsToShow];

    public AlarmContainerControl()
    {
        InitializeComponent();

        if (Program.AlarmManager != null)
        {
            Program.AlarmManager.AlarmAdded -= UpdateAppointmentControls;
            Program.AlarmManager.AlarmAdded += UpdateAppointmentControls;
            Program.AlarmManager.AlarmChanged -= UpdateAppointmentControls;
            Program.AlarmManager.AlarmChanged += UpdateAppointmentControls;
            Program.AlarmManager.AlarmRemoved -= UpdateAppointmentControls;
            Program.AlarmManager.AlarmRemoved += UpdateAppointmentControls;
            Program.AlarmManager.AlarmsUpdated -= UpdateAppointmentControls;
            Program.AlarmManager.AlarmsUpdated += UpdateAppointmentControls;
        }

        SuspendLayout();

        InitializeAlarmControls();
        InitializeFooterProgressBar();

        ResumeLayout();

        RefreshTimer.Tick += RefreshTimer_Tick;
        RefreshTimer.Start();
    }

    private AlarmProgressBar FooterProgressBar { get; } = new();
    private Timer RefreshTimer { get; } = new() { Interval = 5000 };

    private void AddFooterRow()
    {
        var newRow = RowCount; // Get the current row count
        RowCount++; // Increment the row count by 1

        // Create a new row style with a fixed height
        var rowStyle = new RowStyle(SizeType.AutoSize);

        // Add the row style to the TableLayoutPanel
        RowStyles.Add(rowStyle);

        // Add the label to the TableLayoutPanel at row 0, column 0
        Controls.Add(FooterProgressBar, 0, newRow);

        FooterProgressBar.Dock = DockStyle.Fill; // Fill the entire row

        RefreshTimer_Tick(this, null);
    }

    private void InitializeAlarmControls()
    {
        RowCount = Program.AppSettings.Alarm.MaxAlarmsToShow + 1;

        for (var i = 0; i < _alarmControls.Length; i++)
        {
            var alarmControl = Program.ServiceProvider?.GetRequiredService<IAlarmControl>();
            _alarmControls[i] = alarmControl;

            if (alarmControl is Control control)
            {
                alarmControl.Visible = true;

                // Add the control to the TableLayoutPanel
                Controls.Add(control, 0, i);

                // Create a new row style with a fixed height
                var rowStyle = new RowStyle(SizeType.Absolute, 0);

                // Add the row style to the TableLayoutPanel
                RowStyles.Add(rowStyle);
            }
        }
    }

    private void InitializeFooterProgressBar()
    {
        // Create an additional label for the empty row
        FooterProgressBar.RightToLeftLayout = false;
        FooterProgressBar.Padding = Padding.Empty;
        FooterProgressBar.Margin = Padding.Empty;
        FooterProgressBar.Minimum = 0;
        FooterProgressBar.Maximum = 3600; // 1 hour in seconds

        FooterProgressBar.Width = Parent?.Width ?? Program.AppSettings.Main.MinimumWidth;

        FooterProgressBar.Width = Program.AppSettings.Main.MinimumWidth;

        AddFooterRow();
    }

    private void RefreshTimer_Tick(object? sender, EventArgs? e)
    {
        var currentAppointment = Program.AlarmManager?.GetCurrentAppointment();
        var nextAppointment = Program.AlarmManager?.GetNextAppointment();
        var backColor = Program.AppSettings.Color.GreenColor;
        var barColor = Program.AppSettings.Color.GreenColor;
        var value = 3600;

        if (currentAppointment != null) barColor = Program.AppSettings.Color.RedColor;

        if (currentAppointment?.End >= nextAppointment?.Start) backColor = Program.AppSettings.Color.YellowColor;

        var timeUntilNextAppointment = nextAppointment?.Start.Subtract(DateTime.Now) ??
                                       currentAppointment?.End.Subtract(DateTime.Now) ?? TimeSpan.FromHours(1);


        if (nextAppointment is null)
        {
            backColor = Program.AppSettings.Color.GreenColor;
        }
        else
        {
            if (timeUntilNextAppointment < TimeSpan.FromMinutes(60)) backColor = Program.AppSettings.Color.YellowColor;

            if (timeUntilNextAppointment < TimeSpan.FromMinutes(Program.AppSettings.Alarm.AlarmWarningTime))
            {
                barColor = Program.AppSettings.Color.YellowColor;
                backColor = Program.AppSettings.Color.RedColor;
            }

            if (timeUntilNextAppointment < TimeSpan.FromMinutes(Program.AppSettings.Alarm.AlarmWarningTime))
            {
                barColor = Program.AppSettings.Color.YellowColor;
                backColor = Program.AppSettings.Color.RedColor;
            }

            if (timeUntilNextAppointment < TimeSpan.FromMinutes(5)) backColor = Program.AppSettings.Color.RedColor;
        }

        FooterProgressBar.BackgroundColor = backColor;
        FooterProgressBar.BarColor = barColor;

        // now we figure out what the value should be
        if (timeUntilNextAppointment.TotalSeconds <= 3600)
            // Update the progress bar value based on the time left
            value = (int)timeUntilNextAppointment.TotalSeconds;
        //value = 3600 - (int)timeUntilNextAppointment.TotalSeconds;

        FooterProgressBar.Value = value;
    }

    private void UpdateAlarmControls()
    {
        // Pause updates until we redo everything
        SuspendLayout();

        if (Program.AlarmManager != null)
        {
            var activeAlarms = Program.AlarmManager.GetActiveAlarms().OrderBy(a => a.Start).ToArray();

            for (var i = 0; i < _alarmControls.Length; i++)
            {
                _alarmControls[i]!.Alarm = i < activeAlarms.Length ? activeAlarms[i] : null;

                if (_alarmControls[i]!.Alarm is not null)
                    RowStyles[i] = new RowStyle(SizeType.AutoSize);
                else
                    RowStyles[i] = new RowStyle(SizeType.Absolute, 0);
            }
        }

        if (Parent is IMainForm parentForm)
        {
            parentForm.AddMouseEvents((Form)parentForm);
            parentForm.CheckMouseLeaveForm();
        }

        ResumeLayout(true);
    }

    private void UpdateAppointmentControls(object? sender, AlarmEventArgs e)
    {
        if (!IsHandleCreated) return;

        if (InvokeRequired && IsHandleCreated)
            Invoke((MethodInvoker)UpdateAlarmControls);
        else
            UpdateAlarmControls();
    }
}