using System.Collections.Concurrent;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Timer = System.Threading.Timer;

namespace GarageKept.OutlookAlarm.Alarm.AlarmManager;

public sealed class OutlookAlarmManager : IAlarmManager
{
    public OutlookAlarmManager(IAlarmSource alarmSource, ISettings settings)
    {
        Alarms = new ConcurrentDictionary<string, IAlarm>();
        AlarmSource = alarmSource;
        AlarmTimers = new ConcurrentDictionary<string, Timer>();
        AlarmsUpdatedCallback = delegate { };
        Settings = settings;
    }

    private IAlarmSource AlarmSource { get; }
    private ConcurrentDictionary<string, IAlarm> Alarms { get; }
    private ConcurrentDictionary<string, Timer> AlarmTimers { get; }
    private ISettings Settings { get; }
    private Timer? UpdateAlarmListTimer { get; set; }

    public IAlarmManager.UpdateAlarmList AlarmsUpdatedCallback { get; set; }

    public void ChangeAlarmState(IAlarm alarm, AlarmAction action)
    {
        if (!Alarms.ContainsKey(alarm.Id)) return;

        switch (action)
        {
            case AlarmAction.FifteenMinBefore:
                AlarmTimers[alarm.Id] = UpdateReminderTime(alarm, AlarmAction.FifteenMinBefore);
                break;
            case AlarmAction.TenMinBefore:
                AlarmTimers[alarm.Id] = UpdateReminderTime(alarm, AlarmAction.TenMinBefore);
                break;
            case AlarmAction.FiveMinBefore:
                AlarmTimers[alarm.Id] = UpdateReminderTime(alarm, AlarmAction.FiveMinBefore);
                break;
            case AlarmAction.ZeroMinBefore:
                AlarmTimers[alarm.Id] = UpdateReminderTime(alarm, AlarmAction.ZeroMinBefore);
                break;
            case AlarmAction.SnoozeFiveMin:
                AlarmTimers[alarm.Id] = UpdateReminderTime(alarm, AlarmAction.SnoozeFiveMin);
                break;
            case AlarmAction.SnoozeTenMin:
                AlarmTimers[alarm.Id] = UpdateReminderTime(alarm, AlarmAction.SnoozeTenMin);
                break;
            case AlarmAction.Dismiss:
                RemoveAlarmTimer(alarm);
                break;
            case AlarmAction.Remove:
                DeactivateAlarm(alarm);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }

    public void ForceFetch() { FetchAlarms(null); }

    public void Reset()
    {
        foreach (var timer in AlarmTimers.Values)
        {
            timer.Dispose();
        }

        Alarms.Clear();
        AlarmTimers.Clear();

        FetchAlarms(this);
    }

    public void Start()
    {
        UpdateAlarmListTimer = new Timer(FetchAlarms, null, TimeSpan.FromSeconds(1),
            TimeSpan.FromMinutes(Settings.AlarmSource.FetchIntervalInMinutes));
    }

    public void Stop()
    {
        UpdateAlarmListTimer?.Dispose();
        UpdateAlarmListTimer = null;
    }

    public void Dispose() { Stop(); }

    private void AddAlarm(IAlarm alarm)
    {
        Alarms.TryAdd(alarm.Id, alarm);

        if (alarm.IsReminderEnabled)
            AddAlarmTimer(alarm);
    }

    private void AddAlarmTimer(IAlarm alarm)
    {
        var timer = GenerateTimer(alarm);

        AlarmTimers.TryAdd(alarm.Id, timer);
    }

    private void AlarmCallback(object? state)
    {
        if (state is null) return;

        if (state is not IAlarm alarm) return;

        if (alarm.IsReminderEnabled)
        {
            // Launch alarm window or perform related actions
            // Launch alarm window or perform related actions
            var alarmWindow = Program.ServiceProvider?.GetRequiredService<IAlarmForm>();
            Application.OpenForms[0]?.Invoke(delegate { alarmWindow?.Show(alarm); });
        }
        else
        {
            ChangeAlarmState(alarm, AlarmAction.Dismiss);
        }
    }

    private void DeactivateAlarm(IAlarm alarm)
    {
        if (!Alarms.ContainsKey(alarm.Id)) return;

        alarm.IsActive = false;

        UpdateAlarm(alarm);

        RemoveAlarmTimer(Alarms[alarm.Id]);

        OnAlarmsUpdated();
    }

    private void FetchAlarms(object? signature)
    {
        // Remove alarms that have ended already
        RemoveOldAlarms();

        // Pull in all alarms and hold their Id
        var alarmsToCheck = GetActiveAlarms().Select(a => a.Id).ToList();

        // Grab all the alarms in the next "FetchTimeInHours" hours
        var alarms = GetAlarmsFromSource(Settings.AlarmSource.FetchTimeInHours).ToList();

        // Look for orphans (Those who are in the list but are not in the current/upcoming items
        // These are items that have been removed by Outlook, i.e. moved or deleted.
        foreach (var alarm in alarms.Where(alarm => alarmsToCheck.Contains(alarm.Id))) alarmsToCheck.Remove(alarm.Id);

        // if we found an orphan, remove it
        if (alarmsToCheck.Any())
        {
            var removeOrphans = Alarms.Values.Where(a => alarmsToCheck.Contains(a.Id));
            foreach (var alarm in removeOrphans)
            {
                DeactivateAlarm(alarm);
                RemoveAlarmTimer(alarm);
            }
        }

        var hasUpdates = false;

        // Not update our list of alarms
        foreach (var alarm in alarms)
            if (Alarms.ContainsKey(alarm.Id))
            {
                hasUpdates = UpdateAlarm(alarm) || hasUpdates;
            }
            else
            {
                AddAlarm(alarm);
                hasUpdates = true;
            }

        if (hasUpdates)
            OnAlarmsUpdated();
    }

    private Timer GenerateTimer(IAlarm alarm)
    {
        var timeToLaunch = alarm.ReminderTime - DateTime.Now;

        if (timeToLaunch <= TimeSpan.Zero) timeToLaunch = TimeSpan.FromMicroseconds(1);

        if (timeToLaunch <= TimeSpan.Zero) timeToLaunch = TimeSpan.FromMicroseconds(1);

        return new Timer(AlarmCallback, alarm, timeToLaunch, Timeout.InfiniteTimeSpan);
    }

    public IEnumerable<IAlarm> GetActiveAlarms() { return Alarms.Values.Where(a => a.IsActive).OrderBy(a => a.Start); }

    private IEnumerable<IAlarm> GetAlarmsFromSource(int hours)
    {
        var alarms = AlarmSource.GetAlarms(hours).ToList();

        if (hours >= 24) return alarms;

        // Remove deactivated Alarms
        var deactivatedAlarms = Alarms.Values.Where(a => !a.IsActive).ToList();
        foreach (var remove in deactivatedAlarms
                     .Select(deactivatedAlarm =>
                         alarms.FirstOrDefault(a => a.Id == deactivatedAlarm.Id && a.Start == deactivatedAlarm.Start))
                     .Where(remove => remove != null))
            if (remove != null)
                alarms.Remove(remove);

        // If we have no more alarms or never did, go get some more (Until we hit 24 hours)
        if (!alarms.Any()) alarms = GetAlarmsFromSource(hours + Settings.AlarmSource.FetchTimeInHours).ToList();

        return alarms;
    }

    private void OnAlarmsUpdated() { AlarmsUpdatedCallback.Invoke(Alarms.Values.Where(a => a.IsActive)); }

    private void RemoveAlarmTimer(IAlarm alarm)
    {
        if (!AlarmTimers.ContainsKey(alarm.Id)) return;

        var timer = AlarmTimers[alarm.Id];
        timer.Dispose();
        AlarmTimers.Remove(alarm.Id, out _);

        if(!Alarms.Values.Any(a =>a.IsActive)) FetchAlarms(this);
    }

    private void RemoveOldAlarms()
    {
        var now = DateTime.Now;
        var alarmsToRemove = Alarms.Values.Where(a => a.End < now);

        foreach (var alarm in alarmsToRemove)
        {
            DeactivateAlarm(alarm);
            RemoveAlarmTimer(alarm);
        }
    }

    private bool UpdateAlarm(IAlarm alarm)
    {
        var existing = Alarms.Values.First(a => a.Id == alarm.Id);

        if (AlarmComparer.AreEqual(existing, alarm)) return false;

        Alarms[alarm.Id] = alarm;

        return true;
    }

    private Timer UpdateReminderTime(IAlarm alarm, AlarmAction action)
    {
        switch (action)
        {
            case AlarmAction.FifteenMinBefore:
                alarm.ReminderTime = alarm.Start + TimeSpan.FromMinutes(-15);
                break;
            case AlarmAction.TenMinBefore:
                alarm.ReminderTime = alarm.Start + TimeSpan.FromMinutes(-10);
                break;
            case AlarmAction.FiveMinBefore:
                alarm.ReminderTime = alarm.Start + TimeSpan.FromMinutes(-5);
                break;
            case AlarmAction.ZeroMinBefore:
                alarm.ReminderTime = alarm.Start;
                break;
            case AlarmAction.SnoozeFiveMin:
                alarm.ReminderTime = DateTime.Now + TimeSpan.FromMinutes(5);
                break;
            case AlarmAction.SnoozeTenMin:
                alarm.ReminderTime = DateTime.Now + TimeSpan.FromMinutes(10);
                break;
            case AlarmAction.Dismiss:
            case AlarmAction.Remove:
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }

        return GenerateTimer(alarm);
    }
}