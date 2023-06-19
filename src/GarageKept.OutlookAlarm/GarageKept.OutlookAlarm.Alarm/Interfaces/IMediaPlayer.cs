using GarageKept.OutlookAlarm.Alarm.Audio;
using NAudio.Wave;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IMediaPlayer
{
    bool IsPlaying { get; }
    void PlaySound(string customSound, bool loopPlay, EventHandler<StoppedEventArgs>? whenStopped = null);
    void PlaySound(SoundType soundType, bool loopPlay, EventHandler<StoppedEventArgs>? whenStopped = null);
    void StopSound();
}