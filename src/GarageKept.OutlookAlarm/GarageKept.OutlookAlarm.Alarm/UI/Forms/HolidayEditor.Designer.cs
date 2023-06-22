namespace GarageKept.OutlookAlarm.Alarm.UI.Forms
{
    partial class HolidayEditor
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
            NameLabel = new Label();
            DatePicker = new DateTimePicker();
            DateLabel = new Label();
            DescriptionLabel = new Label();
            NameTextBox = new TextBox();
            DescriptionTextBox = new TextBox();
            SaveButton = new Button();
            CancelButton = new Button();
            SuspendLayout();
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(12, 15);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(39, 15);
            NameLabel.TabIndex = 0;
            NameLabel.Text = "Name";
            // 
            // DatePicker
            // 
            DatePicker.Location = new Point(56, 41);
            DatePicker.Name = "DatePicker";
            DatePicker.Size = new Size(200, 23);
            DatePicker.TabIndex = 1;
            // 
            // DateLabel
            // 
            DateLabel.AutoSize = true;
            DateLabel.Location = new Point(12, 47);
            DateLabel.Name = "DateLabel";
            DateLabel.Size = new Size(31, 15);
            DateLabel.TabIndex = 2;
            DateLabel.Text = "Date";
            // 
            // DescriptionLabel
            // 
            DescriptionLabel.AutoSize = true;
            DescriptionLabel.Location = new Point(12, 73);
            DescriptionLabel.Name = "DescriptionLabel";
            DescriptionLabel.Size = new Size(124, 15);
            DescriptionLabel.TabIndex = 3;
            DescriptionLabel.Text = "Description (Optional)";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(56, 12);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(200, 23);
            NameTextBox.TabIndex = 4;
            // 
            // DescriptionTextBox
            // 
            DescriptionTextBox.Location = new Point(12, 91);
            DescriptionTextBox.Multiline = true;
            DescriptionTextBox.Name = "DescriptionTextBox";
            DescriptionTextBox.Size = new Size(244, 97);
            DescriptionTextBox.TabIndex = 5;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(181, 194);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 6;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(100, 194);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 23);
            CancelButton.TabIndex = 7;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // HolidayEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(269, 224);
            Controls.Add(CancelButton);
            Controls.Add(SaveButton);
            Controls.Add(DescriptionTextBox);
            Controls.Add(NameTextBox);
            Controls.Add(DescriptionLabel);
            Controls.Add(DateLabel);
            Controls.Add(DatePicker);
            Controls.Add(NameLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "HolidayEditor";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Holiday Editor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label NameLabel;
        private DateTimePicker DatePicker;
        private Label DateLabel;
        private Label DescriptionLabel;
        private TextBox NameTextBox;
        private TextBox DescriptionTextBox;
        private Button SaveButton;
        private new Button CancelButton;
    }
}