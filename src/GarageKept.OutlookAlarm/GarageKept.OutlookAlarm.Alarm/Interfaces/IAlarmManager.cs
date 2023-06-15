using GarageKept.OutlookAlarm.Alarm.Alarm;
using GarageKept.OutlookAlarm.Alarm.Common;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmManager
{
    bool IsRunning { get; }
    event EventHandler<AlarmEventArgs> AlarmAdded;
    event EventHandler<AlarmEventArgs> AlarmChanged;
    event EventHandler<AlarmEventArgs> AlarmRemoved;
    event EventHandler<AlarmEventArgs> AlarmsUpdated;

    IEnumerable<IAlarm> GetActiveAlarms();
    bool AddAlarm(IAlarm alarm);
    void AlarmActionChange(IAlarm alarm, AlarmAction action);
    IAlarm GetCurrentAppointment();
    IAlarm GetNextAppointment();
    bool RemoveAlarm(IAlarm alarm);
    bool UpdateAlarm(IAlarm alarm);

    void Start();
    void Stop();
    void Reset();
}