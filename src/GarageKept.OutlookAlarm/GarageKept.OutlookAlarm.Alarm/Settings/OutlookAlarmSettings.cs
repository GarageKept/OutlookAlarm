using System.Text.Json;
using System.Text.Json.Serialization;
using Accessibility;
using GarageKept.OutlookAlarm.Alarm.Audio;
using GarageKept.OutlookAlarm.Alarm.Interfaces;

namespace GarageKept.OutlookAlarm.Alarm.Settings;

/// <summary>
///     Represents the application settings.
///     Provides methods to save and load settings from a JSON file.
/// </summary>
public class OutlookAlarmSettings : ISettings
{
    private const string SettingsFilePath = "settings.json";

    /// <summary>
    ///     Initializes a new instance of the <see cref="OutlookAlarmSettings" /> class with default values.
    /// </summary>
    public OutlookAlarmSettings()
    {
        LoadOrCreate();
    }

    [JsonConstructor]
    public OutlookAlarmSettings(bool loaded)
    {
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
    public int TurnOffAlarmAfterStart { get; set; } = 15;
    public AlarmSettings AlarmSettings { get; set; } = new AlarmSettings();

    /// <summary>
    ///     Saves the current instance of the <see cref="OutlookAlarmSettings" /> object to the settings file.
    /// </summary>
    public void Save()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new ColorJsonConverter() }
        };

        var settingsJson = JsonSerializer.Serialize(this, options);
        File.WriteAllText(SettingsFilePath, settingsJson);
    }

    /// <summary>
    ///     Loads the settings from the settings file.
    ///     If the file doesn't exist, it creates a new file with default settings.
    /// </summary>
    /// <returns>A <see cref="OutlookAlarmSettings" /> object representing the loaded or default settings.</returns>
    public void LoadOrCreate()
    {
        if (!File.Exists(SettingsFilePath))
        {
            Save();
        }
        else
        {
            var settingsJson = File.ReadAllText(SettingsFilePath);

            if (string.IsNullOrEmpty(settingsJson)) return;

            var options = GetJsonSerializeOptions();

            var savedSettings = JsonSerializer.Deserialize<OutlookAlarmSettings>(settingsJson, options);

            // Copy properties from savedSettings to this using reflection
            if (savedSettings == null) return;

            foreach (var property in typeof(OutlookAlarmSettings).GetProperties())
            {
                if (!property.CanRead || !property.CanWrite) continue;
                var savedValue = property.GetValue(savedSettings);
                property.SetValue(this, savedValue);
            }
        }
    }

    private static JsonSerializerOptions GetJsonSerializeOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new ColorJsonConverter(), new SettingsJsonConverter() }
        };
    }

    internal static OutlookAlarmSettings FromJsonElement(JsonElement element, bool loaded)
    {
        var settings = new OutlookAlarmSettings(loaded);

        // Create and configure JsonSerializerOptions
        var options = GetJsonSerializeOptions();

        foreach (var property in typeof(OutlookAlarmSettings).GetProperties())
        {
            if (!property.CanRead || !property.CanWrite) continue;
            if (!element.TryGetProperty(property.Name, out var propertyValue)) continue;

            var value = JsonSerializer.Deserialize(propertyValue.GetRawText(), property.PropertyType, options);
            property.SetValue(settings, value);
        }

        return settings;
    }

}

public class AlarmSettings
{
    private int _left = -1;
    public Color AlarmPastStartColor { get; } = Color.Red;
    public string TimeLeftStringFormat { get; } = "{0:%h}h {0:mm}m {0:ss}s";
    public string AlarmStartStringFormat { get; } = "hh:mm tt";

    public int Left
    {
        get
        {
            if (_left < 0) return Screen.PrimaryScreen?.Bounds.Width/2 ?? 0;
            return _left;
        }
        set => _left = value;
    }

    public int Top { get; set; } = 0;
}