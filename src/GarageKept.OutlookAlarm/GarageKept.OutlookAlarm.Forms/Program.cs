using GarageKept.OutlookAlarm.Forms.Common;
using GarageKept.OutlookAlarm.Forms.UI.Forms;

namespace GarageKept.OutlookAlarm.Forms;

/// <summary>
///     Defines the entry point of the application with associated settings.
/// </summary>
internal static class Program
{
    /// <summary>
    ///     Gets or sets the application settings.
    /// </summary>
    public static Settings ApplicationSettings { get; set; } = new();

    public static MainForm? MainWindow { get; set; }

    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // Load or create the settings for the application.
        ApplicationSettings = Settings.LoadOrCreate();

        ApplicationConfiguration.Initialize();

        Application.EnableVisualStyles();

        Application.SetCompatibleTextRenderingDefault(false);

        MainWindow = new MainForm();

        // Start the application with the main form.
        Application.Run(MainWindow);
    }
}