namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

internal interface IAlarmForm : IDisposable
{
    void Show(IAlarm alarm);
}