using System.Diagnostics;
using GarageKept.OutlookAlarm.Forms.AlarmSources;
using GarageKept.OutlookAlarm.Forms.Common;
using GarageKept.OutlookAlarm.Forms.Interfaces;
using GarageKept.OutlookAlarm.Forms.UI.Forms;
using Timer = System.Threading.Timer;

namespace GarageKept.OutlookAlarm.Forms.Alarm;

public static class AlarmManager2
{
    static AlarmManager2()
    {
        AlarmTimers = new Dictionary<string, Timer>();
        Alarms = new List<IAlarm>();
        AlarmSource = new OutlookAlarmSource();
        IsRunning = false;
    }

    private static IAlarmSource AlarmSource { get; }
    private static List<IAlarm> Alarms { get; }
    private static Dictionary<string, Timer> AlarmTimers { get; }
    private static Timer? UpdateAlarmListTimer { get; set; }

    public static bool IsRunning { get; set; }

    public static void AddAlarm(IAlarm alarm)
    {
        Alarms.Add(alarm);

        if (!alarm.IsEnabled) return;

        var timer = new Timer(AlarmTriggered, alarm.Id, DateTime.Now - alarm.ReminderTime, Timeout.InfiniteTimeSpan);
        var added = AlarmTimers.TryAdd(alarm.Id, timer);

        Debug.WriteLineIf(!added, "Alarm not added");
    }

    public static void RemoveAlarm(IAlarm alarm)
    {
        if (Alarms.Any(a => a.Id == alarm.Id))
            Alarms.Remove(alarm);

        RemoveAlarmTimers(alarm.Id);
    }

    private static void RemoveAlarmTimers(string alarmId)
    {
        if (!AlarmTimers.ContainsKey(alarmId)) return;

        var timer = AlarmTimers[alarmId];
        timer.Dispose();
        AlarmTimers.Remove(alarmId);
    }

    private static void AlarmTriggered(object? id)
    {
        if (id is string alarmId)
            if (Alarms.Any(a => a.Id == alarmId))
            {
                // We have the alarm
                var alarm = Alarms.First(a => a.Id == alarmId);

                var alarmWindows = new AlarmWindow(alarm, AlarmFormCallback);
            }

        // verify launch time has been reached
        // if so, launch alarm window
        // if not reset timer to fire at correct time
    }

    private static void AlarmFormCallback(AlarmAction action, string id)
    {
    }

    public static void Start()
    {
        UpdateAlarmListTimer = new Timer(UpdateAlarms, null, 0, Program.ApplicationSettings.FetchTime * 60000);

        IsRunning = true;
    }

    private static void UpdateAlarms(object? state)
    {
        var alarms = GetAlarms(2);
        AddAlarm(alarms);
    }

    private static void AddAlarm(List<IAlarm> alarms)
    {
        foreach (var alarm in alarms) AddAlarm(alarm);
    }

    private static List<IAlarm> GetAlarms(int hours)
    {
        var alarms = AlarmSource.GetAlarms(hours);

        if (alarms.Count == 0 || hours >= 24)
            alarms = GetAlarms(hours + 2);

        return alarms;
    }

    public static void Stop()
    {
        UpdateAlarmListTimer?.Dispose();

        AlarmSource.Stop();
        IsRunning = false;
    }
}