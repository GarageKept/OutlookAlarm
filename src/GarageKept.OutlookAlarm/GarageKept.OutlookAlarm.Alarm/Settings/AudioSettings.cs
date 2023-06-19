using GarageKept.OutlookAlarm.Alarm.Audio;

namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class AudioSettings : SettingsBase
{
    private SoundType _defaultSound = SoundType.TickTock;
    private int _turnOffAlarmAfterStart = 15;

    // ReSharper disable once UnusedMember.Global
    public AudioSettings() { }

    public AudioSettings(Action save) : base(save) { }

    public SoundType DefaultSound
    {
        get => _defaultSound;
        set
        {
            if (_defaultSound == value) return;

            _defaultSound = value;
            Save?.Invoke();
            ;
        }
    }

    public int TurnOffAlarmAfterStart
    {
        get => _turnOffAlarmAfterStart;
        set
        {
            if (_turnOffAlarmAfterStart == value) return;
            {
                _turnOffAlarmAfterStart = value;
                Save?.Invoke();
                ;
            }
        }
    }
}