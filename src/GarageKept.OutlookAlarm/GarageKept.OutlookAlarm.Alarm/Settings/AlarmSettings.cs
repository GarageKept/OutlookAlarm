namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class AlarmSettings : SettingsBase
{
    private string _alarmStartStringFormat = "hh:mm tt";
    private int _alarmWarningTime = 15;
    private int _left = -1;
    private string _timeLeftStringFormat = "{0:%h}h {0:mm}m {0:ss}s";
    private int _top;

    public AlarmSettings(Action save, bool isDeserializing) : base(save, isDeserializing) { }

    public int AlarmWarningTime
    {
        get => _alarmWarningTime;
        set
        {
            if (_alarmWarningTime == value) return;

            _alarmWarningTime = value;
            if(!IsDeserializing) Save();;
        }
    }

    public string TimeLeftStringFormat
    {
        get => _timeLeftStringFormat;
        set
        {
            if (_timeLeftStringFormat == value) return;

            _timeLeftStringFormat = value;
            if(!IsDeserializing) Save();;
        }
    }

    public string AlarmStartStringFormat
    {
        get => _alarmStartStringFormat;
        set
        {
            if (_alarmStartStringFormat == value) return;

            _alarmStartStringFormat = value;
            if(!IsDeserializing) Save();;
        }
    }

    public int Top
    {
        get => _top;
        set
        {
            if (_top == value) return;

            _top = value;
            if(!IsDeserializing) Save();;
        }
    }

    public int Left
    {
        get
        {
            if (_left < 0)
                return Screen.PrimaryScreen?.Bounds.Width / 2 ?? 0;

            return _left;
        }
        set
        {
            if (_left == value) return;

            _left = value;
            if(!IsDeserializing) Save();;
        }
    }
}