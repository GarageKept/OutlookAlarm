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
            MainGroupBox = new GroupBox();
            MinimumWidthNumericUpDown = new NumericUpDown();
            MinimumWidthLabel = new Label();
            MinimumWidthTrackBar = new TrackBar();
            SliderGroupBox = new GroupBox();
            BarHeightNumericUpDown = new NumericUpDown();
            SystemBarHeightLabel = new Label();
            SliderSpeedNumericUpDown = new NumericUpDown();
            SliderSpeedLabel = new Label();
            SliderSpeedTrackBar = new TrackBar();
            SizeGroupBox = new GroupBox();
            LeftNumericUpDown = new NumericUpDown();
            locationLabel = new Label();
            LeftTrackBar = new TrackBar();
            alarmPage = new TabPage();
            AlarmLocationGroupBox = new GroupBox();
            AlarmLocationLeftLabel = new Label();
            AlarmLocationLeftNumericUpDown = new NumericUpDown();
            AlarmLocationTopNumericUpDown = new NumericUpDown();
            AlarmLocationTopLabel = new Label();
            StringFormatGroupBox = new GroupBox();
            AlarmStartFormatLabel = new Label();
            AlarmStartFormatExampleLabel = new Label();
            AlarmStartFormatTextBox = new TextBox();
            TimeLeftFormatLabel = new Label();
            TimeLeftFormatExampleLabel = new Label();
            TimeLeftFormatExampleTextBox = new TextBox();
            AlarmWarningBox = new GroupBox();
            AlarmWarningTimeNumericUpDown = new NumericUpDown();
            AlarmWarningTimeLabel = new Label();
            alarmSourcePage = new TabPage();
            StartupGroupBox = new GroupBox();
            RunOnStartCheckBox = new CheckBox();
            FetchGroupBox = new GroupBox();
            FetchTimeLabel = new Label();
            FetchTimeNumericUpDown = new NumericUpDown();
            FetchIntervalNumericUpDown = new NumericUpDown();
            FetchIntervalLabel = new Label();
            audioPage = new TabPage();
            AudioOptionsgroupBox = new GroupBox();
            PlayTestButton = new Button();
            OffAfterStartLabel = new Label();
            OffAfterStartNumericUpDown = new NumericUpDown();
            DefaultSoundComboBox = new ComboBox();
            DefaultSoundLabel = new Label();
            colorPage = new TabPage();
            ColorGroupBox = new GroupBox();
            ColorYellowLabel = new Label();
            ColorGreenLabel = new Label();
            ColorAlarmPastStartLabel = new Label();
            ColorRedLabel = new Label();
            colorDialog = new ColorDialog();
            settingsTabControl.SuspendLayout();
            mainPage.SuspendLayout();
            MainGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MinimumWidthNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinimumWidthTrackBar).BeginInit();
            SliderGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BarHeightNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SliderSpeedNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SliderSpeedTrackBar).BeginInit();
            SizeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LeftNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LeftTrackBar).BeginInit();
            alarmPage.SuspendLayout();
            AlarmLocationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AlarmLocationLeftNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AlarmLocationTopNumericUpDown).BeginInit();
            StringFormatGroupBox.SuspendLayout();
            AlarmWarningBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AlarmWarningTimeNumericUpDown).BeginInit();
            alarmSourcePage.SuspendLayout();
            StartupGroupBox.SuspendLayout();
            FetchGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FetchTimeNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FetchIntervalNumericUpDown).BeginInit();
            audioPage.SuspendLayout();
            AudioOptionsgroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OffAfterStartNumericUpDown).BeginInit();
            colorPage.SuspendLayout();
            ColorGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // settingsTabControl
            // 
            settingsTabControl.Controls.Add(mainPage);
            settingsTabControl.Controls.Add(alarmPage);
            settingsTabControl.Controls.Add(alarmSourcePage);
            settingsTabControl.Controls.Add(audioPage);
            settingsTabControl.Controls.Add(colorPage);
            settingsTabControl.Dock = DockStyle.Fill;
            settingsTabControl.Location = new Point(0, 0);
            settingsTabControl.Name = "settingsTabControl";
            settingsTabControl.SelectedIndex = 0;
            settingsTabControl.Size = new Size(307, 376);
            settingsTabControl.TabIndex = 0;
            // 
            // mainPage
            // 
            mainPage.Controls.Add(MainGroupBox);
            mainPage.Controls.Add(SliderGroupBox);
            mainPage.Controls.Add(SizeGroupBox);
            mainPage.Location = new Point(4, 24);
            mainPage.Name = "mainPage";
            mainPage.Padding = new Padding(3);
            mainPage.Size = new Size(299, 348);
            mainPage.TabIndex = 0;
            mainPage.Text = "Main";
            // 
            // MainGroupBox
            // 
            MainGroupBox.Controls.Add(MinimumWidthNumericUpDown);
            MainGroupBox.Controls.Add(MinimumWidthLabel);
            MainGroupBox.Controls.Add(MinimumWidthTrackBar);
            MainGroupBox.Location = new Point(8, 244);
            MainGroupBox.Name = "MainGroupBox";
            MainGroupBox.Size = new Size(280, 98);
            MainGroupBox.TabIndex = 22;
            MainGroupBox.TabStop = false;
            MainGroupBox.Text = "Main";
            // 
            // MinimumWidthNumericUpDown
            // 
            MinimumWidthNumericUpDown.Location = new Point(194, 22);
            MinimumWidthNumericUpDown.Name = "MinimumWidthNumericUpDown";
            MinimumWidthNumericUpDown.Size = new Size(80, 23);
            MinimumWidthNumericUpDown.TabIndex = 19;
            MinimumWidthNumericUpDown.ValueChanged += MinimumWidthNumericUpDown_ValueChanged;
            // 
            // MinimumWidthLabel
            // 
            MinimumWidthLabel.AutoSize = true;
            MinimumWidthLabel.Location = new Point(6, 24);
            MinimumWidthLabel.Name = "MinimumWidthLabel";
            MinimumWidthLabel.Size = new Size(142, 15);
            MinimumWidthLabel.TabIndex = 17;
            MinimumWidthLabel.Text = "Window Minimum Width";
            // 
            // MinimumWidthTrackBar
            // 
            MinimumWidthTrackBar.Location = new Point(6, 51);
            MinimumWidthTrackBar.Maximum = 5120;
            MinimumWidthTrackBar.Name = "MinimumWidthTrackBar";
            MinimumWidthTrackBar.Size = new Size(268, 45);
            MinimumWidthTrackBar.TabIndex = 18;
            MinimumWidthTrackBar.ValueChanged += MinimumWidthTrackBar_ValueChanged;
            // 
            // SliderGroupBox
            // 
            SliderGroupBox.Controls.Add(BarHeightNumericUpDown);
            SliderGroupBox.Controls.Add(SystemBarHeightLabel);
            SliderGroupBox.Controls.Add(SliderSpeedNumericUpDown);
            SliderGroupBox.Controls.Add(SliderSpeedLabel);
            SliderGroupBox.Controls.Add(SliderSpeedTrackBar);
            SliderGroupBox.Location = new Point(8, 111);
            SliderGroupBox.Name = "SliderGroupBox";
            SliderGroupBox.Size = new Size(280, 127);
            SliderGroupBox.TabIndex = 21;
            SliderGroupBox.TabStop = false;
            SliderGroupBox.Text = "Slider";
            // 
            // BarHeightNumericUpDown
            // 
            BarHeightNumericUpDown.Location = new Point(194, 22);
            BarHeightNumericUpDown.Name = "BarHeightNumericUpDown";
            BarHeightNumericUpDown.Size = new Size(80, 23);
            BarHeightNumericUpDown.TabIndex = 3;
            BarHeightNumericUpDown.ValueChanged += BarHeightNumericUpDown_ValueChanged;
            BarHeightNumericUpDown.Enter += BarHeightNumericUpDown_Enter;
            BarHeightNumericUpDown.Leave += BarHeightNumericUpDown_Leave;
            // 
            // SystemBarHeightLabel
            // 
            SystemBarHeightLabel.AutoSize = true;
            SystemBarHeightLabel.Location = new Point(6, 24);
            SystemBarHeightLabel.Name = "SystemBarHeightLabel";
            SystemBarHeightLabel.Size = new Size(104, 15);
            SystemBarHeightLabel.TabIndex = 4;
            SystemBarHeightLabel.Text = "System Bar Height";
            // 
            // SliderSpeedNumericUpDown
            // 
            SliderSpeedNumericUpDown.Location = new Point(194, 51);
            SliderSpeedNumericUpDown.Name = "SliderSpeedNumericUpDown";
            SliderSpeedNumericUpDown.Size = new Size(80, 23);
            SliderSpeedNumericUpDown.TabIndex = 11;
            SliderSpeedNumericUpDown.ValueChanged += SliderSpeedNumericUpDown_ValueChanged;
            SliderSpeedNumericUpDown.Enter += SliderSpeedNumericUpDown_Enter;
            SliderSpeedNumericUpDown.Leave += SliderSpeedNumericUpDown_Leave;
            // 
            // SliderSpeedLabel
            // 
            SliderSpeedLabel.AutoSize = true;
            SliderSpeedLabel.Location = new Point(6, 53);
            SliderSpeedLabel.Name = "SliderSpeedLabel";
            SliderSpeedLabel.Size = new Size(71, 15);
            SliderSpeedLabel.TabIndex = 7;
            SliderSpeedLabel.Text = "Slider Speed";
            // 
            // SliderSpeedTrackBar
            // 
            SliderSpeedTrackBar.Location = new Point(8, 80);
            SliderSpeedTrackBar.Maximum = 5120;
            SliderSpeedTrackBar.Name = "SliderSpeedTrackBar";
            SliderSpeedTrackBar.Size = new Size(266, 45);
            SliderSpeedTrackBar.TabIndex = 8;
            SliderSpeedTrackBar.ValueChanged += SliderSpeedTrackBar_ValueChanged;
            SliderSpeedTrackBar.Enter += SliderSpeedTrackBar_Enter;
            SliderSpeedTrackBar.Leave += SliderSpeedTrackBar_Leave;
            // 
            // SizeGroupBox
            // 
            SizeGroupBox.Controls.Add(LeftNumericUpDown);
            SizeGroupBox.Controls.Add(locationLabel);
            SizeGroupBox.Controls.Add(LeftTrackBar);
            SizeGroupBox.Location = new Point(8, 6);
            SizeGroupBox.Name = "SizeGroupBox";
            SizeGroupBox.Size = new Size(280, 99);
            SizeGroupBox.TabIndex = 20;
            SizeGroupBox.TabStop = false;
            SizeGroupBox.Text = "Location";
            // 
            // LeftNumericUpDown
            // 
            LeftNumericUpDown.ImeMode = ImeMode.NoControl;
            LeftNumericUpDown.Location = new Point(194, 22);
            LeftNumericUpDown.Name = "LeftNumericUpDown";
            LeftNumericUpDown.Size = new Size(80, 23);
            LeftNumericUpDown.TabIndex = 10;
            LeftNumericUpDown.ValueChanged += LeftNumericUpDown_ValueChanged;
            // 
            // locationLabel
            // 
            locationLabel.AutoSize = true;
            locationLabel.Location = new Point(6, 24);
            locationLabel.Name = "locationLabel";
            locationLabel.Size = new Size(80, 15);
            locationLabel.TabIndex = 0;
            locationLabel.Text = "Start Location";
            // 
            // LeftTrackBar
            // 
            LeftTrackBar.Location = new Point(8, 51);
            LeftTrackBar.Maximum = 5120;
            LeftTrackBar.Name = "LeftTrackBar";
            LeftTrackBar.Size = new Size(266, 45);
            LeftTrackBar.TabIndex = 1;
            LeftTrackBar.Scroll += LeftTrackBarScroll;
            // 
            // alarmPage
            // 
            alarmPage.Controls.Add(AlarmLocationGroupBox);
            alarmPage.Controls.Add(StringFormatGroupBox);
            alarmPage.Controls.Add(AlarmWarningBox);
            alarmPage.Location = new Point(4, 24);
            alarmPage.Name = "alarmPage";
            alarmPage.Padding = new Padding(3);
            alarmPage.Size = new Size(299, 348);
            alarmPage.TabIndex = 1;
            alarmPage.Text = "Alarm";
            alarmPage.UseVisualStyleBackColor = true;
            // 
            // AlarmLocationGroupBox
            // 
            AlarmLocationGroupBox.Controls.Add(AlarmLocationLeftLabel);
            AlarmLocationGroupBox.Controls.Add(AlarmLocationLeftNumericUpDown);
            AlarmLocationGroupBox.Controls.Add(AlarmLocationTopNumericUpDown);
            AlarmLocationGroupBox.Controls.Add(AlarmLocationTopLabel);
            AlarmLocationGroupBox.Location = new Point(8, 203);
            AlarmLocationGroupBox.Name = "AlarmLocationGroupBox";
            AlarmLocationGroupBox.Size = new Size(280, 100);
            AlarmLocationGroupBox.TabIndex = 13;
            AlarmLocationGroupBox.TabStop = false;
            AlarmLocationGroupBox.Text = "Location";
            // 
            // AlarmLocationLeftLabel
            // 
            AlarmLocationLeftLabel.AutoSize = true;
            AlarmLocationLeftLabel.Location = new Point(6, 53);
            AlarmLocationLeftLabel.Name = "AlarmLocationLeftLabel";
            AlarmLocationLeftLabel.Size = new Size(27, 15);
            AlarmLocationLeftLabel.TabIndex = 14;
            AlarmLocationLeftLabel.Text = "Left";
            // 
            // AlarmLocationLeftNumericUpDown
            // 
            AlarmLocationLeftNumericUpDown.Location = new Point(151, 51);
            AlarmLocationLeftNumericUpDown.Name = "AlarmLocationLeftNumericUpDown";
            AlarmLocationLeftNumericUpDown.Size = new Size(120, 23);
            AlarmLocationLeftNumericUpDown.TabIndex = 15;
            AlarmLocationLeftNumericUpDown.ValueChanged += AlarmLocationLeftNumericUpDown_ValueChanged;
            // 
            // AlarmLocationTopNumericUpDown
            // 
            AlarmLocationTopNumericUpDown.Location = new Point(151, 22);
            AlarmLocationTopNumericUpDown.Name = "AlarmLocationTopNumericUpDown";
            AlarmLocationTopNumericUpDown.Size = new Size(120, 23);
            AlarmLocationTopNumericUpDown.TabIndex = 1;
            AlarmLocationTopNumericUpDown.ValueChanged += AlarmLocationTopNumericUpDown_ValueChanged;
            // 
            // AlarmLocationTopLabel
            // 
            AlarmLocationTopLabel.AutoSize = true;
            AlarmLocationTopLabel.Location = new Point(6, 24);
            AlarmLocationTopLabel.Name = "AlarmLocationTopLabel";
            AlarmLocationTopLabel.Size = new Size(26, 15);
            AlarmLocationTopLabel.TabIndex = 0;
            AlarmLocationTopLabel.Text = "Top";
            // 
            // StringFormatGroupBox
            // 
            StringFormatGroupBox.Controls.Add(AlarmStartFormatLabel);
            StringFormatGroupBox.Controls.Add(AlarmStartFormatExampleLabel);
            StringFormatGroupBox.Controls.Add(AlarmStartFormatTextBox);
            StringFormatGroupBox.Controls.Add(TimeLeftFormatLabel);
            StringFormatGroupBox.Controls.Add(TimeLeftFormatExampleLabel);
            StringFormatGroupBox.Controls.Add(TimeLeftFormatExampleTextBox);
            StringFormatGroupBox.Location = new Point(8, 68);
            StringFormatGroupBox.Name = "StringFormatGroupBox";
            StringFormatGroupBox.Size = new Size(280, 125);
            StringFormatGroupBox.TabIndex = 12;
            StringFormatGroupBox.TabStop = false;
            StringFormatGroupBox.Text = "Formating";
            // 
            // AlarmStartFormatLabel
            // 
            AlarmStartFormatLabel.AutoSize = true;
            AlarmStartFormatLabel.Location = new Point(6, 74);
            AlarmStartFormatLabel.Name = "AlarmStartFormatLabel";
            AlarmStartFormatLabel.Size = new Size(149, 15);
            AlarmStartFormatLabel.TabIndex = 10;
            AlarmStartFormatLabel.Text = "Alarm Guitar Format String";
            // 
            // AlarmStartFormatExampleLabel
            // 
            AlarmStartFormatExampleLabel.AutoSize = true;
            AlarmStartFormatExampleLabel.Location = new Point(177, 74);
            AlarmStartFormatExampleLabel.Name = "AlarmStartFormatExampleLabel";
            AlarmStartFormatExampleLabel.Size = new Size(71, 15);
            AlarmStartFormatExampleLabel.TabIndex = 11;
            AlarmStartFormatExampleLabel.Text = "00:00:00 AM";
            // 
            // AlarmStartFormatTextBox
            // 
            AlarmStartFormatTextBox.Location = new Point(6, 92);
            AlarmStartFormatTextBox.Name = "AlarmStartFormatTextBox";
            AlarmStartFormatTextBox.Size = new Size(268, 23);
            AlarmStartFormatTextBox.TabIndex = 12;
            AlarmStartFormatTextBox.TextChanged += AlarmStartFormatTextBox_TextChanged;
            // 
            // TimeLeftFormatLabel
            // 
            TimeLeftFormatLabel.AutoSize = true;
            TimeLeftFormatLabel.Location = new Point(6, 19);
            TimeLeftFormatLabel.Name = "TimeLeftFormatLabel";
            TimeLeftFormatLabel.Size = new Size(162, 15);
            TimeLeftFormatLabel.TabIndex = 7;
            TimeLeftFormatLabel.Text = "Alarm Warning Format String";
            // 
            // TimeLeftFormatExampleLabel
            // 
            TimeLeftFormatExampleLabel.AutoSize = true;
            TimeLeftFormatExampleLabel.Location = new Point(177, 19);
            TimeLeftFormatExampleLabel.Name = "TimeLeftFormatExampleLabel";
            TimeLeftFormatExampleLabel.Size = new Size(71, 15);
            TimeLeftFormatExampleLabel.TabIndex = 8;
            TimeLeftFormatExampleLabel.Text = "00:00:00 AM";
            // 
            // TimeLeftFormatExampleTextBox
            // 
            TimeLeftFormatExampleTextBox.Location = new Point(6, 37);
            TimeLeftFormatExampleTextBox.Name = "TimeLeftFormatExampleTextBox";
            TimeLeftFormatExampleTextBox.Size = new Size(268, 23);
            TimeLeftFormatExampleTextBox.TabIndex = 9;
            TimeLeftFormatExampleTextBox.TextChanged += TimeLeftFormatExampleTextBox_TextChanged;
            // 
            // AlarmWarningBox
            // 
            AlarmWarningBox.Controls.Add(AlarmWarningTimeNumericUpDown);
            AlarmWarningBox.Controls.Add(AlarmWarningTimeLabel);
            AlarmWarningBox.Location = new Point(8, 6);
            AlarmWarningBox.Name = "AlarmWarningBox";
            AlarmWarningBox.Size = new Size(280, 56);
            AlarmWarningBox.TabIndex = 11;
            AlarmWarningBox.TabStop = false;
            AlarmWarningBox.Text = "Warning Options";
            // 
            // AlarmWarningTimeNumericUpDown
            // 
            AlarmWarningTimeNumericUpDown.Location = new Point(154, 22);
            AlarmWarningTimeNumericUpDown.Name = "AlarmWarningTimeNumericUpDown";
            AlarmWarningTimeNumericUpDown.Size = new Size(120, 23);
            AlarmWarningTimeNumericUpDown.TabIndex = 1;
            AlarmWarningTimeNumericUpDown.ValueChanged += AlarmWarningTimeNumericUpDown_ValueChanged;
            // 
            // AlarmWarningTimeLabel
            // 
            AlarmWarningTimeLabel.AutoSize = true;
            AlarmWarningTimeLabel.Location = new Point(6, 24);
            AlarmWarningTimeLabel.Name = "AlarmWarningTimeLabel";
            AlarmWarningTimeLabel.Size = new Size(116, 15);
            AlarmWarningTimeLabel.TabIndex = 0;
            AlarmWarningTimeLabel.Text = "Alarm Warning Time";
            // 
            // alarmSourcePage
            // 
            alarmSourcePage.Controls.Add(StartupGroupBox);
            alarmSourcePage.Controls.Add(FetchGroupBox);
            alarmSourcePage.Location = new Point(4, 24);
            alarmSourcePage.Name = "alarmSourcePage";
            alarmSourcePage.Padding = new Padding(3);
            alarmSourcePage.Size = new Size(299, 348);
            alarmSourcePage.TabIndex = 2;
            alarmSourcePage.Text = "Alarm Source";
            alarmSourcePage.UseVisualStyleBackColor = true;
            // 
            // StartupGroupBox
            // 
            StartupGroupBox.Controls.Add(RunOnStartCheckBox);
            StartupGroupBox.Location = new Point(8, 96);
            StartupGroupBox.Name = "StartupGroupBox";
            StartupGroupBox.Size = new Size(277, 54);
            StartupGroupBox.TabIndex = 15;
            StartupGroupBox.TabStop = false;
            StartupGroupBox.Text = "Startup Options";
            // 
            // RunOnStartCheckBox
            // 
            RunOnStartCheckBox.AutoSize = true;
            RunOnStartCheckBox.Location = new Point(6, 22);
            RunOnStartCheckBox.Name = "RunOnStartCheckBox";
            RunOnStartCheckBox.Size = new Size(90, 19);
            RunOnStartCheckBox.TabIndex = 1;
            RunOnStartCheckBox.Text = "Run on start";
            RunOnStartCheckBox.UseVisualStyleBackColor = true;
            RunOnStartCheckBox.CheckedChanged += RunOnStartCheckBox_CheckedChanged;
            // 
            // FetchGroupBox
            // 
            FetchGroupBox.Controls.Add(FetchTimeLabel);
            FetchGroupBox.Controls.Add(FetchTimeNumericUpDown);
            FetchGroupBox.Controls.Add(FetchIntervalNumericUpDown);
            FetchGroupBox.Controls.Add(FetchIntervalLabel);
            FetchGroupBox.Location = new Point(8, 6);
            FetchGroupBox.Name = "FetchGroupBox";
            FetchGroupBox.Size = new Size(277, 84);
            FetchGroupBox.TabIndex = 14;
            FetchGroupBox.TabStop = false;
            FetchGroupBox.Text = "Fetch";
            // 
            // FetchTimeLabel
            // 
            FetchTimeLabel.AutoSize = true;
            FetchTimeLabel.Location = new Point(6, 53);
            FetchTimeLabel.Name = "FetchTimeLabel";
            FetchTimeLabel.Size = new Size(113, 15);
            FetchTimeLabel.TabIndex = 14;
            FetchTimeLabel.Text = "Fetch X hours worth";
            // 
            // FetchTimeNumericUpDown
            // 
            FetchTimeNumericUpDown.Location = new Point(160, 51);
            FetchTimeNumericUpDown.Name = "FetchTimeNumericUpDown";
            FetchTimeNumericUpDown.Size = new Size(111, 23);
            FetchTimeNumericUpDown.TabIndex = 15;
            FetchTimeNumericUpDown.ValueChanged += FetchTimeNumericUpDown_ValueChanged;
            // 
            // FetchIntervalNumericUpDown
            // 
            FetchIntervalNumericUpDown.Location = new Point(160, 22);
            FetchIntervalNumericUpDown.Name = "FetchIntervalNumericUpDown";
            FetchIntervalNumericUpDown.Size = new Size(111, 23);
            FetchIntervalNumericUpDown.TabIndex = 1;
            FetchIntervalNumericUpDown.ValueChanged += FetchIntervalNumericUpDown_ValueChanged;
            // 
            // FetchIntervalLabel
            // 
            FetchIntervalLabel.AutoSize = true;
            FetchIntervalLabel.Location = new Point(6, 24);
            FetchIntervalLabel.Name = "FetchIntervalLabel";
            FetchIntervalLabel.Size = new Size(148, 15);
            FetchIntervalLabel.TabIndex = 0;
            FetchIntervalLabel.Text = "Fetch new every X Minutes";
            // 
            // audioPage
            // 
            audioPage.Controls.Add(AudioOptionsgroupBox);
            audioPage.Location = new Point(4, 24);
            audioPage.Name = "audioPage";
            audioPage.Padding = new Padding(3);
            audioPage.Size = new Size(299, 348);
            audioPage.TabIndex = 4;
            audioPage.Text = "Audio";
            audioPage.UseVisualStyleBackColor = true;
            // 
            // AudioOptionsgroupBox
            // 
            AudioOptionsgroupBox.Controls.Add(PlayTestButton);
            AudioOptionsgroupBox.Controls.Add(OffAfterStartLabel);
            AudioOptionsgroupBox.Controls.Add(OffAfterStartNumericUpDown);
            AudioOptionsgroupBox.Controls.Add(DefaultSoundComboBox);
            AudioOptionsgroupBox.Controls.Add(DefaultSoundLabel);
            AudioOptionsgroupBox.Location = new Point(8, 6);
            AudioOptionsgroupBox.Name = "AudioOptionsgroupBox";
            AudioOptionsgroupBox.Size = new Size(280, 111);
            AudioOptionsgroupBox.TabIndex = 0;
            AudioOptionsgroupBox.TabStop = false;
            AudioOptionsgroupBox.Text = "Options";
            // 
            // PlayTestButton
            // 
            PlayTestButton.Location = new Point(199, 22);
            PlayTestButton.Name = "PlayTestButton";
            PlayTestButton.Size = new Size(75, 23);
            PlayTestButton.TabIndex = 1;
            PlayTestButton.Text = "Test";
            PlayTestButton.UseVisualStyleBackColor = true;
            PlayTestButton.Click += PlayTestButton_Click;
            // 
            // OffAfterStartLabel
            // 
            OffAfterStartLabel.AutoSize = true;
            OffAfterStartLabel.Location = new Point(6, 82);
            OffAfterStartLabel.Name = "OffAfterStartLabel";
            OffAfterStartLabel.Size = new Size(165, 15);
            OffAfterStartLabel.TabIndex = 4;
            OffAfterStartLabel.Text = "Turn sound off after start time";
            // 
            // OffAfterStartNumericUpDown
            // 
            OffAfterStartNumericUpDown.Location = new Point(177, 80);
            OffAfterStartNumericUpDown.Name = "OffAfterStartNumericUpDown";
            OffAfterStartNumericUpDown.Size = new Size(97, 23);
            OffAfterStartNumericUpDown.TabIndex = 3;
            OffAfterStartNumericUpDown.ValueChanged += OffAfterStartNumericUpDown_ValueChanged;
            // 
            // DefaultSoundComboBox
            // 
            DefaultSoundComboBox.FormattingEnabled = true;
            DefaultSoundComboBox.Location = new Point(6, 51);
            DefaultSoundComboBox.Name = "DefaultSoundComboBox";
            DefaultSoundComboBox.Size = new Size(268, 23);
            DefaultSoundComboBox.TabIndex = 2;
            DefaultSoundComboBox.SelectedIndexChanged += DefaultSoundComboBox_SelectedIndexChanged;
            // 
            // DefaultSoundLabel
            // 
            DefaultSoundLabel.AutoSize = true;
            DefaultSoundLabel.Location = new Point(6, 26);
            DefaultSoundLabel.Name = "DefaultSoundLabel";
            DefaultSoundLabel.Size = new Size(82, 15);
            DefaultSoundLabel.TabIndex = 0;
            DefaultSoundLabel.Text = "Default Sound";
            // 
            // colorPage
            // 
            colorPage.Controls.Add(ColorGroupBox);
            colorPage.Location = new Point(4, 24);
            colorPage.Name = "colorPage";
            colorPage.Padding = new Padding(3);
            colorPage.Size = new Size(299, 348);
            colorPage.TabIndex = 3;
            colorPage.Text = "Colors";
            colorPage.UseVisualStyleBackColor = true;
            // 
            // ColorGroupBox
            // 
            ColorGroupBox.Controls.Add(ColorYellowLabel);
            ColorGroupBox.Controls.Add(ColorGreenLabel);
            ColorGroupBox.Controls.Add(ColorAlarmPastStartLabel);
            ColorGroupBox.Controls.Add(ColorRedLabel);
            ColorGroupBox.Location = new Point(8, 6);
            ColorGroupBox.Name = "ColorGroupBox";
            ColorGroupBox.Size = new Size(280, 265);
            ColorGroupBox.TabIndex = 0;
            ColorGroupBox.TabStop = false;
            ColorGroupBox.Text = "Color";
            // 
            // ColorYellowLabel
            // 
            ColorYellowLabel.BackColor = Color.Magenta;
            ColorYellowLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            ColorYellowLabel.Location = new Point(6, 143);
            ColorYellowLabel.Name = "ColorYellowLabel";
            ColorYellowLabel.Size = new Size(268, 49);
            ColorYellowLabel.TabIndex = 11;
            ColorYellowLabel.Text = "Yellow";
            ColorYellowLabel.TextAlign = ContentAlignment.MiddleCenter;
            ColorYellowLabel.Click += ColorYellowLabel_Click;
            // 
            // ColorGreenLabel
            // 
            ColorGreenLabel.BackColor = Color.Magenta;
            ColorGreenLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            ColorGreenLabel.Location = new Point(6, 81);
            ColorGreenLabel.Name = "ColorGreenLabel";
            ColorGreenLabel.Size = new Size(268, 49);
            ColorGreenLabel.TabIndex = 10;
            ColorGreenLabel.Text = "Green";
            ColorGreenLabel.TextAlign = ContentAlignment.MiddleCenter;
            ColorGreenLabel.Click += ColorGreenLabel_Click;
            // 
            // ColorAlarmPastStartLabel
            // 
            ColorAlarmPastStartLabel.BackColor = Color.Magenta;
            ColorAlarmPastStartLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            ColorAlarmPastStartLabel.Location = new Point(6, 19);
            ColorAlarmPastStartLabel.Name = "ColorAlarmPastStartLabel";
            ColorAlarmPastStartLabel.Size = new Size(268, 49);
            ColorAlarmPastStartLabel.TabIndex = 9;
            ColorAlarmPastStartLabel.Text = "Alarm Past Start";
            ColorAlarmPastStartLabel.TextAlign = ContentAlignment.MiddleCenter;
            ColorAlarmPastStartLabel.Click += ColorAlarmPastStartLabel_Click;
            // 
            // ColorRedLabel
            // 
            ColorRedLabel.BackColor = Color.Magenta;
            ColorRedLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            ColorRedLabel.Location = new Point(6, 205);
            ColorRedLabel.Name = "ColorRedLabel";
            ColorRedLabel.Size = new Size(268, 49);
            ColorRedLabel.TabIndex = 8;
            ColorRedLabel.Text = "Red";
            ColorRedLabel.TextAlign = ContentAlignment.MiddleCenter;
            ColorRedLabel.Click += RedColorExampleLabel_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(307, 376);
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
            MainGroupBox.ResumeLayout(false);
            MainGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MinimumWidthNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinimumWidthTrackBar).EndInit();
            SliderGroupBox.ResumeLayout(false);
            SliderGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BarHeightNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)SliderSpeedNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)SliderSpeedTrackBar).EndInit();
            SizeGroupBox.ResumeLayout(false);
            SizeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LeftNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)LeftTrackBar).EndInit();
            alarmPage.ResumeLayout(false);
            AlarmLocationGroupBox.ResumeLayout(false);
            AlarmLocationGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AlarmLocationLeftNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)AlarmLocationTopNumericUpDown).EndInit();
            StringFormatGroupBox.ResumeLayout(false);
            StringFormatGroupBox.PerformLayout();
            AlarmWarningBox.ResumeLayout(false);
            AlarmWarningBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AlarmWarningTimeNumericUpDown).EndInit();
            alarmSourcePage.ResumeLayout(false);
            StartupGroupBox.ResumeLayout(false);
            StartupGroupBox.PerformLayout();
            FetchGroupBox.ResumeLayout(false);
            FetchGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)FetchTimeNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)FetchIntervalNumericUpDown).EndInit();
            audioPage.ResumeLayout(false);
            AudioOptionsgroupBox.ResumeLayout(false);
            AudioOptionsgroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OffAfterStartNumericUpDown).EndInit();
            colorPage.ResumeLayout(false);
            ColorGroupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl settingsTabControl;
        private TabPage mainPage;
        private TabPage alarmPage;
        private TabPage alarmSourcePage;
        private TabPage audioPage;
        private TabPage colorPage;
        private TrackBar LeftTrackBar;
        private Label locationLabel;
        private NumericUpDown BarHeightNumericUpDown;
        private Label SystemBarHeightLabel;
        private TrackBar SliderSpeedTrackBar;
        private Label SliderSpeedLabel;
        private NumericUpDown SliderSpeedNumericUpDown;
        private NumericUpDown LeftNumericUpDown;
        private NumericUpDown MinimumWidthNumericUpDown;
        private TrackBar MinimumWidthTrackBar;
        private Label MinimumWidthLabel;
        private NumericUpDown AlarmWarningTimeNumericUpDown;
        private Label AlarmWarningTimeLabel;
        private GroupBox StringFormatGroupBox;
        private Label AlarmStartFormatLabel;
        private Label AlarmStartFormatExampleLabel;
        private TextBox AlarmStartFormatTextBox;
        private Label TimeLeftFormatLabel;
        private Label TimeLeftFormatExampleLabel;
        private TextBox TimeLeftFormatExampleTextBox;
        private GroupBox AlarmWarningBox;
        private GroupBox AlarmLocationGroupBox;
        private NumericUpDown AlarmLocationTopNumericUpDown;
        private Label AlarmLocationTopLabel;
        private Label AlarmLocationLeftLabel;
        private NumericUpDown AlarmLocationLeftNumericUpDown;
        private GroupBox SliderGroupBox;
        private GroupBox SizeGroupBox;
        private GroupBox FetchGroupBox;
        private Label FetchTimeLabel;
        private NumericUpDown FetchTimeNumericUpDown;
        private NumericUpDown FetchIntervalNumericUpDown;
        private Label FetchIntervalLabel;
        private ColorDialog colorDialog;
        private GroupBox MainGroupBox;
        private GroupBox AudioOptionsgroupBox;
        private Label OffAfterStartLabel;
        private NumericUpDown OffAfterStartNumericUpDown;
        private ComboBox DefaultSoundComboBox;
        private Label DefaultSoundLabel;
        private Button PlayTestButton;
        private GroupBox ColorGroupBox;
        private Label ColorRedLabel;
        private Label ColorYellowLabel;
        private Label ColorGreenLabel;
        private Label ColorAlarmPastStartLabel;
        private GroupBox StartupGroupBox;
        private CheckBox RunOnStartCheckBox;
    }
}