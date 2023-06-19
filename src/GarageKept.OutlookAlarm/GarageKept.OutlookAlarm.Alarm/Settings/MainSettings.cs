namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class MainSettings : SettingsBase
{
    private int _barSize = 10;
    private int _left;
    private int _minimumWidth = 256;
    private int _sliderSpeed = 5;

    public MainSettings(Action save, bool isDeserializing) : base(save, isDeserializing) { }

    public int BarSize
    {
        get => _barSize;
        set
        {
            if(_barSize == value) return;
            
            _barSize = value;
            if(!IsDeserializing) Save();;
        }
    }

    public int Left
    {
        get => _left;
        set
        {
            if( _left == value) return;

            _left = value;
            if(!IsDeserializing) Save();;
        }
    }

    public int MinimumWidth
    {
        get => _minimumWidth;
        set
        {
            if(_minimumWidth == value) return;

            _minimumWidth = value;
            if(!IsDeserializing) Save();;
        }
    }

    public int SliderSpeed
    {
        get => _sliderSpeed;
        set
        { 
            if(_sliderSpeed == value) return;

            _sliderSpeed = value;
            if(!IsDeserializing) Save();;
        }
    }
}