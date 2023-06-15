using GarageKept.OutlookAlarm.Alarm.Alarm;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using GarageKept.OutlookAlarm.Alarms.UI.Controls;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarm.UI.Controls;

public partial class AlarmContainerControl : UserControl, IAlarmContainerControl
{
    public AlarmContainerControl(ISettings appSettings, IAlarmManager alarmManager)
    {
        InitializeComponent();

        tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        AppSettings = appSettings;
        AlarmManager = alarmManager;

        alarmManager.AlarmAdded += UpdateAppointmentControls;
        alarmManager.AlarmChanged += UpdateAppointmentControls;
        alarmManager.AlarmRemoved += UpdateAppointmentControls;
        alarmManager.AlarmsUpdated += UpdateAppointmentControls;

        InitializeFooterProgressBar();

        RefreshTimer.Tick += RefreshTimer_Tick;
        RefreshTimer.Start();
    }

    public ISettings AppSettings { get; set; }
    public IAlarmManager AlarmManager { get; set; }

    public Timer RefreshTimer { get; set; } = new() { Interval = 1000 };

    public AlarmProgressBar FooterProgressBar { get; set; } = new();

    public void DismissAlarm(IAlarm alarm)
    {
        throw new NotImplementedException();
    }

    public void RemoveAlarm(IAlarm alarm)
    {
        AlarmManager.RemoveAlarm(alarm);
    }

    private void RefreshTimer_Tick(object? sender, EventArgs e)
    {
        var currentAppointment = AlarmManager.GetCurrentAppointment();
        var nextAppointment = AlarmManager.GetNextAppointment();
        var backColor = AppSettings.GreenColor;
        var barColor = AppSettings.GreenColor;
        var value = 3600;

        if (currentAppointment != null) barColor = AppSettings.RedColor;

        if (currentAppointment?.End >= nextAppointment?.Start) backColor = AppSettings.YellowColor;

        var timeUntilNextAppointment = nextAppointment?.Start.Subtract(DateTime.Now) ?? new TimeSpan(1, 1, 1);


        if (timeUntilNextAppointment < TimeSpan.FromMinutes(60)) backColor = AppSettings.YellowColor;

        if (timeUntilNextAppointment < TimeSpan.FromMinutes(AppSettings.AlarmWarningTime))
        {
            barColor = AppSettings.YellowColor;
            backColor = AppSettings.RedColor;
        }

        if (timeUntilNextAppointment < TimeSpan.FromMinutes(AppSettings.AlarmWarningTime))
        {
            barColor = AppSettings.YellowColor;
            backColor = AppSettings.RedColor;
        }

        if (timeUntilNextAppointment < TimeSpan.FromMinutes(5)) backColor = AppSettings.RedColor;

        FooterProgressBar.BackgroundColor = backColor;
        FooterProgressBar.BarColor = barColor;

        // now we figure out what the value should be
        if (timeUntilNextAppointment.TotalSeconds <= 3600)
            // Update the progress bar value based on the time left
            value = 3600 - (int)timeUntilNextAppointment.TotalSeconds;

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

        FooterProgressBar.Width = Parent?.Width ?? 50;
    }

    public void UpdateAppointmentControls(object? sender, AlarmEventArgs e)
    {
        // Pause updates until we redo everything
        SuspendLayout();

        // Clear the controls collection
        tableLayoutPanel.Controls.Clear();

        // Clear the row styles collection
        tableLayoutPanel.RowStyles.Clear();

        // Reset the row count to 0
        tableLayoutPanel.RowCount = 0;

        foreach (var alarm in AlarmManager.GetActiveAlarms().OrderBy(a => a.Start))
            AddRow(new AlarmControl { Alarm = alarm, AppSettings = AppSettings });

        AddFooterRow();

        if (Parent is IMainForm parentForm)
        {
            parentForm.SubscribeToMouseEvents();

            if (Parent.Top < 0)
                Parent.Top = -Parent.Height + AppSettings.BarSize;
        }

        ResumeLayout();
    }

    public void AddRow(Control control)
    {
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