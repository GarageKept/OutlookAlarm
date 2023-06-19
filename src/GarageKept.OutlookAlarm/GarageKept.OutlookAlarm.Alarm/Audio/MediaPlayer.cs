﻿using GarageKept.OutlookAlarm.Alarm.Interfaces;
using GarageKept.OutlookAlarm.Alarm.Properties;
using NAudio.Wave;

namespace GarageKept.OutlookAlarm.Alarm.Audio;

/// <summary>
///     Provides functionality to play mediaPlayer files and control system audio settings.
/// </summary>
public class MediaPlayer : IMediaPlayer
{
    private readonly WaveOutEvent _player = new();

    private readonly Dictionary<SoundType, UnmanagedMemoryStream> _soundStreams = new()
    {
        { SoundType.BeepBeep, Resources.double_beep },
        { SoundType.TickTock, Resources.tick_tock },
        { SoundType.Guitar, Resources.guitar_notification },
        { SoundType.Urgent, Resources.urgent_simple_tone }
    };

    /// <summary>
    ///     Plays the specified sound if it has not already been played for the given event.
    /// </summary>
    /// <param name="soundType">The type of sound to play.</param>
    public void PlaySound(SoundType soundType, bool loopPlay)
    {
        if (_player.PlaybackState == PlaybackState.Playing)
            _player.Stop();

        AudioEngine.UnMuteSystemVolume();

        var stream = _soundStreams[soundType]; // Get our stream
        var unmanagedStream = new UnmanagedMemoryStreamWaveStream(stream); // Make it playable by NAudio

        if (loopPlay)
        {
            var loop = new LoopStream(unmanagedStream); // Make it looping
            _player.Init(loop);
        }
        else
        {
            _player.Init(unmanagedStream);
        }

        _player.Play();
    }

    public void StopSound()
    {
        _player.Stop();
    }

    public bool IsPlaying =>_player.PlaybackState == PlaybackState.Playing;

    public void PlaySound(string customSound, bool loopPlay)
    {
        if (string.IsNullOrEmpty(customSound)) return;
        if (!File.Exists(customSound)) return;

        AudioEngine.UnMuteSystemVolume();

        if (loopPlay)
        {
            using var wav = new LoopStream(new AudioFileReader(customSound));

            _player.Init(wav);
        }
        else
        {
            using var wav = new AudioFileReader(customSound);
            _player.Init(wav);
        }

        _player.Play();
    }
}