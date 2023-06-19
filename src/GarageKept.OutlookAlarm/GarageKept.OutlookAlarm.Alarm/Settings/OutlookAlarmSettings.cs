using System.Text.Json;
using GarageKept.OutlookAlarm.Alarm.Interfaces;

namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class OutlookAlarmSettings : ISettings
{
    private const string SettingsFilePath = "settings.json";

    // Make the default constructor private so it can't be called externally.
    public OutlookAlarmSettings()
    {
        Alarm = new AlarmSettings(Save);
        AlarmSource = new AlarmSourceSettings(Save);
        Audio = new AudioSettings(Save);
        Color = new ColorSettings(Save);
        Main = new MainSettings(Save);
    }

    public AlarmSettings Alarm { get; set; }
    public AlarmSourceSettings AlarmSource { get; set; }
    public AudioSettings Audio { get; set; }
    public ColorSettings Color { get; set; }
    public MainSettings Main { get; set; }

    private static JsonSerializerOptions GetJsonSerializeOptions() { return new JsonSerializerOptions { PropertyNameCaseInsensitive = true, WriteIndented = true, Converters = { new ColorJsonConverter() } }; }

    public static OutlookAlarmSettings Load()
    {
        OutlookAlarmSettings? settings;

        if (File.Exists(SettingsFilePath))
        {
            var settingsJson = File.ReadAllText(SettingsFilePath);

            if (string.IsNullOrWhiteSpace(settingsJson)) return new OutlookAlarmSettings();

            var options = new JsonSerializerOptions { WriteIndented = true, Converters = { new ColorJsonConverter() } };

            // Use the JsonSerializer to deserialize the settings.
            settings = JsonSerializer.Deserialize<OutlookAlarmSettings>(settingsJson, options) ?? new OutlookAlarmSettings();

            settings.Alarm.Save = settings.Save;
            settings.AlarmSource.Save = settings.Save;
            settings.Audio.Save = settings.Save;
            settings.Audio.Save = settings.Save;
            settings.Color.Save = settings.Save;
            settings.Main.Save = settings.Save;
        }
        else
        {
            // If the settings file doesn't exist, create a new settings object.
            settings = new OutlookAlarmSettings();
            settings.Save();
        }

        return settings;
    }

    public void Save()
    {
        var options = GetJsonSerializeOptions();
        var settingsJson = JsonSerializer.Serialize(this, options);
        File.WriteAllText(SettingsFilePath, settingsJson);
    }
}