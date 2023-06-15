namespace GarageKept.OutlookAlarm.Forms.Interfaces;

public interface IAlarmManager
{
    bool IsRunning { get; set; }
    void AddAlarm(IAlarm alarm);
    void RemoveAlarm(IAlarm alarm);
    void Start();
    void Stop();
}