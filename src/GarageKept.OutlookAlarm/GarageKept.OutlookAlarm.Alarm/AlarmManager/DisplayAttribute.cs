namespace GarageKept.OutlookAlarm.Alarm.AlarmManager;

public class DisplayAttribute : Attribute
{
    public DisplayAttribute(string value) { Value = value; }

    public string Value { get; }
}