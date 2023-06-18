using GarageKept.OutlookAlarm.Alarm.UI.Forms;

namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface ISettingsForm
{
    DialogResult ShowDialog();
    Form? Owner { get; set; }
}