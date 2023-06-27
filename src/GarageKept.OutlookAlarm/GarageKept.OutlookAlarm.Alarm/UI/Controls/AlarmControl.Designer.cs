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
        components = new System.ComponentModel.Container();
        SubjectLabel = new Label();
        progressBar = new ProgressBar();
        TimeLeft = new Label();
        TimeRight = new Label();
        CategoryLabel = new Label();
        OrganizerLabel = new Label();
        HorizontalLine = new Label();
        TeamsLinkLabel = new LinkLabel();
        RightClickMenuStrip = new ContextMenuStrip(components);
        removeToolStripMenuItem = new ToolStripMenuItem();
        dismissToolStripMenuItem = new ToolStripMenuItem();
        reminderToolStripMenuItem = new ToolStripMenuItem();
        RightClick_15Min = new ToolStripMenuItem();
        RightClick_10Min = new ToolStripMenuItem();
        RightClick_5Min = new ToolStripMenuItem();
        RightClick_0Min = new ToolStripMenuItem();
        RefreshTimer = new System.Windows.Forms.Timer(components);
        pictureBox1 = new PictureBox();
        RightClickMenuStrip.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
        TeamsLinkLabel.LinkClicked += OnTeamsLinkLabelOnLinkClicked;
        // 
        // RightClickMenuStrip
        // 
        RightClickMenuStrip.Items.AddRange(new ToolStripItem[] { removeToolStripMenuItem, dismissToolStripMenuItem, reminderToolStripMenuItem });
        RightClickMenuStrip.Name = "RightClickMenuStrip";
        RightClickMenuStrip.Size = new Size(126, 70);
        // 
        // removeToolStripMenuItem
        // 
        removeToolStripMenuItem.Name = "removeToolStripMenuItem";
        removeToolStripMenuItem.Size = new Size(125, 22);
        removeToolStripMenuItem.Text = "Remove";
        removeToolStripMenuItem.Click += RightClickMenuRemove_Click;
        // 
        // dismissToolStripMenuItem
        // 
        dismissToolStripMenuItem.Name = "dismissToolStripMenuItem";
        dismissToolStripMenuItem.Size = new Size(125, 22);
        dismissToolStripMenuItem.Text = "Dismiss";
        dismissToolStripMenuItem.Click += RightClickMenuDismiss_Click;
        // 
        // reminderToolStripMenuItem
        // 
        reminderToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { RightClick_15Min, RightClick_10Min, RightClick_5Min, RightClick_0Min });
        reminderToolStripMenuItem.Name = "reminderToolStripMenuItem";
        reminderToolStripMenuItem.Size = new Size(125, 22);
        reminderToolStripMenuItem.Text = "Reminder";
        // 
        // RightClick_15Min
        // 
        RightClick_15Min.Name = "RightClick_15Min";
        RightClick_15Min.Size = new Size(147, 22);
        RightClick_15Min.Text = "15 Min Before";
        RightClick_15Min.Click += RightClick_15Min_Click;
        // 
        // RightClick_10Min
        // 
        RightClick_10Min.Name = "RightClick_10Min";
        RightClick_10Min.Size = new Size(147, 22);
        RightClick_10Min.Text = "10 Min Before";
        RightClick_10Min.Click += RightClick_10Min_Click;
        // 
        // RightClick_5Min
        // 
        RightClick_5Min.Name = "RightClick_5Min";
        RightClick_5Min.Size = new Size(147, 22);
        RightClick_5Min.Text = "5 Min Before";
        RightClick_5Min.Click += RightClick_5Min_Click;
        // 
        // RightClick_0Min
        // 
        RightClick_0Min.Name = "RightClick_0Min";
        RightClick_0Min.Size = new Size(147, 22);
        RightClick_0Min.Text = "0 min Before";
        RightClick_0Min.Click += RightClick_0Min_Click;
        // 
        // RefreshTimer
        // 
        RefreshTimer.Enabled = true;
        RefreshTimer.Interval = 1000;
        RefreshTimer.Tick += Refresh_Tick;
        // 
        // pictureBox1
        // 
        pictureBox1.Location = new Point(229, 3);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(24, 24);
        pictureBox1.TabIndex = 10;
        pictureBox1.TabStop = false;
        // 
        // AlarmControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ContextMenuStrip = RightClickMenuStrip;
        Controls.Add(pictureBox1);
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
        RightClickMenuStrip.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
    private ContextMenuStrip RightClickMenuStrip;
    private ToolStripMenuItem removeToolStripMenuItem;
    private ToolStripMenuItem dismissToolStripMenuItem;
    private System.Windows.Forms.Timer RefreshTimer;
    private ToolStripMenuItem reminderToolStripMenuItem;
    private ToolStripMenuItem RightClick_15Min;
    private ToolStripMenuItem RightClick_10Min;
    private ToolStripMenuItem RightClick_5Min;
    private ToolStripMenuItem RightClick_0Min;
    private PictureBox pictureBox1;
}