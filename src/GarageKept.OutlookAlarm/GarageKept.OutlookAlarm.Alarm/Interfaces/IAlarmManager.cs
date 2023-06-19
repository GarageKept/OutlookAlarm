using GarageKept.OutlookAlarm.Alarm.AlarmManager;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmManager
{
    bool IsRunning { get; }
    event EventHandler<AlarmEventArgs> AlarmAdded;
    event EventHandler<AlarmEventArgs> AlarmChanged;
    event EventHandler<AlarmEventArgs> AlarmRemoved;
    event EventHandler<AlarmEventArgs> AlarmsUpdated;

    void ChangeAlarmState(IAlarm alarm, AlarmAction action);
    void DeactivateAlarm(IAlarm alarm);
    void ForceFetch();

    IEnumerable<IAlarm> GetActiveAlarms();

    IAlarm? GetCurrentAppointment();
    IAlarm? GetNextAppointment();
    void Reset();

    void Start();
    void Stop();
}