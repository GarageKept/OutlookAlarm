namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmControl
{
    IAlarm? Alarm { get; set; }
    ISettings? AppSettings { get; set; }

    void UpdateDisplay();
}