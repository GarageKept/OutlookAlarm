using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GarageKept.OutlookAlarm.Alarm.UI.Controls;

public partial class AlarmContainerControl : TableLayoutPanel, IAlarmContainerControl
{
    private IAlarm[] _alarms;
    private IAlarmControl[] AlarmControls { get; set; }

    public IEnumerable<IAlarm> Alarms
    {
        get => _alarms;
        set
        {
            _alarms = value.OrderBy(s => s.Start).ToArray();

            for (var i = 0; i < AlarmControls.Length && i < _alarms.Length; i++)
            {
                AlarmControls[i].Alarm = _alarms[i];
            }
        }
    }

    private ISettings Settings { get; }

    public AlarmContainerControl(ISettings settings)
    {
        Settings = settings;

        _alarms = Array.Empty<IAlarm>();

        AlarmControls = new IAlarmControl[Settings.Alarm.MaxAlarmsToShow];

        InitializeComponent();

        SuspendLayout();

        InitializeAlarmControls();
        InitializeFooterProgressBar();

        ResumeLayout();

    }

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
        if (Program.ServiceProvider is null) return;

        RowCount = Settings.Alarm.MaxAlarmsToShow + 1;

        for (var i = 0; i < AlarmControls.Length; i++)
        {
            var alarmControl = Program.ServiceProvider.GetRequiredService<IAlarmControl>();
            AlarmControls[i] = alarmControl;

            if (alarmControl is not Control control) continue;

            control.Visible = false;

            // Add the control to the TableLayoutPanel
            Controls.Add(control, 0, i);

            // Create a new row style with a fixed height
            var rowStyle = new RowStyle(SizeType.Absolute, 0);

            // Add the row style to the TableLayoutPanel
            RowStyles.Add(rowStyle);
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

        FooterProgressBar.Width = Parent?.Width ?? Settings.Main.MinimumWidth;

        FooterProgressBar.Width = Settings.Main.MinimumWidth;

        AddFooterRow();
    }

    private void RefreshTimer_Tick(object? sender, EventArgs? e)
    {
        var currentAppointment = GetCurrentAppointment();
        var nextAppointment = GetNextAppointment();
        var backColor = Settings.Color.GreenColor;
        var barColor = Settings.Color.GreenColor;
        var value = 3600;

        if (currentAppointment != null) barColor = Settings.Color.RedColor;

        if (currentAppointment?.End >= nextAppointment?.Start) backColor = Settings.Color.YellowColor;

        var timeUntilNextAppointment = nextAppointment?.Start.Subtract(DateTime.Now) ??
                                       currentAppointment?.End.Subtract(DateTime.Now) ?? TimeSpan.FromHours(1);


        if (nextAppointment is null)
        {
            backColor = Settings.Color.GreenColor;
        }
        else
        {
            if (timeUntilNextAppointment < TimeSpan.FromMinutes(60)) backColor = Settings.Color.YellowColor;

            if (timeUntilNextAppointment < TimeSpan.FromMinutes(Settings.Alarm.AlarmWarningTime))
            {
                barColor = Settings.Color.YellowColor;
                backColor = Settings.Color.RedColor;
            }

            if (timeUntilNextAppointment < TimeSpan.FromMinutes(Settings.Alarm.AlarmWarningTime))
            {
                barColor = Settings.Color.YellowColor;
                backColor = Settings.Color.RedColor;
            }

            if (timeUntilNextAppointment < TimeSpan.FromMinutes(5)) backColor = Settings.Color.RedColor;
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

    private IAlarm? GetNextAppointment()
    {
        return Alarms.FirstOrDefault(a => a.Start > DateTime.Now);
    }

    private IAlarm? GetCurrentAppointment()
    {
        var now = DateTime.Now;

        return Alarms.FirstOrDefault(a => a.Start < now && a.End > now);
    }
}