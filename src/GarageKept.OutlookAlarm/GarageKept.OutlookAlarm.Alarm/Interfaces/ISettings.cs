using GarageKept.OutlookAlarm.Alarm.Audio;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface ISettings
{
    Color GreenColor { get; set; }
    Color RedColor { get; set; }
    Color YellowColor { get; set; }
    int AlarmWarningTime { get; set; }
    int BarSize { get; }
    int FetchTime { get; set; }
    int Left { get; set; }
    int FetchRate { get; set; }
    int SliderSpeed { get; set; }
    string TimeFormat { get; set; }
    string TimeLeftStringFormat { get; set; }
    SoundType DefaultSound { get; set; }
    int TurnOffAlarmAfterStart { get; set; }

    /// <summary>
    ///     Saves the current instance of the <see cref="Settings" /> object to the settings file.
    /// </summary>
    void Save();
}