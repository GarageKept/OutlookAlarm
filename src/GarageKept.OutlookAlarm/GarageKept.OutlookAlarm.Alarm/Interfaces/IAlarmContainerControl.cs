namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmContainerControl
{
    void DismissAlarm(IAlarm alarm);
    void RemoveAlarm(IAlarm alarm);
}