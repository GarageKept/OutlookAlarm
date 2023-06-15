using System.Text.Json;
using GarageKept.OutlookAlarm.Alarm.Audio;
using GarageKept.OutlookAlarm.Alarm.Interfaces;

namespace GarageKept.OutlookAlarm.Alarm.Common;

/// <summary>
///     Represents the application settings.
///     Provides methods to save and load settings from a JSON file.
/// </summary>
internal class Settings : ISettings
{
    private const string SettingsFilePath = "settings.json";

    /// <summary>
    ///     Initializes a new instance of the <see cref="Settings" /> class with default values.
    /// </summary>
    public Settings()
    {
        //  LoadOrCreate();
    }

    public Color GreenColor { get; set; } = Color.Green;
    public Color RedColor { get; set; } = Color.Red;
    public Color YellowColor { get; set; } = Color.Yellow;
    public int Left { get; set; }
    public int SliderSpeed { get; set; } = 5;
    public string TimeFormat { get; set; } = "hh:mm:ss";
    public string TimeLeftStringFormat { get; set; } = "{0:%h}h {0:mm}m {0:ss}s";
    public int FetchRate { get; set; } = 5000;
    public int FetchTime { get; set; } = 1;
    public int AlarmWarningTime { get; set; } = 15;
    public int BarSize { get; internal set; } = 10;
    public SoundType DefaultSound { get; set; } = SoundType.Warning0;

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
}