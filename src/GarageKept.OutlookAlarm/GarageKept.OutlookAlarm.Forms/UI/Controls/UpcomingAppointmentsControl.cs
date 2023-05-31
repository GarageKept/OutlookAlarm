using GarageKept.OutlookAlarm.Forms.Common;
using GarageKept.OutlookAlarm.Forms.UI.Forms;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Forms.UI.Controls;

public partial class UpcomingAppointmentsControl : UserControl
{
    public UpcomingAppointmentsControl()
    {
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        InitializeComponent();

        InitializeFooterProgressBar();

        tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;

        AppointmentManager.Refresh += AppointmentManager_Refresh;
        AppointmentManager.Start();

        RefreshTimer.Tick += RefreshTimer_Tick;
        RefreshTimer.Start();
    }

    public Timer RefreshTimer { get; set; } = new() { Interval = 1000 };

    public GarageKeptProgressBar FooterProgressBar { get; set; } = new();

    private void RefreshTimer_Tick(object? sender, EventArgs e)
    {
        var appointment = AppointmentManager.GetNextAppointment();

        var timeLeft = appointment?.Start.Subtract(DateTime.Now) ?? new TimeSpan(1, 1, 1);

        // Check if the appointment is within an hour of starting
        if (timeLeft.TotalSeconds <= 3600)
        {
            // Update the progress bar value based on the time left
            FooterProgressBar.Value = 3600 - (int)timeLeft.TotalSeconds;

            if (timeLeft.TotalMinutes <= 15)
                FooterProgressBar.BackgroundColor = Program.ApplicationSettings.YellowColor;
            else if (timeLeft.TotalMinutes <= 5)
                FooterProgressBar.BackgroundColor = Program.ApplicationSettings.RedColor;
            else
                FooterProgressBar.BackgroundColor = Program.ApplicationSettings.GreenColor;
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
    }

    private void AppointmentManager_Refresh(object? sender, EventArgs? e)
    {
        var appointments = AppointmentManager.Appointments;

        UpdateAppointmentControls(appointments);
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

        foreach (var appointment in appointments) AddRow(new AppointmentItemControl(appointment));

        AddFooterRow();

        if (FindForm() is MainForm parentForm)
        {
            parentForm.SubscribeToMouseEvents(parentForm);
            parentForm.AddMouseEvents(parentForm);
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