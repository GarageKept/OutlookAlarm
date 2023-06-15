using GarageKept.OutlookAlarm.Alarm.Common;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Timer = System.Threading.Timer;

namespace GarageKept.OutlookAlarm.Alarm.Alarm;

public class AlarmManager : IAlarmManager
{
    public AlarmManager(ISettings appSettings, IAlarmSource alarmSource)
    {
        AppSettings = appSettings;
        AlarmSource = alarmSource;
        Alarms = new Dictionary<string, IAlarm>();
        AlarmTimers = new Dictionary<string, Timer>();

        AlarmAdded = delegate { };
        AlarmChanged = delegate { };
        AlarmRemoved = delegate { };
        AlarmsUpdated = delegate { };
    }

    private ISettings AppSettings { get; }
    private IAlarmSource AlarmSource { get; }
    private Dictionary<string, IAlarm> Alarms { get; }
    private Dictionary<string, Timer> AlarmTimers { get; }
    private Timer? UpdateAlarmListTimer { get; set; }

    public event EventHandler<AlarmEventArgs> AlarmAdded;
    public event EventHandler<AlarmEventArgs> AlarmChanged;
    public event EventHandler<AlarmEventArgs> AlarmRemoved;
    public event EventHandler<AlarmEventArgs> AlarmsUpdated;

    public bool IsRunning => UpdateAlarmListTimer != null;

    public void Start()
    {
        UpdateAlarmListTimer = new Timer(FetchAlarms, null, 0, AppSettings.FetchRate * 60000);
    }

    public void Stop()
    {
        UpdateAlarmListTimer?.Dispose();
        UpdateAlarmListTimer = null;
    }

    public void Reset()
    {
        var alarmsToRemove = new List<IAlarm>(Alarms.Count);

        foreach (var alarm in Alarms.Values)
        {
            alarmsToRemove.Add(alarm);

            RemoveAlarmTimer(alarm);
        }

        foreach (var alarm in alarmsToRemove)
        {
            RemoveAlarm(alarm);
        }


    }

    public IEnumerable<IAlarm> GetActiveAlarms()
    {
        return Alarms.Values.Where(a => a.IsEnabled);
    }

    public bool AddAlarm(IAlarm alarm)
    {
        if (Alarms.ContainsKey(alarm.Id))
            return false;

        Alarms.Add(alarm.Id, alarm);

        var timerAdded = AddAlarmTimer(alarm);

        OnAlarmAdded(new AlarmEventArgs(alarm, AlarmEvent.Added));

        return timerAdded;
    }

    public void AlarmActionChange(IAlarm alarm, AlarmAction action)
    {
        if (!Alarms.ContainsKey(alarm.Id)) return;

        switch (action)
        {
            case AlarmAction.FiveMinBefore:
                break;
            case AlarmAction.ZeroMinBefore:
                break;
            case AlarmAction.SnoozeFiveMin:
                break;
            case AlarmAction.SnoozeTenMin:
                break;
            case AlarmAction.Dismiss:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }

    public IAlarm GetCurrentAppointment()
    {
        throw new NotImplementedException();
    }

    public IAlarm GetNextAppointment()
    {
        throw new NotImplementedException();
    }

    public bool RemoveAlarm(IAlarm alarm)
    {
        Alarms.Remove(alarm.Id);

        var removed = RemoveAlarmTimer(alarm);

        OnAlarmRemoved(new AlarmEventArgs(alarm, AlarmEvent.Removed));

        return removed;
    }

    public bool UpdateAlarm(IAlarm alarm)
    {
        if (!Alarms.ContainsKey(alarm.Id))
            return false;

        Alarms[alarm.Id] = alarm;

        var updatedTimer = UpdateAlarmTimer(alarm);

        OnAlarmChanged(new AlarmEventArgs(alarm, AlarmEvent.Updated));

        return updatedTimer;
    }

    protected virtual void OnAlarmChanged(AlarmEventArgs e)
    {
        AlarmChanged.Invoke(this, e);
    }

    protected virtual void OnAlarmAdded(AlarmEventArgs e)
    {
        AlarmAdded.Invoke(this, e);
    }

    protected virtual void OnAlarmRemoved(AlarmEventArgs e)
    {
        AlarmRemoved.Invoke(this, e);
    }

    protected virtual void OnAlarmsUpdated(AlarmEventArgs e)
    {
        AlarmsUpdated.Invoke(this, e);
    }

    private bool AddAlarmTimer(IAlarm alarm)
    {
        var timer = GenerateTimer(alarm);

        AlarmTimers.Add(alarm.Id, timer);

        return true;
    }

    private bool RemoveAlarmTimer(IAlarm alarm)
    {
        if (!AlarmTimers.ContainsKey(alarm.Id))
            return false;

        var timer = AlarmTimers[alarm.Id];
        timer.Dispose();
        AlarmTimers.Remove(alarm.Id);

        return true;
    }

    private bool UpdateAlarmTimer(IAlarm alarm)
    {
        if (!AlarmTimers.ContainsKey(alarm.Id)) return false;

        var timer = AlarmTimers[alarm.Id];
        timer.Dispose();
        timer = GenerateTimer(alarm);
        AlarmTimers[alarm.Id] = timer;

        return true;
    }

    private Timer GenerateTimer(IAlarm alarm)
    {
        var timeToLaunch = DateTime.Now - alarm.ReminderTime;

        if (timeToLaunch <= TimeSpan.Zero) timeToLaunch = TimeSpan.FromMicroseconds(1);

        return new Timer(AlarmCallback, alarm, timeToLaunch, Timeout.InfiniteTimeSpan);
    }

    private void AlarmCallback(object? state)
    {
        if (state is not string alarmId || !Alarms.ContainsKey(alarmId)) return;

        var alarm = Alarms[alarmId];

        // Launch alarm window or perform related actions
        var alarmWindow = ActivatorUtilities.CreateInstance<IAlarmForm>(Program.ServiceProvider, alarm);

        alarmWindow.Show();
    }

    private void FetchAlarms(object? signature)
    {
        var alarms = GetAlarms(AppSettings.FetchTime);

        foreach (var alarm in alarms)
        {
            if (Alarms.ContainsKey(alarm.Id))
                UpdateAlarm(alarm);
            else
                AddAlarm(alarm);
        }

        OnAlarmsUpdated(new AlarmEventArgs(AlarmEvent.Updated));
    }

    private IEnumerable<IAlarm> GetAlarms(int hours)
    {
        var alarms = AlarmSource.GetAlarms(hours).ToList();

        if (hours >= 24) return alarms;

        if (!alarms.Any())
            alarms = GetAlarms(hours + AppSettings.FetchTime).ToList();

        return alarms;
    }
}