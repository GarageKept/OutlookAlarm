using GarageKept.OutlookAlarm.Alarm.Settings;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

internal interface IHolidayEditor : IDisposable
{
    Holiday Holiday { get; set; }
    DialogResult ShowDialog();
}