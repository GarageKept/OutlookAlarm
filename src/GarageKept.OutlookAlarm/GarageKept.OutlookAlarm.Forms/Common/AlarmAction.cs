using System.Reflection;

namespace GarageKept.OutlookAlarm.Forms.Common;

public enum AlarmAction
{
    [Display("5 Minutes Before Start")] FiveMinBefore,
    [Display("0 Minutes Before Start")] ZeroMinBefore,
    [Display("5 Minutes")] SnoozeFiveMin,
    [Display("10 Minutes")] SnoozeTenMin,
    [Display("Dismissed")] Dismiss
}

public class DisplayAttribute : Attribute
{
    public DisplayAttribute(string value)
    {
        Value = value;
    }

    public string Value { get; }
}

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