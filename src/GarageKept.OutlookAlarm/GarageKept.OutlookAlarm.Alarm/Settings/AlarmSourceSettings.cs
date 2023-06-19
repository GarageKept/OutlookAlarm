namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class AlarmSourceSettings : SettingsBase
{
    private int _fetchIntervalInMinutes = 2;
    private int _fetchTimeInHours = 1;

    public AlarmSourceSettings(Action save, bool isDeserializing) : base(save, isDeserializing) { }

    public int FetchIntervalInMinutes
    {
        get => _fetchIntervalInMinutes;
        set
        {
            if (_fetchIntervalInMinutes == value) return;

            _fetchIntervalInMinutes = value;
            if(!IsDeserializing) Save();;
        }
    }

    public int FetchTimeInHours
    {
        get => _fetchTimeInHours;
        set
        {
            if (_fetchTimeInHours == value) return;

            _fetchTimeInHours = value;
            if(!IsDeserializing) Save();;
        }
    }
}