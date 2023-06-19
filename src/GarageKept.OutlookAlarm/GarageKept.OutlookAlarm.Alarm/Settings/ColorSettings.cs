namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class ColorSettings : SettingsBase
{
    private Color _alarmPastStartColor = Color.Red;
    private Color _greenColor = Color.Green;
    private Color _redColor = Color.Red;
    private Color _yellowColor = Color.Yellow;

    public ColorSettings(Action save, bool isDeserializing) : base(save, isDeserializing) { }

    public Color AlarmPastStartColor
    {
        get => _alarmPastStartColor;
        set
        {
            if (_alarmPastStartColor == value) return;
            _alarmPastStartColor = value;
            if(!IsDeserializing) Save();;
        }
    }

    public Color GreenColor
    {
        get => _greenColor;
        set
        {
            if (_greenColor == value) return;

            _greenColor = value;
            if(!IsDeserializing) Save();;
        }
    }

    public Color RedColor
    {
        get => _redColor;
        set
        {
            if (_redColor == value) return;

            _redColor = value;
            if(!IsDeserializing) Save();;
        }
    }

    public Color YellowColor
    {
        get => _yellowColor;
        set
        {
            if (_yellowColor == value) return;

            _yellowColor = value;
            if(!IsDeserializing) Save();;
        }
    }
}