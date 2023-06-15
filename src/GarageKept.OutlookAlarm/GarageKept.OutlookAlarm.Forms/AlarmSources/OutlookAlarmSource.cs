using GarageKept.OutlookAlarm.Forms.Interfaces;

namespace GarageKept.OutlookAlarm.Forms.AlarmSources;

internal class OutlookAlarmSource : IAlarmSource
{
    public List<IAlarm> GetAlarms(int hours)
    {
        return new List<IAlarm>(0);
    }

    public void Start()
    {
    }

    public void Stop()
    {
    }
}