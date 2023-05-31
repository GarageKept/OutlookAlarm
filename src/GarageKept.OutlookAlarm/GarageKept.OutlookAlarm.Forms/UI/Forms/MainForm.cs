using GarageKept.OutlookAlarm.Forms.Common;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Forms.UI.Forms;

public partial class MainForm : BaseForm
{
    private readonly Timer _slidingTimer = new() { Interval = 10 };
    private bool _isExpanded;

    public MainForm() : base(true)
    {
        InitializeComponent();

        // Set the form's start position to manual
        StartPosition = FormStartPosition.Manual;

        // Calculate the form's initial x and y positions
        var xPos = Program.ApplicationSettings.Left;
        var yPos = -Height + 10;

        // Set the form's location
        Location = new Point(xPos, yPos);

        // Subscribe to form's mouse enter and leave events
        MouseEnter += MainWindow_MouseEnter;
        MouseLeave += MainWindow_MouseLeave;

        // Initialize and set up the context menu
        rightClickMenu.Items.Clear();
        rightClickMenu.Items.Add("RefreshTimer", null, RightClickMenu_RefreshClick);
        rightClickMenu.Items.Add("Settings", null, RightClickMenu_SettingsClick);
        rightClickMenu.Items.Add("About", null, RightClickMenu_AboutClick);
        rightClickMenu.Items.Add(new ToolStripSeparator());
        rightClickMenu.Items.Add("Close", null, (_, _) => Close());

        // Subscribe to MouseEnter and MouseLeave events for each child control
        SubscribeToMouseEvents(this);
        AddMouseEvents(this);

        // Initialize and set up the sliding timer
        _slidingTimer.Tick += SlidingTimer_Tick;
        _slidingTimer.Start();
    }

    /// <summary>
    ///     Subscribes to MouseEnter and MouseLeave events for each child control.
    /// </summary>
    /// <param name="control">The control we are adding mouse events to.</param>
    public void AddMouseEvents(Control control)
    {
        control.MouseEnter += ChildControl_MouseEnter;
        control.MouseLeave += ChildControl_MouseLeave;

        foreach (Control child in control.Controls) AddMouseEvents(child);
    }

    /// <summary>
    ///     Event handler for child control's MouseEnter event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void ChildControl_MouseEnter(object? sender, EventArgs e)
    {
        MainWindow_MouseEnter(sender, e);
    }

    /// <summary>
    ///     Event handler for child control's MouseLeave event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void ChildControl_MouseLeave(object? sender, EventArgs e)
    {
        CheckMouseLeaveForm();
    }

    /// <summary>
    ///     Checks if the mouse pointer is still within the form bounds.
    /// </summary>
    private void CheckMouseLeaveForm()
    {
        var clientCursorPos = PointToClient(Cursor.Position);

        if (ClientRectangle.Contains(clientCursorPos) || rightClickMenu.Visible) return;

        _isExpanded = false;
        _slidingTimer.Start();
    }

    /// <summary>
    ///     Event handler for the FormClosing event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Save the current position so we can restore it back to where it was on next run
        Program.ApplicationSettings.Left = Location.X;

        Program.ApplicationSettings.Save();
    }

    /// <summary>
    ///     Event handler for the MouseEnter event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void MainWindow_MouseEnter(object? sender, EventArgs e)
    {
        _isExpanded = true;
        _slidingTimer.Start();
    }

    /// <summary>
    ///     Event handler for the MouseLeave event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void MainWindow_MouseLeave(object? sender, EventArgs e)
    {
        if (rightClickMenu.Visible) return;

        _isExpanded = false;
        _slidingTimer.Start();
    }

    /// <summary>
    ///     Event handler for the About menu item click event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void RightClickMenu_AboutClick(object? sender, EventArgs e)
    {
        // Implement your About functionality here
    }

    /// <summary>
    ///     Event handler for the Settings menu item click event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void RightClickMenu_SettingsClick(object? sender, EventArgs e)
    {
        var settingsForm = new SettingsWindow();

        settingsForm.ShowDialog(this);
    }

    /// <summary>
    ///     Event handler for the RefreshTimer menu item click event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void RightClickMenu_RefreshClick(object? sender, EventArgs e)
    {
        AppointmentManager.ForceRefresh();
    }

    /// <summary>
    ///     Event handler for the Timer Tick event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void SlidingTimer_Tick(object? sender, EventArgs e)
    {
        var targetY = _isExpanded ? 0 : -Height + 10;

        if (Math.Abs(Location.Y - targetY) <= 1)
        {
            Location = Location with { Y = targetY };
            _slidingTimer.Stop();
        }
        else
        {
            var step = (targetY - Location.Y) / Program.ApplicationSettings.SliderSpeed;

            if (step == 0) step = targetY > Location.Y ? 1 : -1;

            Location = new Point(Location.X, Location.Y + step);
        }
    }
}