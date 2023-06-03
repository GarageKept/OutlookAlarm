using GarageKept.OutlookAlarm.Forms.Properties;
using NAudio.Wave;

namespace GarageKept.OutlookAlarm.Forms.Audio;

/// <summary>
///     Provides functionality to play media files and control system audio settings.
/// </summary>
public class Media
{
    private readonly Dictionary<SoundType, UnmanagedMemoryStream> _soundStreams = new()
    {
        { SoundType.Warning15, Resources.double_beep },
        { SoundType.Warning5, Resources.magic_notification },
        { SoundType.Warning0, Resources.tick_tock },
        { SoundType.Start, Resources.guitar_notification }
    };

    private readonly WaveOutEvent _player = new();

    /// <summary>
    ///     Plays the specified sound if it has not already been played for the given event.
    /// </summary>
    /// <param name="soundType">The type of sound to play.</param>
    public void PlaySound(SoundType soundType)
    {
        AudioEngine.UnMuteSystemVolume();

        var stream = _soundStreams[soundType]; // Get our stream
        var unmanagedStream = new UnmanagedMemoryStreamWaveStream(stream); // Make it playable by NAudio
        var loop = new LoopStream(unmanagedStream); // Make it looping

        _player.Init(loop);
        _player.Play();
    }

    public void Stop()
    {
        _player.Stop();
    }

    public void PlaySound(string customSound)
    {
        if (string.IsNullOrEmpty(customSound)) return;
        if (!File.Exists(customSound)) return;

        AudioEngine.UnMuteSystemVolume();

        using var wav = new LoopStream(new AudioFileReader(customSound));
        _player.Init(wav);
        _player.Play();
    }
}