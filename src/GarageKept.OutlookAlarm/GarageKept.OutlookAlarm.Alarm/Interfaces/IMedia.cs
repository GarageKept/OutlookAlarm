using GarageKept.OutlookAlarm.Alarm.Audio;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IMedia
{
    void PlaySound(string customSound);
    void PlaySound(SoundType soundType);
    void StopSound();
}