using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace GarageKept.OutlookAlarm.Alarm.UI.Controls;

public class AlarmProgressBar : ProgressBar
{
    public AlarmProgressBar()
    {
        SetStyle(ControlStyles.UserPaint, true);
        base.DoubleBuffered = true;
    }

    public AlarmProgressBar(Color barColor, Color backgroundColor) : this()
    {
        BarColor = barColor;
        BackgroundColor = backgroundColor;
    }

    public Color BarColor { get; set; } = Color.Green;
    public Color BackgroundColor { get; set; } = Color.LightGray;

    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
            return cp;
        }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        SendMessage(Handle, 0x400 + 16, 1, IntPtr.Zero); // PBM_SETMARQUEE
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        SuspendLayout();

        var rect = ClientRectangle;

        // Draw the background of the progress bar
        using (Brush brush = new SolidBrush(BackgroundColor))
        {
            e.Graphics.FillRectangle(brush, rect);
        }

        // Calculate the width of the progress bar
        var progressBarWidth = (int)(rect.Width * ((double)Value / Maximum));

        // Create the rectangle for the progress bar
        var progressBarRect = new Rectangle(rect.X, rect.Y, progressBarWidth, rect.Height);

        // Draw the progress bar
        using (Brush brush = new SolidBrush(BarColor))
        {
            e.Graphics.FillRectangle(brush, progressBarRect);
        }

        ResumeLayout(true);
    }

    [DllImport("user32.dll")]
    [SuppressMessage("Interoperability", "SYSLIB1054:Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time", Justification = "<Pending>")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
}