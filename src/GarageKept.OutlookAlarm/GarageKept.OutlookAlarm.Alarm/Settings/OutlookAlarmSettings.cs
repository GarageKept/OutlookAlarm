using System.Text.Json;
using System.Text.Json.Serialization;
using GarageKept.OutlookAlarm.Alarm.Interfaces;

namespace GarageKept.OutlookAlarm.Alarm.Settings;

/// <summary>
///     Represents the application settings.
///     Provides methods to save and load settings from a JSON file.
/// </summary>
public class OutlookAlarmSettings : ISettings
{
    private const string SettingsFilePath = "settings.json";

    public OutlookAlarmSettings() : this(false) { LoadOrCreate(); }

    [JsonConstructor]
    public OutlookAlarmSettings(bool isDeserializing = true)
    {
        // This constructor is used by the JSON deserializer.

        Alarm = new AlarmSettings(Save, isDeserializing);
        AlarmSource = new AlarmSourceSettings(Save, isDeserializing);
        Audio = new AudioSettings(Save, isDeserializing);
        Color = new ColorSettings(Save, isDeserializing);
        Main = new MainSettings(Save, isDeserializing);
    }

    public AlarmSettings Alarm { get; set; }
    public AlarmSourceSettings AlarmSource { get; set; }
    public AudioSettings Audio { get; set; }
    public ColorSettings Color { get; set; }
    public MainSettings Main { get; set; }

    internal static OutlookAlarmSettings FromJsonElement(JsonElement element, bool loaded)
    {
        var settings = new OutlookAlarmSettings(loaded);

        var options = GetJsonSerializeOptions();

        foreach (var property in typeof(OutlookAlarmSettings).GetProperties())
        {
            if (!property.CanRead || !property.CanWrite)
                continue;

            if (!element.TryGetProperty(property.Name, out var propertyValue))
                continue;

            var value = JsonSerializer.Deserialize(propertyValue.GetRawText(), property.PropertyType, options);
            property.SetValue(settings, value);
        }

        return settings;
    }

    private static JsonSerializerOptions GetJsonSerializeOptions() { return new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters = { new ColorJsonConverter(), new SettingsJsonConverter() } }; }

    /// <summary>
    ///     Loads the settings from a JSON file or creates a new file if it does not exist.
    /// </summary>
    public void LoadOrCreate()
    {
        if (!File.Exists(SettingsFilePath))
        {
            Save();
        }
        else
        {
            var settingsJson = File.ReadAllText(SettingsFilePath);

            if (string.IsNullOrEmpty(settingsJson))
                return;

            var options = GetJsonSerializeOptions();

            var savedSettings = JsonSerializer.Deserialize<OutlookAlarmSettings>(settingsJson, options);

            if (savedSettings == null)
                return;

            foreach (var property in typeof(OutlookAlarmSettings).GetProperties())
            {
                if (!property.CanRead || !property.CanWrite)
                    continue;

                var savedValue = property.GetValue(savedSettings);
                property.SetValue(this, savedValue);
            }
        }
    }

    /// <summary>
    ///     Saves the settings to a JSON file.
    /// </summary>
    protected void Save()
    {
        var options = new JsonSerializerOptions { WriteIndented = true, Converters = { new ColorJsonConverter() } };

        var settingsJson = JsonSerializer.Serialize(this, options);
        File.WriteAllText(SettingsFilePath, settingsJson);
    }
}