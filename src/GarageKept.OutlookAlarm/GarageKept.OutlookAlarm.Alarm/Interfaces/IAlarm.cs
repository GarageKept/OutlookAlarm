namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarm
{
    string Name { get; set; }
    string Id { get; set; }
    DateTime Start { get; set; }
    DateTime End { get; set; }
    DateTime ReminderTime { get; set; }
    bool IsReminderEnabled { get; set; }
    bool IsActive { get; set; }
    bool IsAudible { get; set; }
    Color AlarmColor { get; set; }
    string CustomSound { get; set; }
    bool HasCustomSound { get; set; }
    string Organizer { get; set; }
    string Categories { get; set; }
    string Location { get; set; }
    string TeamsMeetingUrl { get; set; }
}