using GarageKept.OutlookAlarm.Alarm.AlarmManager;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmManager
{
    event EventHandler<AlarmEventArgs> AlarmAdded;
    event EventHandler<AlarmEventArgs> AlarmChanged;
    event EventHandler<AlarmEventArgs> AlarmRemoved;
    event EventHandler<AlarmEventArgs> AlarmsUpdated;

    IEnumerable<IAlarm> GetActiveAlarms();
    
    void ChangeAlarmState(IAlarm alarm, AlarmAction action);
    void DeactivateAlarm(IAlarm alarm);
    void ForceFetch();

    IAlarm? GetCurrentAppointment();
    IAlarm? GetNextAppointment();

    void Start();
    void Stop();
    void Reset();

    bool IsRunning { get; }
}