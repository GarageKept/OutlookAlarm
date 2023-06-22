using System.Text.Json;
using GarageKept.OutlookAlarm.Alarm.Interfaces;

namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class OutlookAlarmSettings : ISettings
{
    public OutlookAlarmSettings()
    {
        Alarm = new AlarmSettings(Save);
        AlarmSource = new AlarmSourceSettings(Save);
        Audio = new AudioSettings(Save);
        Color = new ColorSettings(Save);
        Main = new MainSettings(Save);
        TimeManagement = new TimeManagementSettings(Save);
    }

    public AlarmSettings Alarm { get; set; }
    public AlarmSourceSettings AlarmSource { get; set; }
    public AudioSettings Audio { get; set; }
    public ColorSettings Color { get; set; }
    public MainSettings Main { get; set; }
    public TimeManagementSettings TimeManagement { get; set; }

    private static string GetAppFolder()
    {
        const string appFolder = @"GarageKept\OutlookAlarm\";
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var settingsFilePath = Path.Combine(appDataPath, appFolder);

        if (!Directory.Exists(settingsFilePath)) Directory.CreateDirectory(settingsFilePath);

        return settingsFilePath;
    }

    private static JsonSerializerOptions GetJsonSerializeOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, WriteIndented = true, Converters = { new ColorJsonConverter() }
        };
    }

    private static string GetSettingsFile()
    {
        const string settingsFilePath = @"settings.json";

        var fullPath = Path.Combine(GetAppFolder(), settingsFilePath);

        return fullPath;
    }

    public static OutlookAlarmSettings Load()
    {
        OutlookAlarmSettings? settings;

        if (File.Exists(GetSettingsFile()))
        {
            var settingsJson = File.ReadAllText(GetSettingsFile());

            if (string.IsNullOrWhiteSpace(settingsJson)) return new OutlookAlarmSettings();

            var options = new JsonSerializerOptions { WriteIndented = true, Converters = { new ColorJsonConverter() } };

            // Use the JsonSerializer to deserialize the settings.
            settings = JsonSerializer.Deserialize<OutlookAlarmSettings>(settingsJson, options) ??
                       new OutlookAlarmSettings();

            settings.Alarm.Save = settings.Save;
            settings.AlarmSource.Save = settings.Save;
            settings.Audio.Save = settings.Save;
            settings.Audio.Save = settings.Save;
            settings.Color.Save = settings.Save;
            settings.Main.Save = settings.Save;
            settings.TimeManagement.Save = settings.Save;
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
        File.WriteAllText(GetSettingsFile(), settingsJson);
    }
}