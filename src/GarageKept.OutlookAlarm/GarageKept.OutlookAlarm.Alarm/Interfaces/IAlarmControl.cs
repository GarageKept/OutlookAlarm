namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmControl
{
    IAlarm? Alarm { get; set; }
    void UpdateDisplay();
}