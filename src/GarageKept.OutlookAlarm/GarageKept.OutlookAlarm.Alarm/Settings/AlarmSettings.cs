namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class AlarmSettings : SettingsBase
{
    private string _alarmStartStringFormat = "hh:mm tt";
    private int _alarmWarningTime = 15;
    private int _left = -1;
    private string _timeLeftStringFormat = "{0:%h}h {0:mm}m {0:ss}s";
    private int _top;
    private int _maxAlarmsToShow = 5;


    // ReSharper disable once UnusedMember.Global
    public AlarmSettings() { }
    public AlarmSettings(Action save) : base(save) { }

    public int AlarmWarningTime
    {
        get => _alarmWarningTime;
        set
        {
            if (_alarmWarningTime == value) return;

            _alarmWarningTime = value;
            Save?.Invoke();
        }
    }

    public string TimeLeftStringFormat
    {
        get => _timeLeftStringFormat;
        set
        {
            if (_timeLeftStringFormat == value) return;

            _timeLeftStringFormat = value;
            Save?.Invoke();
        }
    }

    public string AlarmStartStringFormat
    {
        get => _alarmStartStringFormat;
        set
        {
            if (_alarmStartStringFormat == value) return;

            _alarmStartStringFormat = value;
            Save?.Invoke();
        }
    }

    public int Top
    {
        get => _top;
        set
        {
            if (_top == value) return;

            _top = value;
            Save?.Invoke();
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
            Save?.Invoke();
        }
    }

    public int MaxAlarmsToShow
    {
        get => _maxAlarmsToShow;
        set
        {
            if(_maxAlarmsToShow == value) return;

            _maxAlarmsToShow = value;
            Save?.Invoke();
        }
    }
}