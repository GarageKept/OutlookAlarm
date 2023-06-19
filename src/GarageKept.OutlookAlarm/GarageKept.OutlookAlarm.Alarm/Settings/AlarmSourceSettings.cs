namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class AlarmSourceSettings : SettingsBase
{
    private int _fetchIntervalInMinutes = 2;
    private int _fetchTimeInHours = 1;

    // ReSharper disable once UnusedMember.Global
    public AlarmSourceSettings() { }
    public AlarmSourceSettings(Action save) : base(save) { }

    public int FetchIntervalInMinutes
    {
        get => _fetchIntervalInMinutes;
        set
        {
            if (_fetchIntervalInMinutes == value) return;

            _fetchIntervalInMinutes = value;
            Save?.Invoke();
            ;
        }
    }

    public int FetchTimeInHours
    {
        get => _fetchTimeInHours;
        set
        {
            if (_fetchTimeInHours == value) return;

            _fetchTimeInHours = value;
            Save?.Invoke();
            ;
        }
    }
}