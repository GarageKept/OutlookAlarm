using System.Reflection;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

public partial class MainForm : BaseForm, IMainForm
{
    private readonly Timer _slidingTimer = new() { Interval = 10 };

    private bool _isExpanded;

    public MainForm(ISettings settings, IAlarmManager alarmManager, IAlarmContainerControl containerControl) :
        base(true)
    {
        Settings = settings;
        AlarmManager = alarmManager;
        alarmManager.AlarmsUpdatedCallback += UpdateAlarms;

        InitializeComponent();
        FormClosing += OnFormClosing;
        ContainerControl = containerControl;
        Controls.Add(ContainerControl as Control);

        SetupPosition();
        SetupContextMenu();

        // Subscribe to form's mouse enter and leave events
        MouseEnter += MainWindow_MouseEnter;
        MouseLeave += MainWindow_MouseLeave;
        // Subscribe to MouseEnter and MouseLeave events for each child control
        AddMouseEvents(this);

        // Initialize and set up the sliding timer
        _slidingTimer.Tick += SlidingTimer_Tick;
        _slidingTimer.Start();

        AlarmManager = alarmManager;
        AlarmManager.Start();
    }

    private ISettings Settings { get; }
    private IAlarmManager AlarmManager { get; }
    private IAlarmContainerControl ContainerControl { get; }

    public void UpdateAlarms(IEnumerable<IAlarm> alarms) { ContainerControl.Alarms = alarms; }

    /// <summary>
    ///     Subscribes to MouseEnter and MouseLeave events for each child control.
    /// </summary>
    /// <param name="control">The control we are adding mouse events to.</param>
    public void AddMouseEvents(Control control)
    {
        control.MouseEnter -= ChildControl_MouseEnter;
        control.MouseEnter += ChildControl_MouseEnter;
        control.MouseLeave -= ChildControl_MouseLeave;
        control.MouseLeave += ChildControl_MouseLeave;

        foreach (Control child in control.Controls) AddMouseEvents(child);

        SetDraggable(this);
    }

    /// <summary>
    ///     Checks if the mouse pointer is still within the form bounds.
    /// </summary>
    public void CheckMouseLeaveForm()
    {
        var clientCursorPos = PointToClient(Cursor.Position);

        if (ClientRectangle.Contains(clientCursorPos) || rightClickMenu.Visible) return;

        _isExpanded = false;
        _slidingTimer.Start();
    }

    /// <summary>
    ///     Event handler for child control's MouseEnter event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void ChildControl_MouseEnter(object? sender, EventArgs e) { MainWindow_MouseEnter(sender, e); }

    /// <summary>
    ///     Event handler for child control's MouseLeave event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void ChildControl_MouseLeave(object? sender, EventArgs e) { CheckMouseLeaveForm(); }

    /// <summary>
    ///     Event handler for the FormClosing event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Save the current position so we can restore it back to where it was on next run
        Settings.Main.Left = Location.X;
    }

    /// <summary>
    ///     Event handler for the MouseEnter event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    public void MainWindow_MouseEnter(object? sender, EventArgs e)
    {
        _isExpanded = true;
        _slidingTimer.Start();
    }

    /// <summary>
    ///     Event handler for the MouseLeave event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    public void MainWindow_MouseLeave(object? sender, EventArgs e)
    {
        if (rightClickMenu.Visible) return;

        _isExpanded = false;
        _slidingTimer.Start();
    }

    private void OnFormClosing(object? sender, FormClosingEventArgs e)
    {
        AlarmManager.Stop();
        AlarmManager.Dispose();
        ContainerControl.Dispose();
    }

    private void RightClick_ResetAllAppointments(object? sender, EventArgs e) { AlarmManager.Reset(); }

    /// <summary>
    ///     Event handler for the About menu item click event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private static void RightClickMenu_AboutClick(object? sender, EventArgs e)
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

        // Implement your About functionality here
        MessageBox.Show(@"Outlook Alarm by Garage Kept " + version);
    }

    /// <summary>
    ///     Event handler for the RefreshTimer menu item click event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void RightClickMenu_FetchNowClick(object? sender, EventArgs e) { AlarmManager.ForceFetch(); }

    /// <summary>
    ///     Event handler for the OutlookAlarmSettings menu item click event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void RightClickMenu_SettingsClick(object? sender, EventArgs e)
    {
        if (Program.ServiceProvider == null) return;

        var settingsForm = Program.ServiceProvider.GetRequiredService<ISettingsForm>();
        settingsForm.Owner = this;
        settingsForm.ShowDialog();
    }

    private void SetupContextMenu()
    {
        // Initialize and set up the context menu
        rightClickMenu.Items.Clear();
        rightClickMenu.Items.Add("Settings", null, RightClickMenu_SettingsClick);
        rightClickMenu.Items.Add("About", null, RightClickMenu_AboutClick);
        var advanced = new ToolStripMenuItem("Advanced");
        var advancedDropdown = new ToolStripDropDownMenu();
        var refresh = new ToolStripMenuItem("Fetch Now");
        refresh.Click += RightClickMenu_FetchNowClick;
        advancedDropdown.Items.Add(refresh);
        var reset = new ToolStripMenuItem("Reset All");
        reset.Click += RightClick_ResetAllAppointments;
        advancedDropdown.Items.Add(reset);
        advanced.DropDown = advancedDropdown;
        rightClickMenu.Items.Add(new ToolStripSeparator());
        rightClickMenu.Items.Add(advanced);
        rightClickMenu.Items.Add(new ToolStripSeparator());
        rightClickMenu.Items.Add("Close", null, (_, _) => Close());
    }

    private void SetupPosition()
    {
        // Set the form's start position to manual
        StartPosition = FormStartPosition.Manual;
        // Set the form's location
        Location = new Point(Settings.Main.Left, 0);
        // Save the form position when moved
        Move += (_, _) => { Settings.Main.Left = Left; };
    }

    /// <summary>
    ///     Event handler for the Timer Tick event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void SlidingTimer_Tick(object? sender, EventArgs e)
    {
        var targetY = _isExpanded ? 0 : -Height + Settings.Main.BarSize;

        if (Math.Abs(Location.Y - targetY) <= 1)
        {
            Location = Location with { Y = targetY };
            _slidingTimer.Stop();
        }
        else
        {
            var step = (targetY - Location.Y) / Settings.Main.SliderSpeed;

            if (step == 0) step = targetY > Location.Y ? 1 : -1;

            Location = new Point(Location.X, Location.Y + step);
        }
    }
}