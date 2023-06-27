namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmControl : IDisposable
{
    IAlarm? Alarm { set; }
}