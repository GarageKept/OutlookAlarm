using System.Text.Json.Serialization;

namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class SettingsBase
{
    internal readonly Action Save;

    protected SettingsBase(Action save, bool isDeserializing)
    {
        Save = save;
        IsDeserializing = true;
    }


    [JsonIgnore] // Optional: Ignore this property during JSON serialization/deserialization
    internal bool IsDeserializing { get; set; }
}