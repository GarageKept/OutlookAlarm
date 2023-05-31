namespace GarageKept.OutlookAlarm.Forms.Common;

public static class AlarmManager
{
    private static Dictionary<string, Alarm> Alarms { get; } = new();

    public static void AddAlarm(Alarm alarm)
    {
        Alarms[alarm.Id] = alarm;
    }

    public static void RemoveAlarm(Alarm alarm)
    {
        Alarms.Remove(alarm.Id);
    }

    public static void AddAlarm(CalendarEvents appointments)
    {
        foreach (var alarm in appointments.Select(@event => new Alarm(@event))) AddAlarm(alarm);
    }
}