using GarageKept.OutlookAlarm.Alarm.AlarmManager;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmManager : IDisposable
{
    // Properties
    bool IsRunning { get; }

    // Events
    event EventHandler<AlarmEventArgs> AlarmAdded;
    event EventHandler<AlarmEventArgs> AlarmChanged;
    event EventHandler<AlarmEventArgs> AlarmRemoved;
    event EventHandler<AlarmEventArgs> AlarmsUpdated;

    // Methods
    void ChangeAlarmState(IAlarm alarm, AlarmAction action);
    void ForceFetch();
    IEnumerable<IAlarm> GetActiveAlarms();
    IAlarm? GetCurrentAppointment();
    IAlarm? GetNextAppointment();
    IAlarm? GetCurrentAppointment();
    IAlarm? GetNextAppointment();
    void Reset();
    void Start();
    void Stop();
}
