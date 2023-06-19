using GarageKept.OutlookAlarm.Alarm.AlarmManager;

namespace GarageKept.OutlookAlarm.Alarm.Audio;

/// <summary>
///     Represents the different types of sounds used in the application.
/// </summary>
public enum SoundType
{
    [Display("Beep Beep")] BeepBeep,
    [Display("Tick Tock")] TickTock,
    [Display("Guitar")] Guitar,
    [Display("Urgent")] Urgent
}