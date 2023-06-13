namespace GarageKept.OutlookAlarm.Forms.Common;

public static class AlarmManager
{
    private static Dictionary<string, Alarm> Alarms { get; } = new();

    public static void AddAlarm(Alarm alarm)
    {
        if (alarm.Appointment != null)
            Alarms[alarm.Appointment.Id] = alarm;
    }

    public static void RemoveAlarm(Alarm alarm)
    {
        if (alarm.Appointment != null)
            Alarms.Remove(alarm.Appointment.Id);
    }

    public static void AddAlarm(CalendarEvents appointments)
    {
        foreach (var alarm in appointments.Values.Select(@event => new Alarm(@event))) AddAlarm(alarm);
    }

    public static void RemoveAlarm(string id)
    {
        Alarms.Remove(id);
    }

    public static void DismissAlarm(string id)
    {
        if (Alarms.TryGetValue(id, out var alarm))
        {
            alarm.State = AlarmState.Dismissed;
        }
    }

    public static void ResetAll()
    {
        foreach (var item in Alarms.Values)
        {
            item.Reset();
        }
    }
}