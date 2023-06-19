using GarageKept.OutlookAlarm.Alarm.Audio;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IMediaPlayer
{
    bool IsPlaying { get; }
    void PlaySound(string customSound, bool loopPlay);
    void PlaySound(SoundType soundType, bool loopPlay);
    void StopSound();
}