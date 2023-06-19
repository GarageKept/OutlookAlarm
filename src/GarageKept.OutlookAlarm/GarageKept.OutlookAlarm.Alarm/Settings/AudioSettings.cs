using GarageKept.OutlookAlarm.Alarm.Audio;

namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class AudioSettings : SettingsBase
{
    private SoundType _defaultSound = SoundType.TickTock;
    private int _turnOffAlarmAfterStart = 15;
    public AudioSettings(Action save, bool isDeserializing) : base(save, isDeserializing) { }

    public SoundType DefaultSound
    {
        get => _defaultSound;
        set
        {
            if (_defaultSound == value) return;

            _defaultSound = value;
            if(!IsDeserializing) Save();;
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
                if(!IsDeserializing) Save();;
            }
        }
    }
}