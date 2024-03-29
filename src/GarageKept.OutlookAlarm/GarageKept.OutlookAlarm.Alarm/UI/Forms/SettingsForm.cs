﻿using GarageKept.OutlookAlarm.Alarm.AlarmManager;
using GarageKept.OutlookAlarm.Alarm.Audio;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using GarageKept.OutlookAlarm.Alarm.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

public partial class SettingsForm : BaseForm, ISettingsForm
{
    private const string AppName = @"GarageKept.OutlookAlarm";
    private const string RunKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

    private readonly DateTime _exampleDateTime =
        new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);

    private readonly TimeSpan _exampleTimeSpan = new(0, 3, 2, 1);
    private readonly IMediaPlayer _mediaPlayer;
    private bool _isExpanded;
    private Timer? _slidingTimer;

    public SettingsForm(IMediaPlayer mediaPlayer, ISettings settings) : base(false)
    {
        Settings = settings;

        InitializeComponent();

        _mediaPlayer = mediaPlayer;

        // Show in center of screen
        Top = (ScreenHeight - Height) / 2;
        Left = (ScreenWidth - Width) / 2;
        BarHeightNumericUpDown.Value = Settings.Main.BarSize;
    }

    private ISettings Settings { get; }

    public new DialogResult ShowDialog()
    {
        #region Main

        #region Main.Left

        LeftNumericUpDown.Minimum = 0;
        LeftNumericUpDown.Maximum = ScreenWidth - Owner?.Width ?? 0;
        LeftNumericUpDown.Value = Settings.Main.Left;

        LeftTrackBar.Minimum = 0;
        LeftTrackBar.Maximum = (int)LeftNumericUpDown.Maximum;
        LeftTrackBar.Value = Settings.Main.Left;

        #endregion

        #region Main.BarHeight

        BarHeightNumericUpDown.Minimum = 1;
        BarHeightNumericUpDown.Maximum = 20;
        BarHeightNumericUpDown.Value = Settings.Main.BarSize;

        #endregion

        #region Main.SliderSpeed

        SliderSpeedNumericUpDown.ValueChanged -= SliderSpeedNumericUpDown_ValueChanged;
        SliderSpeedNumericUpDown.Minimum = 1;
        SliderSpeedNumericUpDown.Maximum = 20;
        SliderSpeedNumericUpDown.Value = Settings.Main.SliderSpeed;
        SliderSpeedNumericUpDown.ValueChanged += SliderSpeedNumericUpDown_ValueChanged;

        SliderSpeedTrackBar.ValueChanged -= SliderSpeedTrackBar_ValueChanged;
        SliderSpeedTrackBar.Minimum = 1;
        SliderSpeedTrackBar.Maximum = 20;
        SliderSpeedTrackBar.Value = Settings.Main.SliderSpeed;
        SliderSpeedTrackBar.ValueChanged += SliderSpeedTrackBar_ValueChanged;

        #endregion

        #region Main.MinimumWidth

        MinimumWidthNumericUpDown.Minimum = 0;
        MinimumWidthNumericUpDown.Maximum = ScreenWidth;
        MinimumWidthNumericUpDown.Value = Settings.Main.MinimumWidth;

        MinimumWidthTrackBar.Minimum = 0;
        MinimumWidthTrackBar.Maximum = ScreenWidth;
        MinimumWidthTrackBar.Value = Settings.Main.MinimumWidth;

        #endregion

        #endregion

        #region Alarm

        #region Alarm.AlarmWarning

        AlarmWarningTimeNumericUpDown.Minimum = 0;
        AlarmWarningTimeNumericUpDown.Maximum = 60;
        AlarmWarningTimeNumericUpDown.Value = Settings.Alarm.AlarmWarningTime;

        #endregion


        #region Alarm.TimeLeftStringFormat

        TimeLeftFormatExampleTextBox.Text = Settings.Alarm.TimeLeftStringFormat;

        #endregion

        #region Alarm.AlarmStartingStringFormat

        AlarmStartFormatTextBox.Text = Settings.Alarm.AlarmStartStringFormat;

        #endregion

        #region Alarm.Top

        AlarmLocationTopNumericUpDown.Minimum = 0;
        AlarmLocationTopNumericUpDown.Maximum = ScreenHeight - 96;
        AlarmLocationTopNumericUpDown.Value = Settings.Alarm.Top;

        #endregion

        #region Alarm.Left

        AlarmLocationLeftNumericUpDown.Minimum = -1;
        AlarmLocationLeftNumericUpDown.Maximum = ScreenWidth - 330;
        AlarmLocationLeftNumericUpDown.Value = Settings.Alarm.Left;

        #endregion

        #endregion

        #region AlarmSource

        #region AlarmSource.FetchIntervalInMinutes

        FetchIntervalNumericUpDown.Minimum = 1;
        FetchIntervalNumericUpDown.Maximum = 60;
        FetchIntervalNumericUpDown.Value = Settings.AlarmSource.FetchIntervalInMinutes;

        #endregion

        #region AlarmSource.FetchTimeInHours

        FetchTimeNumericUpDown.Minimum = 1;
        FetchTimeNumericUpDown.Maximum = 24;
        FetchTimeNumericUpDown.Value = Settings.AlarmSource.FetchTimeInHours;

        #endregion

        #region AlarmSource.Startup

        RunOnStartCheckBox.Checked = GetStartupValue();

        #endregion

        #endregion

        #region Audio

        #region Audio.DefaultSound

        DefaultSoundComboBox.SelectedIndexChanged -= DefaultSoundComboBox_SelectedIndexChanged;
        var dataSource = Enum.GetValues(typeof(SoundType)).Cast<SoundType>()
            .Select(s => new { Value = s, Text = AlarmActionHelpers.GetEnumDisplayValue(s) })
            .Where(a => a.Text != "Dismissed").ToList();
        DefaultSoundComboBox.DisplayMember = "Text";
        DefaultSoundComboBox.ValueMember = "Value";

        DefaultSoundComboBox.DataSource = dataSource;

        // Find the item that matches the Settings.Audio.DefaultSound value
        var defaultSound = dataSource.FirstOrDefault(item => item.Value == Settings.Audio.DefaultSound);

        // Set the found item as the selected item
        DefaultSoundComboBox.SelectedItem = defaultSound;

        DefaultSoundComboBox.SelectedIndexChanged -= DefaultSoundComboBox_SelectedIndexChanged;
        DefaultSoundComboBox.SelectedIndexChanged += DefaultSoundComboBox_SelectedIndexChanged;

        #endregion

        #region Audio.TurnOffAlarmAfterStart

        OffAfterStartNumericUpDown.Minimum = -1;
        OffAfterStartNumericUpDown.Maximum = 60;
        OffAfterStartNumericUpDown.Value = Settings.Audio.TurnOffAlarmAfterStart;

        #endregion

        #endregion

        #region Color

        #region Color.AlarmPastStartColor

        ColorAlarmPastStartLabel.BackColor = Settings.Color.AlarmPastStartColor;
        ColorAlarmPastStartLabel.ForeColor = DetermineTextColor(ColorAlarmPastStartLabel.BackColor);

        #endregion

        #region Color.GreenColor

        ColorGreenLabel.BackColor = Settings.Color.GreenColor;
        ColorGreenLabel.ForeColor = DetermineTextColor(ColorGreenLabel.BackColor);

        #endregion

        #region Color.YellowColor

        ColorYellowLabel.BackColor = Settings.Color.YellowColor;
        ColorYellowLabel.ForeColor = DetermineTextColor(ColorYellowLabel.BackColor);

        #endregion

        #region Color.RedColor

        ColorRedLabel.BackColor = Settings.Color.RedColor;
        ColorRedLabel.ForeColor = DetermineTextColor(ColorRedLabel.BackColor);

        #endregion

        #endregion

        #region Time

        #region Time.WorkingDays

        WorkdayStartTimePicker.Value = Settings.TimeManagement.WorkingStartTime;
        WorkdayEndTimePicker.Value = Settings.TimeManagement.WorkingEndTime;
        SundayCheckBox.Checked = Settings.TimeManagement.WorkDays.Any(w => w == DayOfWeek.Sunday);
        MondayCheckBox.Checked = Settings.TimeManagement.WorkDays.Any(w => w == DayOfWeek.Monday);
        TuesdayCheckBox.Checked = Settings.TimeManagement.WorkDays.Any(w => w == DayOfWeek.Tuesday);
        WednesdayCheckBox.Checked = Settings.TimeManagement.WorkDays.Any(w => w == DayOfWeek.Wednesday);
        ThursdayCheckBox.Checked = Settings.TimeManagement.WorkDays.Any(w => w == DayOfWeek.Thursday);
        FridayCheckBox.Checked = Settings.TimeManagement.WorkDays.Any(w => w == DayOfWeek.Friday);
        SaturdayCheckBox.Checked = Settings.TimeManagement.WorkDays.Any(w => w == DayOfWeek.Saturday);
        WorkdayEnabledCheckBox.Checked = Settings.TimeManagement.EnableOnlyWorkingPeriods;
        EnableWorkingDays(Settings.TimeManagement.EnableOnlyWorkingPeriods);

        #endregion

        #region Time.ExceptionCategories

        CategoryExceptionListBox.Items.Clear();
        var exceptionItems = Settings.TimeManagement.ExceptionCategories.ToArray<object>();
        CategoryExceptionListBox.Items.AddRange(exceptionItems);

        var exceptionToolStrip = new ContextMenuStrip();
        var exceptionRemove = new ToolStripMenuItem("Remove Selected");
        exceptionRemove.Click += ExceptionRemoveOnClick;
        exceptionToolStrip.Items.Add(exceptionRemove);
        CategoryExceptionListBox.ContextMenuStrip = exceptionToolStrip;

        #endregion

        #region Time.Holidays

        HolidayListView.View = View.Details;
        HolidayListView.Columns.Clear();
        HolidayListView.Columns.Add("Name");
        HolidayListView.Columns.Add("Date");
        HolidayListView.Columns.Add("Description");
        HolidayListView.Items.Clear();
        foreach (var holiday in Settings.TimeManagement.Holidays)
        {
            // Create a ListViewItem for the holiday
            var item = new ListViewItem(holiday.Name);

            // Add sub-items for each property to populate the columns
            item.SubItems.Add(holiday.Date.ToShortDateString());
            item.SubItems.Add(holiday.Description);
            item.SubItems.Add(holiday.Id.ToString());

            // Add the ListViewItem to the ListView
            HolidayListView.Items.Add(item);
        }

        HolidayListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        var holidayToolStrip = new ContextMenuStrip();
        var holidayRemove = new ToolStripMenuItem("Remove Selected");
        holidayRemove.Click += HolidayRemoveOnClick;
        holidayToolStrip.Items.Add(holidayRemove);

        var holidayEdit = new ToolStripMenuItem("Edit Selected");
        holidayEdit.Click += HolidayEdit_Click;
        holidayToolStrip.Items.Add(holidayEdit);

        HolidayListView.ContextMenuStrip = holidayToolStrip;

        #endregion

        #endregion

        return base.ShowDialog();
    }


    #region Main

    #region Main.Left

    private void LeftNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (sender == LeftTrackBar) return;

        LeftTrackBar.Value = (int)LeftNumericUpDown.Value;
        Settings.Main.Left = (int)LeftNumericUpDown.Value;

        if (Owner is IMainForm)
            Owner.Left = Settings.Main.Left;
    }

    private void LeftTrackBarScroll(object sender, EventArgs e)
    {
        LeftNumericUpDown.Value = LeftTrackBar.Value;
        Settings.Main.Left = LeftTrackBar.Value;

        if (Owner is IMainForm) Owner.Left = Settings.Main.Left;
    }

    #endregion

    #region Main.BarSize

    private void BarHeightNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (Owner is not IMainForm) return;

        Owner.Top = (int)BarHeightNumericUpDown.Value - Owner.Height;
        Settings.Main.BarSize = (int)BarHeightNumericUpDown.Value;
    }

    private void BarHeightNumericUpDown_Enter(object sender, EventArgs e)
    {
        if (Owner is IMainForm) Owner.Top = (int)BarHeightNumericUpDown.Value - Owner.Height;
    }

    private void BarHeightNumericUpDown_Leave(object sender, EventArgs e)
    {
        if (Owner is IMainForm) Owner.Top = 0;
    }

    #endregion

    #region Main.SliderSpeed

    private void SliderSpeedNumericUpDown_ValueChanged(object? sender, EventArgs e)
    {
        SliderSpeedTrackBar.Value = (int)SliderSpeedNumericUpDown.Value;
        Settings.Main.SliderSpeed = SliderSpeedTrackBar.Value;
    }

    private void SliderSpeedTrackBar_ValueChanged(object? sender, EventArgs e)
    {
        SliderSpeedNumericUpDown.Value = SliderSpeedTrackBar.Value;
        Settings.Main.SliderSpeed = SliderSpeedTrackBar.Value;
    }

    private void SliderSpeedNumericUpDown_Enter(object sender, EventArgs e) { StartSliderPreview(); }

    private void SliderSpeedNumericUpDown_Leave(object sender, EventArgs e) { StopSliderPreview(); }

    private void SliderSpeedTrackBar_Enter(object sender, EventArgs e) { StartSliderPreview(); }

    private void SliderSpeedTrackBar_Leave(object sender, EventArgs e) { StopSliderPreview(); }

    private void StartSliderPreview()
    {
        if (Owner is null) return;

        _isExpanded = Owner.Top >= 0;

        // Initialize and set up the sliding timer
        _slidingTimer = new Timer { Interval = 10 };
        _slidingTimer.Tick -= SlidingTimer_Tick;
        _slidingTimer.Tick += SlidingTimer_Tick;
        _slidingTimer.Enabled = true;
        _slidingTimer.Start();
    }

    private void StopSliderPreview()
    {
        _slidingTimer?.Stop();
        _slidingTimer?.Dispose();
        _slidingTimer = null;

        if (Owner is not null) Owner.Top = 0;
    }

    private void SlidingTimer_Tick(object? sender, EventArgs e)
    {
        if (Owner is null) return;

        var targetY = _isExpanded ? 0 : -Owner.Height + Settings.Main.BarSize;

        if (Math.Abs(Owner.Location.Y - targetY) <= 1)
        {
            Owner.Location = Owner.Location with { Y = targetY };
        }
        else
        {
            var step = (targetY - Owner.Location.Y) / Settings.Main.SliderSpeed;

            if (step == 0) step = targetY > Owner.Location.Y ? 1 : -1;

            Owner.Location = new Point(Owner.Location.X, Owner.Location.Y + step);
        }

        _isExpanded = targetY == Owner.Location.Y ? !_isExpanded : _isExpanded;
    }

    #endregion

    #region Main.MinimumWidth

    private void MinimumWidthNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        MinimumWidthTrackBar.Value = (int)MinimumWidthNumericUpDown.Value;
        Settings.Main.MinimumWidth = MinimumWidthTrackBar.Value;

        if (Owner is not null) Owner.Width = Settings.Main.MinimumWidth;
    }

    private void MinimumWidthTrackBar_ValueChanged(object sender, EventArgs e)
    {
        MinimumWidthNumericUpDown.Value = MinimumWidthTrackBar.Value;
        Settings.Main.MinimumWidth = MinimumWidthTrackBar.Value;

        if (Owner is not null) Owner.Width = Settings.Main.MinimumWidth;
    }

    #endregion

    #endregion

    #region Alarm

    #region Alarm.AlarmWarning

    private void AlarmWarningTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        Settings.Alarm.AlarmWarningTime = (int)AlarmWarningTimeNumericUpDown.Value;
    }

    #endregion

    #region Alarm.TimeLeftStringFormat

    private void TimeLeftFormatExampleTextBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TimeLeftFormatExampleLabel.Text = string.Format(TimeLeftFormatExampleTextBox.Text, _exampleTimeSpan);

            if (string.IsNullOrWhiteSpace(TimeLeftFormatExampleTextBox.Text)) return;

            Settings.Alarm.TimeLeftStringFormat = TimeLeftFormatExampleTextBox.Text;
        }
        catch
        {
            TimeLeftFormatExampleLabel.Text = @"--:--:--";
        }
    }

    #endregion

    #region Alarm.AlarmStartStringFormat

    private void AlarmStartFormatTextBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AlarmStartFormatExampleLabel.Text = _exampleDateTime.ToString(AlarmStartFormatTextBox.Text);

            if (string.IsNullOrWhiteSpace(AlarmStartFormatExampleLabel.Text)) return;

            Settings.Alarm.AlarmStartStringFormat = AlarmStartFormatTextBox.Text;
        }
        catch
        {
            AlarmStartFormatExampleLabel.Text = @"--:--:--";
        }
    }

    #endregion

    #region Alarm.Top

    private void AlarmLocationTopNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        Settings.Alarm.Top = (int)AlarmLocationTopNumericUpDown.Value;
    }

    #endregion

    #region Alarm.Left

    private void AlarmLocationLeftNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        Settings.Alarm.Left = (int)AlarmLocationLeftNumericUpDown.Value;
    }

    #endregion

    #endregion

    #region AlarmSource

    #region AlarmSource.FetchIntervalInMinutes

    private void FetchIntervalNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        Settings.AlarmSource.FetchIntervalInMinutes = (int)FetchIntervalNumericUpDown.Value;
    }

    #endregion

    #region AlarmSource.FetchTimeInHours

    private void FetchTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        Settings.AlarmSource.FetchTimeInHours = (int)FetchTimeNumericUpDown.Value;
    }

    #endregion

    #region AlarmSource.Startup

    private void RunOnStartCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        SetStartup(RunOnStartCheckBox.Checked);
    }

    private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopSliderPreview();

        if (Owner is not null) Owner.Top = 0;
    }

    private static void SetStartup(bool enable)
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, true);

        if (enable)
            key?.SetValue(AppName, Application.ExecutablePath);
        else
            key?.DeleteValue(AppName, false);
    }

    private static bool GetStartupValue()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, false);
        return key?.GetValue(AppName) != null;
    }

    #endregion

    #endregion

    #region Audio

    #region Audio.DefaultSound

    private void DefaultSoundComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        Settings.Audio.DefaultSound = GetDefaultSoundComboBoxSelectedSoundType();
    }

    private void PlayTestButton_Click(object sender, EventArgs e)
    {
        if (_mediaPlayer.IsPlaying)
        {
            _mediaPlayer.StopSound();
            PlayTestButton.Text = @"Test";
        }
        else
        {
            PlayTestButton.Text = @"Stop";
            _mediaPlayer.PlaySound(GetDefaultSoundComboBoxSelectedSoundType(), false,
                (_, _) => { PlayTestButton.Text = @"Test"; });
        }
    }

    private SoundType GetDefaultSoundComboBoxSelectedSoundType()
    {
        if (DefaultSoundComboBox.SelectedItem is not { } selectedItem) return SoundType.TickTock;

        var selectedType = selectedItem.GetType();
        var valueProperty = selectedType.GetProperty("Value");
        var selectedValue = valueProperty?.GetValue(selectedItem);

        if (selectedValue is null) return SoundType.TickTock;

        return (SoundType)selectedValue;
    }

    #endregion

    #region Audio.TurnOffAlarmAfterStart

    private void OffAfterStartNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        Settings.Audio.TurnOffAlarmAfterStart = (int)OffAfterStartNumericUpDown.Value;
    }

    #endregion

    #endregion

    #region Color

    #region Color.AlarmPastStartColor

    private void ColorAlarmPastStartLabel_Click(object sender, EventArgs e)
    {
        ColorAlarmPastStartLabel.BackColor = GetColor(ColorAlarmPastStartLabel.BackColor);
        ColorAlarmPastStartLabel.ForeColor = DetermineTextColor(ColorAlarmPastStartLabel.BackColor);

        if (ColorAlarmPastStartLabel.BackColor == Settings.Color.AlarmPastStartColor) return;

        Settings.Color.AlarmPastStartColor = ColorAlarmPastStartLabel.BackColor;
    }

    #endregion

    #region Color.GreenColor

    private void ColorGreenLabel_Click(object sender, EventArgs e)
    {
        ColorGreenLabel.BackColor = GetColor(ColorGreenLabel.BackColor);
        ColorGreenLabel.ForeColor = DetermineTextColor(ColorGreenLabel.BackColor);

        if (ColorGreenLabel.BackColor == Settings.Color.RedColor) return;

        Settings.Color.GreenColor = ColorGreenLabel.BackColor;
    }

    #endregion

    #region Color.YellowColor

    private void ColorYellowLabel_Click(object sender, EventArgs e)
    {
        ColorYellowLabel.BackColor = GetColor(ColorYellowLabel.BackColor);
        ColorYellowLabel.ForeColor = DetermineTextColor(ColorYellowLabel.BackColor);

        if (ColorYellowLabel.BackColor == Settings.Color.RedColor) return;

        Settings.Color.YellowColor = ColorYellowLabel.BackColor;
    }

    #endregion

    #region Color.RedColor

    private void RedColorExampleLabel_Click(object sender, EventArgs e)
    {
        ColorRedLabel.BackColor = GetColor(ColorRedLabel.BackColor);
        ColorRedLabel.ForeColor = DetermineTextColor(ColorRedLabel.BackColor);

        if (ColorRedLabel.BackColor == Settings.Color.RedColor) return;

        Settings.Color.RedColor = ColorRedLabel.BackColor;
    }

    #endregion

    private Color GetColor(Color originalColor)
    {
        var result = colorDialog.ShowDialog();

        return result == DialogResult.OK ? colorDialog.Color : originalColor;
    }

    #endregion

    #region Time

    #region Time.WorkingDays

    private void WorkdayEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.TimeManagement.EnableOnlyWorkingPeriods = WorkdayEnabledCheckBox.Checked;

        EnableWorkingDays(WorkdayEnabledCheckBox.Checked);
    }

    private void EnableWorkingDays(bool enabled)
    {
        WorkdayStartLabel.Enabled = enabled;
        WorkdayStartTimePicker.Enabled = enabled;
        WorkdayEndLabel.Enabled = enabled;
        WorkdayEndTimePicker.Enabled = enabled;
        SundayCheckBox.Enabled = enabled;
        MondayCheckBox.Enabled = enabled;
        TuesdayCheckBox.Enabled = enabled;
        WednesdayCheckBox.Enabled = enabled;
        ThursdayCheckBox.Enabled = enabled;
        FridayCheckBox.Enabled = enabled;
        SaturdayCheckBox.Enabled = enabled;
    }

    private void WorkdayStartTimePicker_ValueChanged(object sender, EventArgs e)
    {
        Settings.TimeManagement.WorkingStartTime = WorkdayStartTimePicker.Value;
    }

    private void WorkdayEndTimePicker_ValueChanged(object sender, EventArgs e)
    {
        Settings.TimeManagement.WorkingEndTime = WorkdayEndTimePicker.Value;
    }

    private void SaveDayOfWeekSetting(DayOfWeek dayOfWeek, bool enable)
    {
        if (enable)
        {
            var exists = Settings.TimeManagement.WorkDays.Any(w => w == dayOfWeek);

            if (!exists)
                Settings.TimeManagement.WorkDays.Add(dayOfWeek);
        }
        else if (Settings.TimeManagement.WorkDays.Contains(dayOfWeek))
        {
            var daysToRemove = Settings.TimeManagement.WorkDays.Where(d => d == dayOfWeek).ToList();

            foreach (var day in daysToRemove) Settings.TimeManagement.WorkDays.Remove(day);
        }
    }

    private void SundayCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        SaveDayOfWeekSetting(DayOfWeek.Sunday, SundayCheckBox.Checked);
    }

    private void MondayCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        SaveDayOfWeekSetting(DayOfWeek.Monday, MondayCheckBox.Checked);
    }

    private void TuesdayCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        SaveDayOfWeekSetting(DayOfWeek.Tuesday, TuesdayCheckBox.Checked);
    }

    private void WednesdayCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        SaveDayOfWeekSetting(DayOfWeek.Wednesday, WednesdayCheckBox.Checked);
    }

    private void ThursdayCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        SaveDayOfWeekSetting(DayOfWeek.Thursday, ThursdayCheckBox.Checked);
    }

    private void FridayCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        SaveDayOfWeekSetting(DayOfWeek.Friday, FridayCheckBox.Checked);
    }

    private void SaturdayCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        SaveDayOfWeekSetting(DayOfWeek.Saturday, SaturdayCheckBox.Checked);
    }

    #endregion

    #region Time.ExceptionCategories

    private void CategoryExceptionAddButton_Click(object sender, EventArgs e)
    {
        var category = CategoryExceptionTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(category)) return;

        if (CategoryExceptionListBox.Items.Contains(category)) return;

        // Add the category to the ExceptionCategories collection if it doesn't already exist
        if (!Settings.TimeManagement.ExceptionCategories.Contains(category))
            Settings.TimeManagement.ExceptionCategories.Add(category);

        CategoryExceptionListBox.Items.Clear();
        CategoryExceptionListBox.Items.AddRange(Settings.TimeManagement.ExceptionCategories.ToArray<object>());

        CategoryExceptionTextBox.Text = string.Empty;
    }

    private void ExceptionRemoveOnClick(object? sender, EventArgs e)
    {
        var category = CategoryExceptionListBox.SelectedItem?.ToString();

        if (category is null) return;

        var exceptions = Settings.TimeManagement.ExceptionCategories.Where(exc => exc == category).ToList();

        foreach (var cat in exceptions) Settings.TimeManagement.ExceptionCategories.Remove(cat);

        CategoryExceptionListBox.Items.Clear();
        CategoryExceptionListBox.Items.AddRange(Settings.TimeManagement.ExceptionCategories.ToArray<object>());
    }

    #endregion

    #region Time.Holidays

    private void HolidayRemoveOnClick(object? sender, EventArgs e)
    {
        if (HolidayListView.SelectedItems.Count <= 0) return;

        // Access and handle the selected items
        foreach (ListViewItem selectedItem in HolidayListView.SelectedItems)
        {
            var idText = selectedItem.SubItems[3].Text;
            var id = Guid.Parse(idText);
            var itemsToRemove = Settings.TimeManagement.Holidays.Where(h => h.Id == id).ToList();

            foreach (var item in itemsToRemove) Settings.TimeManagement.Holidays.Remove(item);

            // Handle the selected item(s)
            HolidayListView.Items.Remove(selectedItem);
        }
    }

    private void HolidayEdit_Click(object? sender, EventArgs e)
    {
        var holidayItem = HolidayListView.SelectedItems[0];

        // Retrieve the values of each sub-item
        var name = holidayItem.SubItems[0].Text;
        var dateText = holidayItem.SubItems[1].Text;
        var description = holidayItem.SubItems[2].Text;
        var idText = holidayItem.SubItems[3].Text;

        var date = DateTime.Parse(dateText);
        var id = Guid.Parse(idText);

        var holiday = new Holiday(name, date, description) { Id = id };

        var editor = Program.ServiceProvider?.GetService<IHolidayEditor>();

        if (editor == null) return;

        editor.Holiday = holiday;
        var result = editor.ShowDialog();

        if (result != DialogResult.OK) return;

        var remove = Settings.TimeManagement.Holidays.Where(h => h.Id == id).ToList();
        foreach (var h in remove) Settings.TimeManagement.Holidays.Remove(h);

        Settings.TimeManagement.Holidays.Add(editor.Holiday);

        holidayItem.Name = editor.Holiday.Name;
        holidayItem.SubItems[0].Text = editor.Holiday.Name;
        holidayItem.SubItems[1].Text = editor.Holiday.Date.ToShortDateString();
        holidayItem.SubItems[2].Text = editor.Holiday.Description;
        holidayItem.SubItems[3].Text = editor.Holiday.Id.ToString();

        HolidayListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    }

    private void HolidayAddButton_Click(object? sender, EventArgs e)
    {
        var editor = Program.ServiceProvider?.GetService<IHolidayEditor>();

        if (editor == null) return;

        var result = editor.ShowDialog();

        if (result != DialogResult.OK) return;

        Settings.TimeManagement.Holidays.Add(editor.Holiday);
        var item = new ListViewItem(editor.Holiday.Name);

        // Add sub-items for each property to populate the columns
        item.SubItems.Add(editor.Holiday.Date.ToShortDateString());
        item.SubItems.Add(editor.Holiday.Description);
        item.SubItems.Add(editor.Holiday.Id.ToString());
        HolidayListView.Items.Add(item);

        HolidayListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    }

    #endregion

    #endregion
}