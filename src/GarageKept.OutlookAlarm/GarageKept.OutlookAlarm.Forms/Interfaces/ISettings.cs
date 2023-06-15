using GarageKept.OutlookAlarm.Forms.Audio;
using GarageKept.OutlookAlarm.Forms.Common;

namespace GarageKept.OutlookAlarm.Forms.Interfaces;

public interface ISettings
{
    Color GreenColor { get; set; }
    Color RedColor { get; set; }
    Color YellowColor { get; set; }
    int AlarmWarningTime { get; set; }
    int BarSize { get; }
    int FetchTime { get; set; }
    int Left { get; set; }
    int RefreshRate { get; set; }
    int SliderSpeed { get; set; }
    string TimeFormat { get; set; }
    string TimeLeftStringFormat { get; set; }
    SoundType DefaultSound { get; set; }

    /// <summary>
    ///     Saves the current instance of the <see cref="Settings" /> object to the settings file.
    /// </summary>
    void Save();
}