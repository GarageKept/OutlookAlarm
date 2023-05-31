using System.Media;

namespace GarageKept.OutlookAlarm.Forms.Common;

/// <summary>
///     Provides functionality to play media files and control system audio settings.
/// </summary>
public class Media
{
    private const string MediaFolder = "Media\\";

    private readonly Dictionary<SoundType, string> _played = new();

    private readonly Dictionary<SoundType, string> _soundFiles = new()
    {
        { SoundType.Warning15, "double-beep.wav" },
        { SoundType.Warning5, "magic-notification.wav" },
        { SoundType.Warning0, "urgent-simple-tone.wav" },
        { SoundType.Start, "guitar-notification.wav" }
    };

    public SoundPlayer? Player { get; set; }

    /// <summary>
    ///     Plays the specified sound if it has not already been played for the given event.
    /// </summary>
    /// <param name="soundType">The type of sound to play.</param>
    /// <param name="eventId">The ID of the event for which the sound is being played.</param>
    public void PlaySound(SoundType soundType, string eventId)
    {
        if (_soundFiles.TryGetValue(soundType, out var fileName))
        {
            if (_played.ContainsKey(soundType) && _played[soundType] == eventId) return;

            _played[soundType] = eventId;

            AudioEngine.UnMuteSystemVolume();

            Play(MediaFolder + fileName);
        }
    }

    /// <summary>
    ///     Plays the specified sound without checking for a specific event.
    /// </summary>
    /// <param name="soundType">The type of sound to play.</param>
    public void PlaySound(SoundType soundType)
    {
        PlaySound(soundType, Guid.NewGuid().ToString());
    }

    /// <summary>
    ///     Plays the specified sound file.
    /// </summary>
    /// <param name="filePath">The file path of the sound file to play.</param>
    private void Play(string filePath)
    {
        try
        {
            Player = new SoundPlayer
            {
                SoundLocation = filePath
            };

            Player.SoundLocationChanged += PlayerOnSoundLocationChanged;

            Player.Play();
        }
        catch (Exception)
        {
            // Just eat the exception
        }
    }

    private void PlayerOnSoundLocationChanged(object? sender, EventArgs e)
    {
        if (sender == null) return;

        var player = (SoundPlayer)sender;
        player.Play();
    }

    public void Stop()
    {
        Player?.Stop();
    }
}