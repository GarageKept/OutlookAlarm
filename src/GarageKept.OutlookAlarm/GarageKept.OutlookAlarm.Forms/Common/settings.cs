using System.Text.Json;

namespace GarageKept.OutlookAlarm.Forms.Common;

/// <summary>
///     Represents the application settings.
///     Provides methods to save and load settings from a JSON file.
/// </summary>
internal class Settings
{
    private const string SettingsFilePath = "settings.json";

    /// <summary>
    ///     Initializes a new instance of the <see cref="Settings" /> class with default values.
    /// </summary>
    public Settings()
    {
        AlarmWarningTime = 15;
        BarSize = 10;
        GreenColor = Color.Green;
        FetchTime = 2;
        Left = 0;
        RedColor = Color.Red;
        RefreshRate = 1;
        SliderSpeed = 5;
        TimeFormat = "hh:mm:ss";
        TimeLeftStringFormat = "{0:%h}h {0:mm}m {0:ss}s";
        YellowColor = Color.Yellow;
    }

    public Color GreenColor { get; set; }
    public Color RedColor { get; set; }
    public Color YellowColor { get; set; }
    public int AlarmWarningTime { get; set; }
    public int BarSize { get; internal set; }
    public int FetchTime { get; set; }
    public int Left { get; set; }
    public int RefreshRate { get; set; }
    public int SliderSpeed { get; set; }
    public string TimeFormat { get; set; }
    public string TimeLeftStringFormat { get; set; }

    public DateTime QuietHoursStart { get; set; }
    public DateTime QuietHoursEnd { get; set; }

    /// <summary>
    ///     Loads the settings from the settings file.
    ///     If the file doesn't exist, it creates a new file with default settings.
    /// </summary>
    /// <returns>A <see cref="Settings" /> object representing the loaded or default settings.</returns>
    public static Settings LoadOrCreate()
    {
        var settings = new Settings();

        if (!File.Exists(SettingsFilePath))
        {
            settings.Save();
            return settings;
        }

        var settingsJson = File.ReadAllText(SettingsFilePath);

        if (string.IsNullOrEmpty(settingsJson)) return settings;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new ColorJsonConverter() }
        };

        return JsonSerializer.Deserialize<Settings>(settingsJson, options) ?? settings;
    }

    /// <summary>
    ///     Saves the current instance of the <see cref="Settings" /> object to the settings file.
    /// </summary>
    public void Save()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new ColorJsonConverter() }
        };

        var settingsJson = JsonSerializer.Serialize(this, options);
        File.WriteAllText(SettingsFilePath, settingsJson);
    }
}