using GarageKept.OutlookAlarm.Alarm.AlarmManager;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarm.UI.Controls;

public partial class AlarmContainerControl : UserControl, IAlarmContainerControl
{
    public AlarmContainerControl()
    {
        InitializeComponent();

        tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        
        Program.AlarmManager.AlarmAdded += UpdateAppointmentControls;
        Program.AlarmManager.AlarmChanged += UpdateAppointmentControls;
        Program.AlarmManager.AlarmRemoved += UpdateAppointmentControls;
        Program.AlarmManager.AlarmsUpdated += UpdateAppointmentControls;

        InitializeFooterProgressBar();

        RefreshTimer.Tick += RefreshTimer_Tick;
        RefreshTimer.Start();
    }

    public Timer RefreshTimer { get; set; } = new() { Interval = 5000 };

    public AlarmProgressBar FooterProgressBar { get; set; } = new();

    private void RefreshTimer_Tick(object? sender, EventArgs e)
    {
        var currentAppointment = Program.AlarmManager.GetCurrentAppointment();
        var nextAppointment = Program.AlarmManager.GetNextAppointment();
        var backColor = Program.AppSettings.Color.GreenColor;
        var barColor = Program.AppSettings.Color.GreenColor;
        var value = 3600;

        if (currentAppointment != null) barColor = Program.AppSettings.Color.RedColor;

        if (currentAppointment?.End >= nextAppointment?.Start) backColor =Program.AppSettings.Color.YellowColor;

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

    private void InitializeFooterProgressBar()
    {
        // Create an additional label for the empty row
        FooterProgressBar.RightToLeftLayout = false;
        FooterProgressBar.Padding = Padding.Empty;
        FooterProgressBar.Margin = Padding.Empty;
        FooterProgressBar.Minimum = 0;
        FooterProgressBar.Maximum = 3600; // 1 hour in seconds

        FooterProgressBar.Width = Parent?.Width ?? Program.AppSettings.Main.MinimumWidth;
    }

    private void StopAllTimerInChildren(ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            if (control is IAlarmControl alarmControl)
            {
                alarmControl.StopTimers();
            }

            StopAllTimerInChildren(control.Controls);
        }

    }

    public void UpdateAppointmentControls(object? sender, AlarmEventArgs e)
    {
        if (!IsHandleCreated) return;

        if (InvokeRequired && IsHandleCreated)
            Invoke((MethodInvoker)UpdateAlarmControls);
        else
            UpdateAlarmControls();
    }

    private void UpdateAlarmControls()
    {
        // Pause updates until we redo everything
        SuspendLayout();

        StopAllTimerInChildren(Controls);

        // Clear the controls collection
        tableLayoutPanel.Controls.Clear();

        // Clear the row styles collection
        tableLayoutPanel.RowStyles.Clear();

        // Reset the row count to 0
        tableLayoutPanel.RowCount = 0;

        foreach (var alarm in Program.AlarmManager.GetActiveAlarms().OrderBy(a => a.Start))
        {
            var alarmControl =
                Program.ServiceProvider
                    .GetRequiredService<IAlarmControl>(); //new AlarmControl { Alarm = alarm, AppSettings = AppSettings };
            alarmControl.Alarm = alarm;
            alarmControl.UpdateDisplay();

            AddRow(alarmControl as Control);
        }

        AddFooterRow();

        if (Parent is IMainForm parentForm)
        {
            parentForm.AddMouseEvents((Form)parentForm);

            if (Parent.Top > 0)
                Parent.Top = 0;

            if (Parent.Top < 0)
                Parent.Top = -Parent.Height + Program.AppSettings.Main.BarSize;

            parentForm.CheckMouseLeaveForm();
        }

        ResumeLayout();
    }

    public void AddRow(Control? control)
    {
        if (control == null) return;

        var newRow = tableLayoutPanel.RowCount; // Get the current row count
        tableLayoutPanel.RowCount++; // Increment the row count by 1

        // Create a new row style with a fixed height
        var rowStyle = new RowStyle(SizeType.AutoSize);

        // Add the row style to the TableLayoutPanel
        tableLayoutPanel.RowStyles.Add(rowStyle);

        tableLayoutPanel.Controls.Add(control, 0, newRow);
    }

    public void AddFooterRow()
    {
        var newRow = tableLayoutPanel.RowCount; // Get the current row count
        tableLayoutPanel.RowCount++; // Increment the row count by 1

        // Create a new row style with a fixed height
        var rowStyle = new RowStyle(SizeType.Absolute, 20); // Set the height to 20 pixels

        // Add the row style to the TableLayoutPanel
        tableLayoutPanel.RowStyles.Add(rowStyle);

        // Add the label to the TableLayoutPanel at row 0, column 0
        tableLayoutPanel.Controls.Add(FooterProgressBar, 0, newRow);

        FooterProgressBar.Dock = DockStyle.Fill; // Fill the entire row
    }
}