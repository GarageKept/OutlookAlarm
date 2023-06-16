using System.Text.Json;
using System.Text.Json.Serialization;

namespace GarageKept.OutlookAlarm.Alarm.Settings;

internal class SettingsJsonConverter : JsonConverter<OutlookAlarmSettings>
{
    public override OutlookAlarmSettings Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        return OutlookAlarmSettings.FromJsonElement(jsonDoc.RootElement, true);
    }

    public override void Write(Utf8JsonWriter writer, OutlookAlarmSettings value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}