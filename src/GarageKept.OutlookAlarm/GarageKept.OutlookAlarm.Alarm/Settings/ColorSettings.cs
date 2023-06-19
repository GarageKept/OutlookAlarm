namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class ColorSettings : SettingsBase
{
    private Color _alarmPastStartColor = Color.Red;
    private Color _greenColor = Color.Green;
    private Color _redColor = Color.Red;
    private Color _yellowColor = Color.Yellow;

    // ReSharper disable once UnusedMember.Global
    public ColorSettings() { }

    public ColorSettings(Action save) : base(save) { }

    public Color AlarmPastStartColor
    {
        get => _alarmPastStartColor;
        set
        {
            if (_alarmPastStartColor == value) return;
            _alarmPastStartColor = value;
            Save?.Invoke();
            ;
        }
    }

    public Color GreenColor
    {
        get => _greenColor;
        set
        {
            if (_greenColor == value) return;

            _greenColor = value;
            Save?.Invoke();
            ;
        }
    }

    public Color RedColor
    {
        get => _redColor;
        set
        {
            if (_redColor == value) return;

            _redColor = value;
            Save?.Invoke();
            ;
        }
    }

    public Color YellowColor
    {
        get => _yellowColor;
        set
        {
            if (_yellowColor == value) return;

            _yellowColor = value;
            Save?.Invoke();
            ;
        }
    }
}