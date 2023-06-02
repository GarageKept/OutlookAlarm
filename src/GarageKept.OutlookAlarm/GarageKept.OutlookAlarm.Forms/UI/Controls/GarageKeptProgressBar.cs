using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace GarageKept.OutlookAlarm.Forms.UI.Controls;

public partial class GarageKeptProgressBar : ProgressBar
{
    public GarageKeptProgressBar()
    {
        SetStyle(ControlStyles.UserPaint, true);
    }

    public GarageKeptProgressBar(Color barColor, Color backgroundColor) : this()
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
            cp.Style |= 0x20; // Set the PBS_MARQUEE style
            return cp;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
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
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        SendMessage(Handle, 0x400 + 16, 1, IntPtr.Zero); // Set the PBM_SETMARQUEE message
    }

    [DllImport("user32.dll")]
    [SuppressMessage("Interoperability",
        "SYSLIB1054:Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time",
        Justification = "<Pending>")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
}