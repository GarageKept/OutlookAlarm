using GarageKept.OutlookAlarm.Alarm.UI.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            rightClickMenu = new ContextMenuStrip(components);
            upcomingAppointments = Program.ServiceProvider.GetService<AlarmContainerControl>();
            SuspendLayout();
            // 
            // rightClickMenu
            // 
            rightClickMenu.Name = "contextMenuStrip1";
            rightClickMenu.Size = new Size(61, 4);
            // 
            // upcomingAppointments
            // 
            upcomingAppointments.AutoSize = true;
            upcomingAppointments.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            upcomingAppointments.Dock = DockStyle.Fill;
            upcomingAppointments.Location = new Point(0, 0);
            upcomingAppointments.Margin = new Padding(0);
            upcomingAppointments.Name = "upcomingAppointments";
            upcomingAppointments.Size = new Size(256, 74);
            upcomingAppointments.TabIndex = 1;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(256, 74);
            ContextMenuStrip = rightClickMenu;
            Controls.Add(upcomingAppointments);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "Main Form";
            FormClosing += MainForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ContextMenuStrip rightClickMenu;
        private Controls.AlarmContainerControl upcomingAppointments;
    }
}