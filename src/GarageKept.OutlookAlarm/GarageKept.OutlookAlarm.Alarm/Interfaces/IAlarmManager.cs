using GarageKept.OutlookAlarm.Alarm.AlarmManager;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmManager : IDisposable
{
    void ChangeAlarmState(IAlarm alarm, AlarmAction action);
    void ForceFetch();
    void Reset();
    void Start();
    void Stop();

    event Action<IEnumerable<IAlarm>> AlarmsUpdated;
}