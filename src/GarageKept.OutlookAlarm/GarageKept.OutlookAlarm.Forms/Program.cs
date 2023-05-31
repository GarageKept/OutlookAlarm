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

    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // Load or create the settings for the application.
        ApplicationSettings = Settings.LoadOrCreate();

        // Customize application configuration (such as set high DPI settings or default font).
        // For more details, refer to https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        Application.EnableVisualStyles();

        Application.SetCompatibleTextRenderingDefault(false);

        // Start the application with the main form.
        Application.Run(new MainForm());
    }
}