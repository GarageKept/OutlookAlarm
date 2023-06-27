using System.Collections;
using GarageKept.OutlookAlarm.Alarm.AlarmManager;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

public partial class AlarmForm : BaseForm, IAlarmForm
{
    private bool _playSound = true;

    public AlarmForm(IAlarmManager alarmManager, IMediaPlayer mediaPlayer, ISettings settings) : base(false)
    {
        AlarmManager = alarmManager;
        MediaPlayerPlayer = mediaPlayer;
        Settings = settings;

        InitializeComponent();

        ShowInTaskbar = false;

        ActionSelector.Items.Clear();
        var dataSource = Enum.GetValues(typeof(AlarmAction)).Cast<AlarmAction>()
            .Where(a => a is not (AlarmAction.Remove or AlarmAction.Dismiss))
            .Select(s => new { Value = s, Text = AlarmActionHelpers.GetEnumDisplayValue(s) })
            .Where(a => a.Text != "Dismissed").ToList();

        ActionSelector.DisplayMember = "Text";
        ActionSelector.ValueMember = "Value";

        ActionSelector.DataSource = dataSource;

        Move += (_, _) =>
        {
            Settings.Alarm.Left = Left;
            Settings.Alarm.Top = Top;
        };
    }

    private IMediaPlayer MediaPlayerPlayer { get; }
    private IAlarm? Alarm { get; set; }
    private IAlarmManager AlarmManager { get; }
    private ISettings Settings { get; }

    public void Show(IAlarm alarm)
    {
        Alarm = alarm ?? throw new ArgumentNullException(typeof(IAlarm).ToString());

        var tooLateForAudio = DateTime.Now - Alarm.Start > TimeSpan.FromMinutes(Settings.Audio.TurnOffAlarmAfterStart);
        var bypassAudio = Settings.TimeManagement.BypassAudio();

        if (Alarm.IsAudible && !tooLateForAudio)
        {
            if (bypassAudio)
            {
                if (Settings.TimeManagement.ExceptionCategories.Intersect(Alarm.Categories).Any())
                    PlayAudio();
            }
            else
            {
                PlayAudio();
            }
        }

        SubjectLabel.Text = Alarm.Name;
        TimeRight.Text = Alarm.ReminderTime.ToString(Settings.Alarm.AlarmStartStringFormat);
        TimeLeft.Text = string.Format(Settings.Alarm.TimeLeftStringFormat, DateTime.Now - Alarm.Start);

        UpdateForm();

        UpdateDropdown();

        // Subscribe to MouseEnter and MouseLeave events for each child control
        SetDraggable(this);

        Location = new Point(Settings.Alarm.Left, Settings.Alarm.Top);

        SetBackGroundColor(Alarm.AlarmColor);

        RefreshTimer = new Timer { Interval = 1000 };
        RefreshTimer.Tick -= FormRefresh;
        RefreshTimer.Tick += FormRefresh;
        RefreshTimer.Enabled = true;
        RefreshTimer.Start();

        if (!IsDisposed)
            base.Show();
    }

    private void AlarmForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        RefreshTimer?.Stop();
        RefreshTimer?.Dispose();
        RefreshTimer = null;
        MediaPlayerPlayer.StopSound();
    }

    private void DismissButton_Click(object sender, EventArgs e)
    {
        if (Alarm == null) return;

        AlarmManager.ChangeAlarmState(Alarm, AlarmAction.Dismiss);

        Close();
    }

    private void FormRefresh(object? sender, EventArgs? e) { UpdateForm(); }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        MediaPlayerPlayer.StopSound();

        base.OnFormClosing(e);

        Dispose();
    }

    private void PlayAudio()
    {
        if (Alarm!.HasCustomSound)
            MediaPlayerPlayer.PlaySound(Alarm.CustomSound, true);
        else
            MediaPlayerPlayer.PlaySound(Settings.Audio.DefaultSound, true);
    }

    private void RemoveActionSelectorItem(AlarmAction action)
    {
        // Retrieve the current data source as a list of anonymous objects
        var dataSource = ((IEnumerable)ActionSelector.DataSource).Cast<dynamic>()
            .Select(item => new { Value = (AlarmAction)item.Value, item.Text }).ToList();

        if (dataSource.All(i => i.Value != action)) return;

        dataSource = dataSource.Where(i => i.Value != action).ToList();

        // Set the modified data source back to the ActionSelector ComboBox
        ActionSelector.DataSource = null; // Reset the data source
        ActionSelector.DataSource = dataSource.ToList(); // Set the updated data source
        ActionSelector.DisplayMember = "Text";
        ActionSelector.ValueMember = "Value";
    }

    private void SetBackGroundColor(Color backGroundColor)
    {
        BackColor = DateTime.Now > Alarm?.Start ? Settings.Color.AlarmPastStartColor : backGroundColor;
        ForeColor = DetermineTextColor(backGroundColor);

        // Exclude buttons from text color adjustment
        foreach (Control control in Controls)
            if (control is Button)
                control.ForeColor = SystemColors.ControlText;
    }

    private void SnoozeButton_Click(object sender, EventArgs e)
    {
        if (Alarm == null) return;

        var selectedAction = (AlarmAction)(ActionSelector.SelectedValue ?? AlarmAction.FiveMinBefore);

        AlarmManager.ChangeAlarmState(Alarm, selectedAction);

        Close();
    }

    private void UpdateDropdown()
    {
        if ((Alarm?.Start - DateTime.Now)!.Value.TotalMinutes < TimeSpan.FromMinutes(15).TotalMinutes)
            RemoveActionSelectorItem(AlarmAction.FifteenMinBefore);

        if ((Alarm?.Start - DateTime.Now)!.Value.TotalMinutes < TimeSpan.FromMinutes(10).TotalMinutes)
            RemoveActionSelectorItem(AlarmAction.TenMinBefore);

        if ((Alarm?.Start - DateTime.Now)!.Value.TotalMinutes < TimeSpan.FromMinutes(5).TotalMinutes)
            RemoveActionSelectorItem(AlarmAction.FiveMinBefore);

        if (DateTime.Now > Alarm?.Start)
            RemoveActionSelectorItem(AlarmAction.ZeroMinBefore);
    }

    private void UpdateForm()
    {
        TimeLeft.Text = string.Format(Settings.Alarm.TimeLeftStringFormat, DateTime.Now - Alarm?.Start);

        if (_playSound && Settings.Audio.TurnOffAlarmAfterStart >= 0 && DateTime.Now - Alarm?.Start >
            TimeSpan.FromMinutes(Settings.Audio.TurnOffAlarmAfterStart))
        {
            MediaPlayerPlayer.StopSound();
            _playSound = false;
        }

        if (DateTime.Now > Alarm?.End)
        {
            AlarmManager.ChangeAlarmState(Alarm, AlarmAction.Dismiss);
            Close();
        }

        if (DateTime.Now > Alarm?.Start)
            SetBackGroundColor(Settings.Color.AlarmPastStartColor);

        UpdateDropdown();
    }
}