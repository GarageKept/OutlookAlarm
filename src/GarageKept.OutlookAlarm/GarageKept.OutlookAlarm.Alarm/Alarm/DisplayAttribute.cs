namespace GarageKept.OutlookAlarm.Alarm.Common;

public class DisplayAttribute : Attribute
{
    public DisplayAttribute(string value)
    {
        Value = value;
    }

    public string Value { get; }
}