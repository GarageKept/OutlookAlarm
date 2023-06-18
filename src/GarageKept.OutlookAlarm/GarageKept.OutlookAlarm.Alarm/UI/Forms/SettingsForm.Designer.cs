using GarageKept.OutlookAlarm.Alarm.UI.Controls;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            settingsTabControl = new TabControl();
            mainPage = new TabPage();
            MinimumWidthNumericUpDown = new NumericUpDown();
            MinimumWidthTrackBar = new TrackBar();
            MinimumWidthLabel = new Label();
            TimeLeftLabel = new Label();
            SliderSpeedNumericUpDown = new NumericUpDown();
            LeftNumericUpDown = new NumericUpDown();
            SliderSpeedTrackBar = new TrackBar();
            SliderSpeedLabel = new Label();
            horizontalBarLabel = new Label();
            SystemBarHeightLabel = new Label();
            BarHeightNumericUpDown = new NumericUpDown();
            LeftTrackBar = new TrackBar();
            locationLabel = new Label();
            alarmPage = new TabPage();
            todoPage = new TabPage();
            colorPage = new TabPage();
            audioPage = new TabPage();
            settingsTabControl.SuspendLayout();
            mainPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MinimumWidthNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinimumWidthTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SliderSpeedNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LeftNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SliderSpeedTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BarHeightNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LeftTrackBar).BeginInit();
            SuspendLayout();
            // 
            // settingsTabControl
            // 
            settingsTabControl.Controls.Add(mainPage);
            settingsTabControl.Controls.Add(alarmPage);
            settingsTabControl.Controls.Add(todoPage);
            settingsTabControl.Controls.Add(colorPage);
            settingsTabControl.Controls.Add(audioPage);
            settingsTabControl.Dock = DockStyle.Fill;
            settingsTabControl.Location = new Point(0, 0);
            settingsTabControl.Name = "settingsTabControl";
            settingsTabControl.SelectedIndex = 0;
            settingsTabControl.Size = new Size(700, 351);
            settingsTabControl.TabIndex = 0;
            // 
            // mainPage
            // 
            mainPage.Controls.Add(MinimumWidthNumericUpDown);
            mainPage.Controls.Add(MinimumWidthTrackBar);
            mainPage.Controls.Add(MinimumWidthLabel);
            mainPage.Controls.Add(TimeLeftLabel);
            mainPage.Controls.Add(SliderSpeedNumericUpDown);
            mainPage.Controls.Add(LeftNumericUpDown);
            mainPage.Controls.Add(SliderSpeedTrackBar);
            mainPage.Controls.Add(SliderSpeedLabel);
            mainPage.Controls.Add(horizontalBarLabel);
            mainPage.Controls.Add(SystemBarHeightLabel);
            mainPage.Controls.Add(BarHeightNumericUpDown);
            mainPage.Controls.Add(LeftTrackBar);
            mainPage.Controls.Add(locationLabel);
            mainPage.Location = new Point(4, 24);
            mainPage.Name = "mainPage";
            mainPage.Padding = new Padding(3);
            mainPage.Size = new Size(692, 323);
            mainPage.TabIndex = 0;
            mainPage.Text = "Main";
            // 
            // MinimumWidthNumericUpDown
            // 
            MinimumWidthNumericUpDown.Location = new Point(194, 249);
            MinimumWidthNumericUpDown.Name = "MinimumWidthNumericUpDown";
            MinimumWidthNumericUpDown.Size = new Size(80, 23);
            MinimumWidthNumericUpDown.TabIndex = 19;
            MinimumWidthNumericUpDown.ValueChanged += MinimumWidthNumericUpDown_ValueChanged;
            // 
            // MinimumWidthTrackBar
            // 
            MinimumWidthTrackBar.Location = new Point(8, 278);
            MinimumWidthTrackBar.Maximum = 5120;
            MinimumWidthTrackBar.Name = "MinimumWidthTrackBar";
            MinimumWidthTrackBar.Size = new Size(266, 45);
            MinimumWidthTrackBar.TabIndex = 18;
            MinimumWidthTrackBar.ValueChanged += MinimumWidthTrackBar_ValueChanged;
            // 
            // MinimumWidthLabel
            // 
            MinimumWidthLabel.AutoSize = true;
            MinimumWidthLabel.Location = new Point(8, 251);
            MinimumWidthLabel.Name = "MinimumWidthLabel";
            MinimumWidthLabel.Size = new Size(142, 15);
            MinimumWidthLabel.TabIndex = 17;
            MinimumWidthLabel.Text = "Window Minimum Width";
            // 
            // TimeLeftLabel
            // 
            TimeLeftLabel.AutoSize = true;
            TimeLeftLabel.Location = new Point(8, 201);
            TimeLeftLabel.Name = "TimeLeftLabel";
            TimeLeftLabel.Size = new Size(100, 15);
            TimeLeftLabel.TabIndex = 12;
            TimeLeftLabel.Text = "Time Left Format:";
            // 
            // SliderSpeedNumericUpDown
            // 
            SliderSpeedNumericUpDown.Location = new Point(194, 117);
            SliderSpeedNumericUpDown.Name = "SliderSpeedNumericUpDown";
            SliderSpeedNumericUpDown.Size = new Size(80, 23);
            SliderSpeedNumericUpDown.TabIndex = 11;
            SliderSpeedNumericUpDown.ValueChanged += SliderSpeedNumericUpDown_ValueChanged;
            SliderSpeedNumericUpDown.Enter += SliderSpeedNumericUpDown_Enter;
            SliderSpeedNumericUpDown.Leave += SliderSpeedNumericUpDown_Leave;
            // 
            // LeftNumericUpDown
            // 
            LeftNumericUpDown.ImeMode = ImeMode.NoControl;
            LeftNumericUpDown.Location = new Point(194, 6);
            LeftNumericUpDown.Name = "LeftNumericUpDown";
            LeftNumericUpDown.Size = new Size(80, 23);
            LeftNumericUpDown.TabIndex = 10;
            LeftNumericUpDown.ValueChanged += LeftNumericUpDown_ValueChanged;
            // 
            // SliderSpeedTrackBar
            // 
            SliderSpeedTrackBar.Location = new Point(8, 146);
            SliderSpeedTrackBar.Maximum = 5120;
            SliderSpeedTrackBar.Name = "SliderSpeedTrackBar";
            SliderSpeedTrackBar.Size = new Size(266, 45);
            SliderSpeedTrackBar.TabIndex = 8;
            SliderSpeedTrackBar.ValueChanged += SliderSpeedTrackBar_ValueChanged;
            SliderSpeedTrackBar.Enter += SliderSpeedTrackBar_Enter;
            SliderSpeedTrackBar.Leave += SliderSpeedTrackBar_Leave;
            // 
            // SliderSpeedLabel
            // 
            SliderSpeedLabel.AutoSize = true;
            SliderSpeedLabel.Location = new Point(8, 119);
            SliderSpeedLabel.Name = "SliderSpeedLabel";
            SliderSpeedLabel.Size = new Size(71, 15);
            SliderSpeedLabel.TabIndex = 7;
            SliderSpeedLabel.Text = "Slider Speed";
            // 
            // horizontalBarLabel
            // 
            horizontalBarLabel.BorderStyle = BorderStyle.FixedSingle;
            horizontalBarLabel.Location = new Point(6, 83);
            horizontalBarLabel.Name = "horizontalBarLabel";
            horizontalBarLabel.Size = new Size(268, 1);
            horizontalBarLabel.TabIndex = 5;
            // 
            // SystemBarHeightLabel
            // 
            SystemBarHeightLabel.AutoSize = true;
            SystemBarHeightLabel.Location = new Point(8, 89);
            SystemBarHeightLabel.Name = "SystemBarHeightLabel";
            SystemBarHeightLabel.Size = new Size(104, 15);
            SystemBarHeightLabel.TabIndex = 4;
            SystemBarHeightLabel.Text = "System Bar Height";
            // 
            // BarHeightNumericUpDown
            // 
            BarHeightNumericUpDown.Location = new Point(194, 87);
            BarHeightNumericUpDown.Name = "BarHeightNumericUpDown";
            BarHeightNumericUpDown.Size = new Size(80, 23);
            BarHeightNumericUpDown.TabIndex = 3;
            BarHeightNumericUpDown.ValueChanged += BarHeightNumericUpDown_ValueChanged;
            BarHeightNumericUpDown.Enter += BarHeightNumericUpDown_Enter;
            BarHeightNumericUpDown.Leave += BarHeightNumericUpDown_Leave;
            // 
            // LeftTrackBar
            // 
            LeftTrackBar.Location = new Point(8, 35);
            LeftTrackBar.Maximum = 5120;
            LeftTrackBar.Name = "LeftTrackBar";
            LeftTrackBar.Size = new Size(266, 45);
            LeftTrackBar.TabIndex = 1;
            LeftTrackBar.Scroll += LeftTrackBarScroll;
            // 
            // locationLabel
            // 
            locationLabel.AutoSize = true;
            locationLabel.Location = new Point(8, 9);
            locationLabel.Name = "locationLabel";
            locationLabel.Size = new Size(80, 15);
            locationLabel.TabIndex = 0;
            locationLabel.Text = "Start Location";
            // 
            // alarmPage
            // 
            alarmPage.Location = new Point(4, 24);
            alarmPage.Name = "alarmPage";
            alarmPage.Padding = new Padding(3);
            alarmPage.Size = new Size(692, 323);
            alarmPage.TabIndex = 1;
            alarmPage.Text = "Alarm";
            alarmPage.UseVisualStyleBackColor = true;
            // 
            // todoPage
            // 
            todoPage.Location = new Point(4, 24);
            todoPage.Name = "todoPage";
            todoPage.Padding = new Padding(3);
            todoPage.Size = new Size(1063, 545);
            todoPage.TabIndex = 2;
            todoPage.Text = "Alarm Source";
            todoPage.UseVisualStyleBackColor = true;
            // 
            // colorPage
            // 
            colorPage.Location = new Point(4, 24);
            colorPage.Name = "colorPage";
            colorPage.Padding = new Padding(3);
            colorPage.Size = new Size(1063, 545);
            colorPage.TabIndex = 3;
            colorPage.Text = "Colors";
            colorPage.UseVisualStyleBackColor = true;
            // 
            // audioPage
            // 
            audioPage.Location = new Point(4, 24);
            audioPage.Name = "audioPage";
            audioPage.Padding = new Padding(3);
            audioPage.Size = new Size(1063, 545);
            audioPage.TabIndex = 4;
            audioPage.Text = "Audio";
            audioPage.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 351);
            Controls.Add(settingsTabControl);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.Manual;
            Text = "Outlook Alarm Settings";
            FormClosing += SettingsForm_FormClosing;
            settingsTabControl.ResumeLayout(false);
            mainPage.ResumeLayout(false);
            mainPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MinimumWidthNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinimumWidthTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)SliderSpeedNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)LeftNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)SliderSpeedTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)BarHeightNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)LeftTrackBar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl settingsTabControl;
        private TabPage mainPage;
        private TabPage alarmPage;
        private TabPage todoPage;
        private TabPage colorPage;
        private TabPage audioPage;
        private TrackBar LeftTrackBar;
        private Label locationLabel;
        private NumericUpDown BarHeightNumericUpDown;
        private Label SystemBarHeightLabel;
        private Label horizontalBarLabel;
        private TrackBar SliderSpeedTrackBar;
        private Label SliderSpeedLabel;
        private NumericUpDown SliderSpeedNumericUpDown;
        private NumericUpDown LeftNumericUpDown;
        private Label TimeLeftLabel;
        private NumericUpDown MinimumWidthNumericUpDown;
        private TrackBar MinimumWidthTrackBar;
        private Label MinimumWidthLabel;
    }
}