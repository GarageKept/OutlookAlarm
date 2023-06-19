namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface ISettingsForm
{
    Form? Owner { get; set; }
    DialogResult ShowDialog();
}