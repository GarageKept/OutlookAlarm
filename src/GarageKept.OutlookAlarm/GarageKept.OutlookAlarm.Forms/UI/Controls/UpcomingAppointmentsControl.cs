using GarageKept.OutlookAlarm.Forms.Common;
using GarageKept.OutlookAlarm.Forms.UI.Forms;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Forms.UI.Controls;

public partial class UpcomingAppointmentsControl : UserControl
{
    public UpcomingAppointmentsControl()
    {
        InitializeComponent();

        tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        if (DesignMode) return;

        InitializeFooterProgressBar();

        AppointmentManager.Refresh += AppointmentManager_Refresh;
        AppointmentManager.Start();

        RefreshTimer.Tick += RefreshTimer_Tick;
        RefreshTimer.Start();
    }

    public Timer RefreshTimer { get; set; } = new() { Interval = 1000 };

    public GarageKeptProgressBar FooterProgressBar { get; set; } = new();

    private void RefreshTimer_Tick(object? sender, EventArgs e)
    {
        var currentAppointment = AppointmentManager.GetCurrentAppointment();
        var nextAppointment = AppointmentManager.GetNextAppointment();
        var backColor = Program.ApplicationSettings.GreenColor;
        var barColor = Program.ApplicationSettings.GreenColor;
        var value = 3600;

        if (currentAppointment != null) barColor = Program.ApplicationSettings.RedColor;

        if (currentAppointment?.End >= nextAppointment?.Start) backColor = Program.ApplicationSettings.YellowColor;

        var timeUntilNextAppointment = nextAppointment?.Start.Subtract(DateTime.Now) ?? new TimeSpan(1, 1, 1);


        if (timeUntilNextAppointment < TimeSpan.FromMinutes(60)) backColor = Program.ApplicationSettings.YellowColor;
        
        if (timeUntilNextAppointment < TimeSpan.FromMinutes(5)) backColor = Program.ApplicationSettings.RedColor;

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

        var tempControl = new AppointmentItemControl(null);

        FooterProgressBar.Width = tempControl.Width;
    }

    private void AppointmentManager_Refresh(object? sender, EventArgs? e)
    {
        var appointments = AppointmentManager.Appointments;

        UpdateAppointmentControls(appointments);
    }

    public void ResetAppointmentControls()
    {
        UpdateAppointmentControls(AppointmentManager.Appointments);
    }

    public void UpdateAppointmentControls(CalendarEvents appointments)
    {
        // Pause updates until we redo everything
        SuspendLayout();

        // Clear the controls collection
        tableLayoutPanel.Controls.Clear();

        // Clear the row styles collection
        tableLayoutPanel.RowStyles.Clear();

        // Reset the row count to 0
        tableLayoutPanel.RowCount = 0;

        foreach (var appointment in appointments.Values.OrderBy(a=>a.Start)) AddRow(new AppointmentItemControl(appointment));

        AddFooterRow();

        if (FindForm() is MainForm parentForm)
        {
 //           parentForm.SubscribeToMouseEvents(parentForm);
            parentForm.AddMouseEvents(parentForm);

            if(parentForm.Top < 0)
                parentForm.Top = -parentForm.Height + Program.ApplicationSettings.BarSize;
        }


        // OK now get everything rendered again
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