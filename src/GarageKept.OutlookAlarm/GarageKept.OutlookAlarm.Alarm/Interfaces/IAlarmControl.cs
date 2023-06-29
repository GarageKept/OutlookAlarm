namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmControl : IDisposable
{
    IAlarm? Alarm { get; set; }

    void RefreshTimer_Tick(object? sender, EventArgs e);
}