using GarageKept.OutlookAlarm.Alarm.AlarmManager;
using GarageKept.OutlookAlarm.Alarm.Audio;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

public partial class SettingsForm : BaseForm, ISettingsForm
{
    private readonly DateTime _exampleDateTime = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);

    private readonly TimeSpan _exampleTimeSpan = new(0, 3, 2, 1);
    private readonly IMediaPlayer _mediaPlayer;
    private bool _isExpanded;
    private Timer? _slidingTimer;

    public SettingsForm(IMediaPlayer mediaPlayer) : base(false)
    {
        InitializeComponent();

        _mediaPlayer = mediaPlayer;

        // Show in center of screen
        Top = (ScreenHeight - Height) / 2;
        Left = (ScreenWidth - Width) / 2;
        BarHeightNumericUpDown.Value = Program.AppSettings.Main.BarSize;
    }

    public new DialogResult ShowDialog()
    {
        #region Main

        #region Main.Left

        LeftNumericUpDown.Minimum = 0;
        LeftNumericUpDown.Maximum = ScreenWidth - Owner?.Width ?? 0;
        LeftNumericUpDown.Value = Program.AppSettings.Main.Left;

        LeftTrackBar.Minimum = 0;
        LeftTrackBar.Maximum = (int)LeftNumericUpDown.Maximum;
        LeftTrackBar.Value = Program.AppSettings.Main.Left;

        #endregion

        #region Main.BarHeight

        BarHeightNumericUpDown.Minimum = 1;
        BarHeightNumericUpDown.Maximum = 20;
        BarHeightNumericUpDown.Value = Program.AppSettings.Main.BarSize;

        #endregion

        #region Main.SliderSpeed

        SliderSpeedNumericUpDown.ValueChanged -= SliderSpeedNumericUpDown_ValueChanged;
        SliderSpeedNumericUpDown.Minimum = 1;
        SliderSpeedNumericUpDown.Maximum = 20;
        SliderSpeedNumericUpDown.Value = Program.AppSettings.Main.SliderSpeed;
        SliderSpeedNumericUpDown.ValueChanged += SliderSpeedNumericUpDown_ValueChanged;

        SliderSpeedTrackBar.ValueChanged -= SliderSpeedTrackBar_ValueChanged;
        SliderSpeedTrackBar.Minimum = 1;
        SliderSpeedTrackBar.Maximum = 20;
        SliderSpeedTrackBar.Value = Program.AppSettings.Main.SliderSpeed;
        SliderSpeedTrackBar.ValueChanged += SliderSpeedTrackBar_ValueChanged;

        #endregion

        #region Main.MinimumWidth

        MinimumWidthNumericUpDown.Minimum = 0;
        MinimumWidthNumericUpDown.Maximum = ScreenWidth;
        MinimumWidthNumericUpDown.Value = Program.AppSettings.Main.MinimumWidth;

        MinimumWidthTrackBar.Minimum = 0;
        MinimumWidthTrackBar.Maximum = ScreenWidth;
        MinimumWidthTrackBar.Value = Program.AppSettings.Main.MinimumWidth;

        #endregion

        #endregion

        #region Alarm

        #region Alarm.AlarmWarning

        AlarmWarningTimeNumericUpDown.Minimum = 0;
        AlarmWarningTimeNumericUpDown.Maximum = 60;
        AlarmWarningTimeNumericUpDown.Value = Program.AppSettings.Alarm.AlarmWarningTime;

        #endregion


        #region Alarm.TimeLeftStringFormat

        TimeLeftFormatExampleTextBox.Text = Program.AppSettings.Alarm.TimeLeftStringFormat;

        #endregion

        #region Alarm.AlarmStartingStringFormat

        AlarmStartFormatTextBox.Text = Program.AppSettings.Alarm.AlarmStartStringFormat;

        #endregion

        #region Alarm.Top

        AlarmLocationTopNumericUpDown.Minimum = 0;
        AlarmLocationTopNumericUpDown.Maximum = ScreenHeight - 96;
        AlarmLocationTopNumericUpDown.Value = Program.AppSettings.Alarm.Top;

        #endregion

        #region Alarm.Left

        AlarmLocationLeftNumericUpDown.Minimum = -1;
        AlarmLocationLeftNumericUpDown.Maximum = ScreenWidth - 330;
        AlarmLocationLeftNumericUpDown.Value = Program.AppSettings.Alarm.Left;

        #endregion

        #endregion

        #region AlarmSource

        #region AlarmSource.FetchIntervalInMinutes

        FetchIntervalNumericUpDown.Minimum = 1;
        FetchIntervalNumericUpDown.Maximum = 60;
        FetchIntervalNumericUpDown.Value = Program.AppSettings.AlarmSource.FetchIntervalInMinutes;

        #endregion

        #region AlarmSource.FetchTimeInHours

        FetchTimeNumericUpDown.Minimum = 1;
        FetchTimeNumericUpDown.Maximum = 24;
        FetchTimeNumericUpDown.Value = Program.AppSettings.AlarmSource.FetchTimeInHours;

        #endregion

        #endregion

        #region Audio

        #region Audio.DefaultSound

        DefaultSoundComboBox.SelectedIndexChanged -= DefaultSoundComboBox_SelectedIndexChanged;
        var dataSource = Enum.GetValues(typeof(SoundType)).Cast<SoundType>().Select(s => new { Value = s, Text = AlarmActionHelpers.GetEnumDisplayValue(s) }).Where(a => a.Text != "Dismissed").ToList();
        DefaultSoundComboBox.DisplayMember = "Text";
        DefaultSoundComboBox.ValueMember = "Value";

        DefaultSoundComboBox.DataSource = dataSource;

        // Find the item that matches the Program.AppSettings.Audio.DefaultSound value
        var defaultSound = dataSource.FirstOrDefault(item => item.Value == Program.AppSettings.Audio.DefaultSound);

        // Set the found item as the selected item
        DefaultSoundComboBox.SelectedItem = defaultSound;

        DefaultSoundComboBox.SelectedIndexChanged += DefaultSoundComboBox_SelectedIndexChanged;

        #endregion

        #region Audio.TurnOffAlarmAfterStart

        OffAfterStartNumericUpDown.Minimum = -1;
        OffAfterStartNumericUpDown.Maximum = 60;
        OffAfterStartNumericUpDown.Value = Program.AppSettings.Audio.TurnOffAlarmAfterStart;

        #endregion

        #endregion

        #region Color

        #region Color.AlarmPastStartColor

        ColorAlarmPastStartLabel.BackColor = Program.AppSettings.Color.AlarmPastStartColor;
        ColorAlarmPastStartLabel.ForeColor = DetermineTextColor(ColorAlarmPastStartLabel.BackColor);

        #endregion

        #region Color.GreenColor

        ColorGreenLabel.BackColor = Program.AppSettings.Color.GreenColor;
        ColorGreenLabel.ForeColor = DetermineTextColor(ColorGreenLabel.BackColor);

        #endregion

        #region Color.YellowColor

        ColorYellowLabel.BackColor = Program.AppSettings.Color.YellowColor;
        ColorYellowLabel.ForeColor = DetermineTextColor(ColorYellowLabel.BackColor);

        #endregion

        #region Color.RedColor

        ColorRedLabel.BackColor = Program.AppSettings.Color.RedColor;
        ColorRedLabel.ForeColor = DetermineTextColor(ColorRedLabel.BackColor);

        #endregion

        #endregion

        return base.ShowDialog();
    }

    private Color GetColor(Color originalColor)
    {
        var result = colorDialog.ShowDialog();

        return result == DialogResult.OK ? colorDialog.Color : originalColor;
    }

    private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopSliderPreview();

        if (Owner is not null) Owner.Top = 0;
    }

    #region Main

    #region Main.Left

    private void LeftNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (sender == LeftTrackBar) return;

        LeftTrackBar.Value = (int)LeftNumericUpDown.Value;
        Program.AppSettings.Main.Left = (int)LeftNumericUpDown.Value;

        if (Owner is IMainForm mainForm)
            mainForm.Left = Program.AppSettings.Main.Left;
    }

    private void LeftTrackBarScroll(object sender, EventArgs e)
    {
        LeftNumericUpDown.Value = LeftTrackBar.Value;
        Program.AppSettings.Main.Left = LeftTrackBar.Value;

        if (Owner is IMainForm mainForm)
            mainForm.Left = Program.AppSettings.Main.Left;
    }

    #endregion

    #region Main.BarSize

    private void BarHeightNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (Owner is not IMainForm) return;

        Owner.Top = (int)BarHeightNumericUpDown.Value - Owner.Height;
        Program.AppSettings.Main.BarSize = (int)BarHeightNumericUpDown.Value;
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
        Program.AppSettings.Main.SliderSpeed = SliderSpeedTrackBar.Value;
    }

    private void SliderSpeedTrackBar_ValueChanged(object? sender, EventArgs e)
    {
        SliderSpeedNumericUpDown.Value = SliderSpeedTrackBar.Value;
        Program.AppSettings.Main.SliderSpeed = SliderSpeedTrackBar.Value;
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

        var targetY = _isExpanded ? 0 : -Owner.Height + Program.AppSettings.Main.BarSize;

        if (Math.Abs(Owner.Location.Y - targetY) <= 1)
        {
            Owner.Location = Owner.Location with { Y = targetY };
        }
        else
        {
            var step = (targetY - Owner.Location.Y) / Program.AppSettings.Main.SliderSpeed;

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
        Program.AppSettings.Main.MinimumWidth = MinimumWidthTrackBar.Value;

        if (Owner is not null) Owner.Width = Program.AppSettings.Main.MinimumWidth;
    }

    private void MinimumWidthTrackBar_ValueChanged(object sender, EventArgs e)
    {
        MinimumWidthNumericUpDown.Value = MinimumWidthTrackBar.Value;
        Program.AppSettings.Main.MinimumWidth = MinimumWidthTrackBar.Value;

        if (Owner is not null) Owner.Width = Program.AppSettings.Main.MinimumWidth;
    }

    #endregion

    #endregion

    #region Alarm

    #region Alarm.AlarmWarning

    private void AlarmWarningTimeNumericUpDown_ValueChanged(object sender, EventArgs e) { Program.AppSettings.Alarm.AlarmWarningTime = (int)AlarmWarningTimeNumericUpDown.Value; }

    #endregion

    #region Alarm.TimeLeftStringFormat

    private void TimeLeftFormatExampleTextBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TimeLeftFormatExampleLabel.Text = string.Format(TimeLeftFormatExampleTextBox.Text, _exampleTimeSpan);

            if (string.IsNullOrWhiteSpace(TimeLeftFormatExampleTextBox.Text)) return;

            Program.AppSettings.Alarm.TimeLeftStringFormat = TimeLeftFormatExampleTextBox.Text;
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

            Program.AppSettings.Alarm.AlarmStartStringFormat = AlarmStartFormatTextBox.Text;
        }
        catch
        {
            AlarmStartFormatExampleLabel.Text = @"--:--:--";
        }
    }

    #endregion

    #region Alarm.Top

    private void AlarmLocationTopNumericUpDown_ValueChanged(object sender, EventArgs e) { Program.AppSettings.Alarm.Top = (int)AlarmLocationTopNumericUpDown.Value; }

    #endregion

    #region Alarm.Left

    private void AlarmLocationLeftNumericUpDown_ValueChanged(object sender, EventArgs e) { Program.AppSettings.Alarm.Left = (int)AlarmLocationLeftNumericUpDown.Value; }

    #endregion

    #endregion

    #region AlarmSource

    #region AlarmSource.FetchIntervalInMinutes

    private void FetchIntervalNumericUpDown_ValueChanged(object sender, EventArgs e) { Program.AppSettings.AlarmSource.FetchIntervalInMinutes = (int)FetchIntervalNumericUpDown.Value; }

    #endregion

    #region AlarmSource.FetchTimeInHours

    private void FetchTimeNumericUpDown_ValueChanged(object sender, EventArgs e) { Program.AppSettings.AlarmSource.FetchTimeInHours = (int)FetchTimeNumericUpDown.Value; }

    #endregion

    #endregion

    #region Audio

    #region Audio.DefaultSound

    private void DefaultSoundComboBox_SelectedIndexChanged(object? sender, EventArgs e) { Program.AppSettings.Audio.DefaultSound = GetDefaultSoundComboBoxSelectedSoundType(); }

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
            _mediaPlayer.PlaySound(GetDefaultSoundComboBoxSelectedSoundType(), false, (_, _) => { PlayTestButton.Text = @"Test"; });
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

    private void OffAfterStartNumericUpDown_ValueChanged(object sender, EventArgs e) { Program.AppSettings.Audio.TurnOffAlarmAfterStart = (int)OffAfterStartNumericUpDown.Value; }

    #endregion

    #endregion

    #region Color

    #region Color.AlarmPastStartColor

    private void ColorAlarmPastStartLabel_Click(object sender, EventArgs e)
    {
        ColorAlarmPastStartLabel.BackColor = GetColor(ColorAlarmPastStartLabel.BackColor);
        ColorAlarmPastStartLabel.ForeColor = DetermineTextColor(ColorAlarmPastStartLabel.BackColor);

        if (ColorAlarmPastStartLabel.BackColor == Program.AppSettings.Color.AlarmPastStartColor) return;

        Program.AppSettings.Color.AlarmPastStartColor = ColorAlarmPastStartLabel.BackColor;
    }

    #endregion

    #region Color.GreenColor

    private void ColorGreenLabel_Click(object sender, EventArgs e)
    {
        ColorGreenLabel.BackColor = GetColor(ColorGreenLabel.BackColor);
        ColorGreenLabel.ForeColor = DetermineTextColor(ColorGreenLabel.BackColor);

        if (ColorGreenLabel.BackColor == Program.AppSettings.Color.RedColor) return;

        Program.AppSettings.Color.GreenColor = ColorGreenLabel.BackColor;
    }

    #endregion

    #region Color.YellowColor

    private void ColorYellowLabel_Click(object sender, EventArgs e)
    {
        ColorYellowLabel.BackColor = GetColor(ColorYellowLabel.BackColor);
        ColorYellowLabel.ForeColor = DetermineTextColor(ColorYellowLabel.BackColor);

        if (ColorYellowLabel.BackColor == Program.AppSettings.Color.RedColor) return;

        Program.AppSettings.Color.YellowColor = ColorYellowLabel.BackColor;
    }

    #endregion

    #region Color.RedColor

    private void RedColorExampleLabel_Click(object sender, EventArgs e)
    {
        ColorRedLabel.BackColor = GetColor(ColorRedLabel.BackColor);
        ColorRedLabel.ForeColor = DetermineTextColor(ColorRedLabel.BackColor);

        if (ColorRedLabel.BackColor == Program.AppSettings.Color.RedColor) return;

        Program.AppSettings.Color.RedColor = ColorRedLabel.BackColor;
    }

    #endregion

    #endregion
}