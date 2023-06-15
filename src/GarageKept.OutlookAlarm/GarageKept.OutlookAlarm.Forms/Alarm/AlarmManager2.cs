using System.Diagnostics;
using GarageKept.OutlookAlarm.Forms.AlarmSources;
using GarageKept.OutlookAlarm.Forms.Common;
using GarageKept.OutlookAlarm.Forms.Interfaces;
using GarageKept.OutlookAlarm.Forms.UI.Forms;
using Timer = System.Threading.Timer;

namespace GarageKept.OutlookAlarm.Forms.Alarm;

public class AlarmManager2 : IAlarmManager
{
    public AlarmManager2(ISettings appSettings)
    {
        AlarmTimers = new Dictionary<string, Timer>();
        Alarms = new List<IAlarm>();
        AlarmSource = new OutlookAlarmSource();
        AppSettings = appSettings;
        IsRunning = false;
    }

    private ISettings AppSettings { get; }
    private IAlarmSource AlarmSource { get; }
    private List<IAlarm> Alarms { get; }
    private Dictionary<string, Timer> AlarmTimers { get; }
    private Timer? UpdateAlarmListTimer { get; set; }

    public bool IsRunning { get; set; }

    public void AddAlarm(IAlarm alarm)
    {
        Alarms.Add(alarm);

        if (!alarm.IsEnabled) return;

        var timer = new Timer(AlarmTriggered, alarm.Id, DateTime.Now - alarm.ReminderTime, Timeout.InfiniteTimeSpan);
        var added = AlarmTimers.TryAdd(alarm.Id, timer);

        Debug.WriteLineIf(!added, "Alarm not added");
    }

    public void RemoveAlarm(IAlarm alarm)
    {
        if (Alarms.Any(a => a.Id == alarm.Id))
            Alarms.Remove(alarm);

        RemoveAlarmTimers(alarm.Id);
    }

    public void Start()
    {
        UpdateAlarmListTimer = new Timer(UpdateAlarms, null, 0, AppSettings.FetchTime * 60000);

        IsRunning = true;
    }

    public void Stop()
    {
        UpdateAlarmListTimer?.Dispose();

        AlarmSource.Stop();
        IsRunning = false;
    }

    private void RemoveAlarmTimers(string alarmId)
    {
        if (!AlarmTimers.ContainsKey(alarmId)) return;

        var timer = AlarmTimers[alarmId];
        timer.Dispose();
        AlarmTimers.Remove(alarmId);
    }

    private void AlarmTriggered(object? id)
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

    private void AlarmFormCallback(AlarmAction action, string id)
    {
    }

    private void UpdateAlarms(object? state)
    {
        var alarms = GetAlarms(2);
        AddAlarm(alarms);
    }

    private void AddAlarm(List<IAlarm> alarms)
    {
        foreach (var alarm in alarms) AddAlarm(alarm);
    }

    private List<IAlarm> GetAlarms(int hours)
    {
        var alarms = AlarmSource.GetAlarms(hours);

        if (alarms.Count == 0 || hours >= 24)
            alarms = GetAlarms(hours + 2);

        return alarms;
    }
}