using GarageKept.OutlookAlarm.Alarm.AlarmManager;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmManager : IDisposable
{
    public delegate void UpdateAlarmList(IEnumerable<IAlarm> alarms);

    UpdateAlarmList AlarmsUpdatedCallback { get; set; }

    void ChangeAlarmState(IAlarm alarm, AlarmAction action);
    void ForceFetch();
    void Reset();
    void Start();
    void Stop();
}