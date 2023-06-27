using GarageKept.OutlookAlarm.Alarm.AlarmManager;
using static GarageKept.OutlookAlarm.Alarm.AlarmManager.OutlookAlarmManager;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmManager : IDisposable
{
    public delegate void UpdateAlarmList(IEnumerable<IAlarm> alarms);
    void ChangeAlarmState(IAlarm alarm, AlarmAction action);
    void ForceFetch();
    IEnumerable<IAlarm> GetActiveAlarms();
    void Reset();
    void Start();
    void Stop();

    UpdateAlarmList AlarmsUpdatedCallback { get; set; }
}

