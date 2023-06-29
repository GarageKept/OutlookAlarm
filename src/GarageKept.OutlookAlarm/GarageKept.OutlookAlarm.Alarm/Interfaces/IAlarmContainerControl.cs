namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmContainerControl : IDisposable
{
    IEnumerable<IAlarm> Alarms { set; }
    void RefreshTimer_Tick(object? sender, EventArgs e);
}