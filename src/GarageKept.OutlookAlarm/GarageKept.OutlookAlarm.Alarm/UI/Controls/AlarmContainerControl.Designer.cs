namespace GarageKept.OutlookAlarm.Alarm.UI.Controls
{
    partial class AlarmContainerControl
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
            FooterProgressBar = new AlarmProgressBar();
            SuspendLayout();
            // 
            // FooterProgressBar
            // 
            FooterProgressBar.Name = "FooterProgressBar";
            FooterProgressBar.Size = new Size(100, 23);
            FooterProgressBar.TabIndex = 0;
            // 
            // AlarmContainerControl
            // 
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ColumnCount = 1;
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Dock = DockStyle.Fill;
            Margin = new Padding(0);
            Size = new Size(256, 15);
            ResumeLayout(false);
        }

        #endregion

        private AlarmProgressBar FooterProgressBar;
    }
}
