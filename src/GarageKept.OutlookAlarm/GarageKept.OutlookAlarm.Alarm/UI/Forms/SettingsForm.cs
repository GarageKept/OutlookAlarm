using System.Runtime.CompilerServices;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

public partial class SettingsForm : BaseForm, ISettingsForm
{
    private Timer? _slidingTimer;
    private bool _isExpanded;

    public SettingsForm() : base(false)
    {
        InitializeComponent();

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

        SliderSpeedNumericUpDown.Minimum = 1;
        SliderSpeedNumericUpDown.Maximum = 20;
        SliderSpeedNumericUpDown.Value = Program.AppSettings.Main.SliderSpeed;

        SliderSpeedTrackBar.Minimum = 1;
        SliderSpeedTrackBar.Maximum = 20;
        SliderSpeedTrackBar.Value = Program.AppSettings.Main.SliderSpeed;

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

        return base.ShowDialog();
    }

    #region Main

    #region Main.Left

    private void LeftNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (sender == LeftTrackBar) return;

        Program.AppSettings.Main.Left = (int)LeftNumericUpDown.Value;
        LeftTrackBar.Value = (int)LeftNumericUpDown.Value;

        if (Owner is IMainForm mainForm)
            mainForm.Left = Program.AppSettings.Main.Left;
    }

    private void LeftTrackBarScroll(object sender, EventArgs e)
    {
        Program.AppSettings.Main.Left = LeftTrackBar.Value;
        LeftNumericUpDown.Value = LeftTrackBar.Value;

        if (Owner is IMainForm mainForm)
            mainForm.Left = Program.AppSettings.Main.Left;
    }

    #endregion

    #region Main.BarSize

    private void BarHeightNumericUpDown_ValueChanged(object sender, EventArgs e)
    {

        if (Owner is IMainForm)
        {
            Program.AppSettings.Main.BarSize = (int)BarHeightNumericUpDown.Value;
            Owner.Top = (int)BarHeightNumericUpDown.Value - Owner.Height;
        }
    }

    private void BarHeightNumericUpDown_Enter(object sender, EventArgs e)
    {
        if (Owner is IMainForm)
        {
            Owner.Top = (int)BarHeightNumericUpDown.Value - Owner.Height;
        }
    }

    private void BarHeightNumericUpDown_Leave(object sender, EventArgs e)
    {
        if (Owner is IMainForm mainForm)
        {
            Owner.Top = 0;
        }
    }

    #endregion

    #region Main.SliderSpeed

    private void SliderSpeedNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        SliderSpeedTrackBar.Value = (int)SliderSpeedNumericUpDown.Value;
        Program.AppSettings.Main.SliderSpeed = SliderSpeedTrackBar.Value;
    }

    private void SliderSpeedTrackBar_ValueChanged(object sender, EventArgs e)
    {
        SliderSpeedNumericUpDown.Value = SliderSpeedTrackBar.Value;
        Program.AppSettings.Main.SliderSpeed = SliderSpeedTrackBar.Value;
    }

    private void SliderSpeedNumericUpDown_Enter(object sender, EventArgs e)
    {
        StartSliderPreview();
    }

    private void SliderSpeedNumericUpDown_Leave(object sender, EventArgs e)
    {
        StopSliderPreview();
    }

    private void SliderSpeedTrackBar_Enter(object sender, EventArgs e)
    {
        StartSliderPreview();
    }

    private void SliderSpeedTrackBar_Leave(object sender, EventArgs e)
    {
        StopSliderPreview();
    }

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

    private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopSliderPreview();

        if (Owner is not null) Owner.Top = 0;
    }
}