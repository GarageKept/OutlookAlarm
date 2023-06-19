namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class MainSettings : SettingsBase
{
    private int _barSize = 10;
    private int _left;
    private int _minimumWidth = 256;
    private int _sliderSpeed = 5;

    // ReSharper disable once UnusedMember.Global
    public MainSettings() { }

    public MainSettings(Action save) : base(save) { }

    public int BarSize
    {
        get => _barSize;
        set
        {
            if (_barSize == value) return;

            _barSize = value;
            Save?.Invoke();
        }
    }

    public int Left
    {
        get => _left;
        set
        {
            if (_left == value) return;

            _left = value;
            Save?.Invoke();
        }
    }

    public int MinimumWidth
    {
        get => _minimumWidth;
        set
        {
            if (_minimumWidth == value) return;

            _minimumWidth = value;
            Save?.Invoke();
        }
    }

    public int SliderSpeed
    {
        get => _sliderSpeed;
        set
        {
            if (_sliderSpeed == value) return;

            _sliderSpeed = value;
            Save?.Invoke();
        }
    }
}