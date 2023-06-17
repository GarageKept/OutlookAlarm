using GarageKept.OutlookAlarm.Alarm.Settings;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface ISettings
{
    AlarmSettings Alarm { get; set; }
    AudioSettings Audio { get; set; }
    AlarmSourceSettings AlarmSource { get; set; }
    ColorSettings Color { get; set; }
    MainSettings Main { get; set; }

    /// <summary>
    ///     Saves the current instance of the <see cref="Settings" /> object to the settings file.
    /// </summary>
    void Save();
}