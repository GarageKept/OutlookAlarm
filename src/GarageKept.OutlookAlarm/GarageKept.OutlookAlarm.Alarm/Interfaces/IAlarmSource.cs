namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmSource
{
    IEnumerable<IAlarm> GetAlarms(int hours);
}