namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class SettingsBase
{
    public Action? Save;

    protected SettingsBase(Action? save = null)
    {
        if (save != null) Save = save;
    }
}