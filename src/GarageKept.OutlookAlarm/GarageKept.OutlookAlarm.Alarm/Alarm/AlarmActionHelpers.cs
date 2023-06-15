using System.Reflection;

namespace GarageKept.OutlookAlarm.Alarm.Common;

public static class AlarmActionHelpers
{
    public static string GetEnumDisplayValue(Enum enumValue)
    {
        var enumType = enumValue.GetType();
        var name = Enum.GetName(enumType, enumValue);

        if (name != null)
        {
            var field = enumType.GetField(name);

            var displayAttribute = field?.GetCustomAttribute<DisplayAttribute>();

            return displayAttribute != null ? displayAttribute.Value : name;
        }

        return string.Empty;
    }
}