
namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

partial class AlarmForm
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
        TimeRight = new Label();
        TimeLeft = new Label();
        ActionSelector = new ComboBox();
        SubjectLabel = new Label();
        DismissButton = new Button();
        SnoozeButton = new Button();
        SuspendLayout();
        // 
        // TimeRight
        // 
        TimeRight.Font = new Font("Calibri", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        TimeRight.Location = new Point(199, 35);
        TimeRight.Name = "TimeRight";
        TimeRight.Size = new Size(102, 23);
        TimeRight.TabIndex = 13;
        TimeRight.Text = "00:00";
        TimeRight.TextAlign = ContentAlignment.TopRight;
        // 
        // TimeLeft
        // 
        TimeLeft.Font = new Font("Calibri", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        TimeLeft.Location = new Point(12, 35);
        TimeLeft.Name = "TimeLeft";
        TimeLeft.Size = new Size(125, 23);
        TimeLeft.TabIndex = 12;
        TimeLeft.Text = "00:00";
        // 
        // ActionSelector
        // 
        ActionSelector.FormattingEnabled = true;
        ActionSelector.Items.AddRange(new object[]
            { "5 minutes before start", "0 hours before start", "5 minutes", "10 minutes" });
        ActionSelector.Location = new Point(12, 61);
        ActionSelector.Name = "ActionSelector";
        ActionSelector.Size = new Size(142, 23);
        ActionSelector.TabIndex = 11;
        // 
        // SubjectLabel
        // 
        SubjectLabel.AutoSize = true;
        SubjectLabel.Font = new Font("Calibri", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
        SubjectLabel.Location = new Point(12, 9);
        SubjectLabel.Name = "SubjectLabel";
        SubjectLabel.Size = new Size(289, 26);
        SubjectLabel.TabIndex = 10;
        SubjectLabel.Text = "This is a sample subject message";
        // 
        // DismissButton
        // 
        DismissButton.Location = new Point(241, 60);
        DismissButton.Name = "DismissButton";
        DismissButton.Size = new Size(75, 23);
        DismissButton.TabIndex = 9;
        DismissButton.Text = "Dismiss";
        DismissButton.UseVisualStyleBackColor = true;
        DismissButton.Click += DismissButton_Click;
        // 
        // SnoozeButton
        // 
        SnoozeButton.Location = new Point(160, 61);
        SnoozeButton.Name = "SnoozeButton";
        SnoozeButton.Size = new Size(75, 23);
        SnoozeButton.TabIndex = 8;
        SnoozeButton.Text = "Snooze";
        SnoozeButton.UseVisualStyleBackColor = true;
        SnoozeButton.Click += SnoozeButton_Click;
        // 
        // AlarmWindowForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(330, 96);
        Controls.Add(TimeRight);
        Controls.Add(TimeLeft);
        Controls.Add(ActionSelector);
        Controls.Add(SubjectLabel);
        Controls.Add(DismissButton);
        Controls.Add(SnoozeButton);
        DoubleBuffered = true;
        FormBorderStyle = FormBorderStyle.None;
        Name = "AlarmWindowForm";
        PinTop = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "AlarmWindowForm";
        FormClosing += AlarmWindowForm_FormClosing;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    public Label TimeRight;
    public Label TimeLeft;
    private ComboBox ActionSelector;
    private Label SubjectLabel;
    private Button DismissButton;
    private Button SnoozeButton;
}