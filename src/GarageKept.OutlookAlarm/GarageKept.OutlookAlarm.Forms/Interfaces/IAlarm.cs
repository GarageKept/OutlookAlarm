namespace GarageKept.OutlookAlarm.Forms.Interfaces;

public interface IAlarm
{
    string Name { get; set; }
    string Id { get; set; }
    DateTime Start { get; set; }
    DateTime End { get; set; }
    DateTime ReminderTime { get; set; }
    bool IsReminderEnabled { get; set; }
    bool IsEnabled { get; set; }
    bool IsAudible { get; set; }
    Color AlarmColor { get; set; }
    string CustomSound { get; set; }
    bool HasCustomSound => string.IsNullOrEmpty(CustomSound);
}