using System.Collections.Concurrent;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Timer = System.Threading.Timer;

namespace GarageKept.OutlookAlarm.Alarm.AlarmManager;

public sealed class OutlookAlarmManager : IAlarmManager
{
    public OutlookAlarmManager(IAlarmSource alarmSource)
    {
        AlarmSource = alarmSource;
        Alarms = new ConcurrentDictionary<string, IAlarm>();
        AlarmTimers = new ConcurrentDictionary<string, Timer>();

        AlarmAdded = delegate { };
        AlarmChanged = delegate { };
        AlarmRemoved = delegate { };
        AlarmsUpdated = delegate { };
    }

    private IAlarmSource AlarmSource { get; }
    private ConcurrentDictionary<string, IAlarm> Alarms { get; }
    private ConcurrentDictionary<string, Timer> AlarmTimers { get; }
    private Timer? UpdateAlarmListTimer { get; set; }

    public event EventHandler<AlarmEventArgs> AlarmAdded;
    public event EventHandler<AlarmEventArgs> AlarmChanged;
    public event EventHandler<AlarmEventArgs> AlarmRemoved;
    public event EventHandler<AlarmEventArgs> AlarmsUpdated;

    public void ChangeAlarmState(IAlarm alarm, AlarmAction action)
    {
        if (!Alarms.ContainsKey(alarm.Id)) return;

        switch (action)
        {
            case AlarmAction.FiveMinBefore:
                AlarmTimers[alarm.Id] = GenerateTimer(alarm, AlarmAction.FiveMinBefore);
                break;
            case AlarmAction.ZeroMinBefore:
                AlarmTimers[alarm.Id] = GenerateTimer(alarm, AlarmAction.ZeroMinBefore);
                break;
            case AlarmAction.SnoozeFiveMin:
                AlarmTimers[alarm.Id] = GenerateTimer(alarm, AlarmAction.SnoozeFiveMin);
                break;
            case AlarmAction.SnoozeTenMin:
                AlarmTimers[alarm.Id] = GenerateTimer(alarm, AlarmAction.SnoozeTenMin);
                break;
            case AlarmAction.Dismiss:
                RemoveAlarmTimer(alarm);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }

    public void DeactivateAlarm(IAlarm alarm)
    {
        var updated = false;

        if (Alarms.TryGetValue(alarm.Id, out var alarmToDeactivate))
        {
            alarmToDeactivate.IsActive = false;
            updated = true;
        }

        if (AlarmTimers.ContainsKey(alarm.Id)) RemoveAlarmTimer(alarm);
        if (updated)
            OnAlarmChanged(new AlarmEventArgs(alarm, AlarmEvent.Updated));
    }

    public void ForceFetch() { FetchAlarms(this); }

    public IEnumerable<IAlarm> GetActiveAlarms() { return Alarms.Values.Where(a => a.IsActive).OrderBy(a => a.Start); }

    public IAlarm? GetCurrentAppointment()
    {
        var now = DateTime.Now;

        var current = Alarms.Values.Where(a => a.IsActive).OrderBy(a => a.Start).FirstOrDefault(a => a.Start < now && a.End > now);

        return current;
    }

    public IAlarm? GetNextAppointment()
    {
        var now = DateTime.Now;

        var current = Alarms.Values.Where(a => a.IsActive).OrderBy(a => a.Start).FirstOrDefault(a => a.Start > now);

        return current;
    }

    public bool IsRunning => UpdateAlarmListTimer != null;

    public void Reset()
    {
        var alarmsToRemove = new List<IAlarm>(Alarms.Count);

        foreach (var alarm in Alarms.Values)
        {
            alarmsToRemove.Add(alarm);

            RemoveAlarmTimer(alarm);
        }

        foreach (var alarm in alarmsToRemove) RemoveAlarm(alarm);

        FetchAlarms(this);
    }

    public void Start() { UpdateAlarmListTimer = new Timer(FetchAlarms, null, TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(Program.AppSettings.AlarmSource.FetchIntervalInMinutes)); }

    public void Stop()
    {
        UpdateAlarmListTimer?.Dispose();
        UpdateAlarmListTimer = null;
    }

    private void AddAlarm(IAlarm alarm)
    {
        if (Alarms.ContainsKey(alarm.Id)) return;

        Alarms.TryAdd(alarm.Id, alarm);

        if (alarm.IsReminderEnabled)
            AddAlarmTimer(alarm);

        OnAlarmAdded(new AlarmEventArgs(alarm, AlarmEvent.Added));
    }

    private void AddAlarmTimer(IAlarm alarm)
    {
        var timer = GenerateTimer(alarm);

        AlarmTimers.TryAdd(alarm.Id, timer);
    }

    private void AlarmCallback(object? state)
    {
        if (state is not IAlarm alarm || !Alarms.ContainsKey(alarm.Id)) return;


        if (!alarm.IsReminderEnabled)
        {
            ChangeAlarmState(alarm, AlarmAction.Dismiss);
        }
        else
        {
            // Launch alarm window or perform related actions
            var alarmWindow = Program.ServiceProvider.GetRequiredService<IAlarmForm>();

            Application.OpenForms[0]?.Invoke(delegate { alarmWindow.Show(alarm); });
        }
    }

    private void FetchAlarms(object? signature)
    {
        // Remove alarms that have ended already
        RemoveOldAlarms();

        // PUll in all alarms and hold their Id
        var alarmsToCheck = GetActiveAlarms().Select(a => a.Id).ToList();

        // Grab all the alarms in the next "FetchTimeInHours" hours
        var alarms = GetAlarmsFromSource(Program.AppSettings.AlarmSource.FetchTimeInHours).ToList();

        // Look for orphans (Those who are in the list but are not in the current/upcoming items
        foreach (var alarm in alarms.Where(alarm => alarmsToCheck.Contains(alarm.Id))) alarmsToCheck.Remove(alarm.Id);

        // if we found an orphan, remove it
        if (alarmsToCheck.Any())
        {
            var removeOrphans = Alarms.Values.Where(a => alarmsToCheck.Contains(a.Id));
            foreach (var alarm in removeOrphans)
            {
                RemoveAlarm(alarm);
                RemoveAlarmTimer(alarm);
            }
        }

        // Not update our list of alarms
        foreach (var alarm in alarms)
            if (Alarms.ContainsKey(alarm.Id))
                UpdateAlarm(alarm);
            else
                AddAlarm(alarm);

        // Let the system know we updated the alarms. This could be triggering a double update.
        OnAlarmsUpdated(new AlarmEventArgs(AlarmEvent.Updated));
    }

    private Timer GenerateTimer(IAlarm alarm, AlarmAction? action = null)
    {
        switch (action)
        {
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
                ChangeAlarmState(alarm, AlarmAction.Dismiss);
                break;
            case null:
                // We do nothing to manipulate the reminder time.
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }

        var timeToLaunch = alarm.ReminderTime - DateTime.Now;

        if (timeToLaunch <= TimeSpan.Zero) timeToLaunch = TimeSpan.FromMicroseconds(1);

        return new Timer(AlarmCallback, alarm, timeToLaunch, Timeout.InfiniteTimeSpan);
    }

    private IEnumerable<IAlarm> GetAlarmsFromSource(int hours)
    {
        var alarms = AlarmSource.GetAlarms(hours).ToList();

        if (hours >= 24) return alarms;

        if (!alarms.Any())
            alarms = GetAlarmsFromSource(hours + Program.AppSettings.AlarmSource.FetchTimeInHours).ToList();

        return alarms;
    }

    private void OnAlarmAdded(AlarmEventArgs e) { AlarmAdded.Invoke(this, e); }

    private void OnAlarmChanged(AlarmEventArgs e) { AlarmChanged.Invoke(this, e); }

    private void OnAlarmRemoved(AlarmEventArgs e) { AlarmRemoved.Invoke(this, e); }

    private void OnAlarmsUpdated(AlarmEventArgs e) { AlarmsUpdated.Invoke(this, e); }

    private void RemoveAlarm(IAlarm alarm)
    {
        Alarms.TryRemove(alarm.Id, out _);

        RemoveAlarmTimer(alarm);

        OnAlarmRemoved(new AlarmEventArgs(alarm, AlarmEvent.Removed));
    }

    private void RemoveAlarmTimer(IAlarm alarm)
    {
        if (!AlarmTimers.ContainsKey(alarm.Id)) return;

        var timer = AlarmTimers[alarm.Id];
        timer.Dispose();
        AlarmTimers.Remove(alarm.Id, out _);
    }

    private void RemoveOldAlarms()
    {
        var now = DateTime.Now;
        var alarmsToRemove = Alarms.Values.Where(a => a.End < now);

        foreach (var alarm in alarmsToRemove)
        {
            RemoveAlarm(alarm);
            RemoveAlarmTimer(alarm);
        }
    }

    private void UpdateAlarm(IAlarm alarm)
    {
        if (!Alarms.ContainsKey(alarm.Id)) return;

        // If the start time didn't change we keep the current enabled state and reminder time
        var sameStart = Alarms[alarm.Id].Start == alarm.Start;
        if (sameStart)
            alarm.IsActive = Alarms[alarm.Id].IsActive;

        Alarms[alarm.Id] = alarm;


        if (alarm.IsReminderEnabled)
        {
            if (!sameStart)
                UpdateAlarmTimer(alarm);
        }
        else
        {
            RemoveAlarmTimer(alarm);
        }

        OnAlarmChanged(new AlarmEventArgs(alarm, AlarmEvent.Updated));
    }

    private void UpdateAlarmTimer(IAlarm alarm)
    {
        if (!AlarmTimers.ContainsKey(alarm.Id)) return;

        var timer = AlarmTimers[alarm.Id];
        timer.Dispose();
        timer = GenerateTimer(alarm);
        AlarmTimers[alarm.Id] = timer;
    }
}