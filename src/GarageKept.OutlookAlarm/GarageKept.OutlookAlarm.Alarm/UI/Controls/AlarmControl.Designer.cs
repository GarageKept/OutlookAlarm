namespace GarageKept.OutlookAlarm.Alarms.UI.Controls;

partial class AlarmControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        SubjectLabel = new Label();
        progressBar = new ProgressBar();
        TimeLeft = new Label();
        TimeRight = new Label();
        SuspendLayout();
        // 
        // SubjectLabel
        // 
        SubjectLabel.Font = new Font("Calibri", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        SubjectLabel.Location = new Point(3, 3);
        SubjectLabel.Name = "SubjectLabel";
        SubjectLabel.Size = new Size(250, 23);
        SubjectLabel.TabIndex = 0;
        SubjectLabel.Text = "Subject Of Meeting";
        // 
        // progressBar
        // 
        progressBar.Location = new Point(3, 29);
        progressBar.Name = "progressBar";
        progressBar.Size = new Size(250, 23);
        progressBar.TabIndex = 3;
        // 
        // TimeLeft
        // 
        TimeLeft.Font = new Font("Calibri", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        TimeLeft.Location = new Point(3, 55);
        TimeLeft.Name = "TimeLeft";
        TimeLeft.Size = new Size(125, 23);
        TimeLeft.TabIndex = 4;
        TimeLeft.Text = "00:00";
        // 
        // TimeRight
        // 
        TimeRight.Font = new Font("Calibri", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        TimeRight.Location = new Point(151, 55);
        TimeRight.Name = "TimeRight";
        TimeRight.Size = new Size(102, 23);
        TimeRight.TabIndex = 5;
        TimeRight.Text = "00:00";
        TimeRight.TextAlign = ContentAlignment.TopRight;
        // 
        // AppointmentItemControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(TimeRight);
        Controls.Add(TimeLeft);
        Controls.Add(progressBar);
        Controls.Add(SubjectLabel);
        Margin = new Padding(0);
        Name = "AppointmentItemControl";
        Size = new Size(256, 82);
        ResumeLayout(false);
    }

    #endregion

    public Label SubjectLabel;
    public ProgressBar progressBar;
    public Label TimeLeft;
    public Label TimeRight;
}