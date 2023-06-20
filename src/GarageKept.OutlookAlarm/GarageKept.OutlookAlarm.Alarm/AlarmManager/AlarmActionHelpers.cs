using System.Reflection;

namespace GarageKept.OutlookAlarm.Alarm.AlarmManager;

public static class AlarmActionHelpers
{
    public static string GetEnumDisplayValue(Enum enumValue)
    {
        var enumType = enumValue.GetType();
        var name = Enum.GetName(enumType, enumValue);

        if (name == null) return string.Empty;

        var field = enumType.GetField(name);

        var displayAttribute = field?.GetCustomAttribute<DisplayAttribute>();

        return displayAttribute != null ? displayAttribute.Value : name;

    }
}