namespace GarageKept.OutlookAlarm.Forms.Interfaces;

public interface IAlarmSource
{
    List<IAlarm> GetAlarms(int hours);
    void Start();
    void Stop();
}