namespace GarageKept.OutlookAlarm.Alarm.UI.Controls;

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
        CategoryLabel = new Label();
        OrganizerLabel = new Label();
        HorizontalLine = new Label();
        TeamsLinkLabel = new LinkLabel();
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
        progressBar.Location = new Point(3, 69);
        progressBar.Name = "progressBar";
        progressBar.Size = new Size(250, 23);
        progressBar.TabIndex = 3;
        // 
        // TimeLeft
        // 
        TimeLeft.Font = new Font("Calibri", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        TimeLeft.Location = new Point(3, 95);
        TimeLeft.Name = "TimeLeft";
        TimeLeft.Size = new Size(125, 23);
        TimeLeft.TabIndex = 4;
        TimeLeft.Text = "00:00";
        // 
        // TimeRight
        // 
        TimeRight.Font = new Font("Calibri", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        TimeRight.Location = new Point(151, 95);
        TimeRight.Name = "TimeRight";
        TimeRight.Size = new Size(102, 23);
        TimeRight.TabIndex = 5;
        TimeRight.Text = "00:00";
        TimeRight.TextAlign = ContentAlignment.TopRight;
        // 
        // CategoryLabel
        // 
        CategoryLabel.AutoSize = true;
        CategoryLabel.Location = new Point(3, 26);
        CategoryLabel.Name = "CategoryLabel";
        CategoryLabel.Size = new Size(63, 15);
        CategoryLabel.TabIndex = 6;
        CategoryLabel.Text = "Categories";
        // 
        // OrganizerLabel
        // 
        OrganizerLabel.AutoSize = true;
        OrganizerLabel.Font = new Font("Calibri", 9.75F, FontStyle.Italic, GraphicsUnit.Point);
        OrganizerLabel.Location = new Point(3, 45);
        OrganizerLabel.Name = "OrganizerLabel";
        OrganizerLabel.Size = new Size(59, 15);
        OrganizerLabel.TabIndex = 7;
        OrganizerLabel.Text = "Organizer";
        // 
        // HorizontalLine
        // 
        HorizontalLine.BackColor = Color.Black;
        HorizontalLine.BorderStyle = BorderStyle.FixedSingle;
        HorizontalLine.Location = new Point(0, 118);
        HorizontalLine.Margin = new Padding(0);
        HorizontalLine.Name = "HorizontalLine";
        HorizontalLine.Size = new Size(256, 4);
        HorizontalLine.TabIndex = 8;
        // 
        // TeamsLinkLabel
        // 
        TeamsLinkLabel.AutoSize = true;
        TeamsLinkLabel.Location = new Point(171, 45);
        TeamsLinkLabel.Name = "TeamsLinkLabel";
        TeamsLinkLabel.Size = new Size(82, 15);
        TeamsLinkLabel.TabIndex = 9;
        TeamsLinkLabel.TabStop = true;
        TeamsLinkLabel.Text = "Launch Teams";
        TeamsLinkLabel.Visible = false;
        // 
        // AlarmControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(TeamsLinkLabel);
        Controls.Add(HorizontalLine);
        Controls.Add(OrganizerLabel);
        Controls.Add(CategoryLabel);
        Controls.Add(TimeRight);
        Controls.Add(TimeLeft);
        Controls.Add(progressBar);
        Controls.Add(SubjectLabel);
        Margin = new Padding(0);
        Name = "AlarmControl";
        Size = new Size(256, 122);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    public Label SubjectLabel;
    public ProgressBar progressBar;
    public Label TimeLeft;
    public Label TimeRight;
    private Label CategoryLabel;
    private Label OrganizerLabel;
    private Label HorizontalLine;
    private LinkLabel TeamsLinkLabel;
}