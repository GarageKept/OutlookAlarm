namespace GarageKept.OutlookAlarm.Forms.UI.Forms
{
    partial class SettingsWindow
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
            generalPage = new TabPage();
            calendarPage = new TabPage();
            todoPage = new TabPage();
            SaveButton = new Button();
            settingsTabControl.SuspendLayout();
            generalPage.SuspendLayout();
            SuspendLayout();
            // 
            // settingsTabControl
            // 
            settingsTabControl.Controls.Add(generalPage);
            settingsTabControl.Controls.Add(calendarPage);
            settingsTabControl.Controls.Add(todoPage);
            settingsTabControl.Dock = DockStyle.Fill;
            settingsTabControl.Location = new Point(0, 0);
            settingsTabControl.Name = "settingsTabControl";
            settingsTabControl.SelectedIndex = 0;
            settingsTabControl.Size = new Size(660, 378);
            settingsTabControl.TabIndex = 0;
            // 
            // generalPage
            // 
            generalPage.Controls.Add(SaveButton);
            generalPage.Location = new Point(4, 24);
            generalPage.Name = "generalPage";
            generalPage.Padding = new Padding(3);
            generalPage.Size = new Size(652, 350);
            generalPage.TabIndex = 0;
            generalPage.Text = "General";
            generalPage.UseVisualStyleBackColor = true;
            // 
            // calendarPage
            // 
            calendarPage.Location = new Point(4, 24);
            calendarPage.Name = "calendarPage";
            calendarPage.Padding = new Padding(3);
            calendarPage.Size = new Size(652, 350);
            calendarPage.TabIndex = 1;
            calendarPage.Text = "OutlookCalendarInterop";
            calendarPage.UseVisualStyleBackColor = true;
            // 
            // todoPage
            // 
            todoPage.Location = new Point(4, 24);
            todoPage.Name = "todoPage";
            todoPage.Padding = new Padding(3);
            todoPage.Size = new Size(652, 350);
            todoPage.TabIndex = 2;
            todoPage.Text = "MS ToDo";
            todoPage.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(569, 319);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 0;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(660, 378);
            Controls.Add(settingsTabControl);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Settings";
            StartPosition = FormStartPosition.Manual;
            Text = "Settings";
            settingsTabControl.ResumeLayout(false);
            generalPage.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl settingsTabControl;
        private TabPage generalPage;
        private TabPage calendarPage;
        private TabPage todoPage;
        private Button SaveButton;
    }
}