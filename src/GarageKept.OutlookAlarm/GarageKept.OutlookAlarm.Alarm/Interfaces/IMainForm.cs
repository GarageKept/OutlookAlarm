namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IMainForm : IDisposable
{
    void UpdateAlarms(IEnumerable<IAlarm> alarms);
}