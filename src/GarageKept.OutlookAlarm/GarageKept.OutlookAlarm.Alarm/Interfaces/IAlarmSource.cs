namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmSource : IDisposable
{
    IEnumerable<IAlarm> GetAlarms(int hours);
}