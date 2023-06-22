using GarageKept.OutlookAlarm.Alarm.Settings;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface ISettings
{
    AlarmSettings Alarm { get; set; }
    AudioSettings Audio { get; set; }
    AlarmSourceSettings AlarmSource { get; set; }
    ColorSettings Color { get; set; }
    MainSettings Main { get; set; }
    TimeManagementSettings TimeManagement { get; set; }
}