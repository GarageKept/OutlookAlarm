using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Microsoft.Office.Interop.Outlook;

namespace GarageKept.OutlookAlarm.Alarm.AlarmSources.Outlook;

public class Appointment : IAlarm
{
    public Appointment(_AppointmentItem item)
    {
        AlarmColor = item.Categories.GetCategoryColor();
        CustomSound = item.ReminderSoundFile;
        Duration = item.Duration;
        End = item.End;
        Id = item.EntryID;
        IsOwnEvent = item.Organizer == item.RequiredAttendees;
        IsReminderEnabled = true;
        ReminderTime = item.Start.AddMinutes(-item.ReminderMinutesBeforeStart);
        Response = item.ResponseStatus.ResponseTypeConverter();
        Start = item.Start;
        Name = item.Subject;
        IsAudible = true;
        IsActive = true;
        HasCustomSound = !string.IsNullOrEmpty(CustomSound);
    }

    public double Duration { get; set; }
    public bool IsOwnEvent { get; set; }
    public ResponseType Response { get; set; }

    public bool IsAudible { get; set; }
    public Color AlarmColor { get; set; }
    public DateTime End { get; set; }
    public string Id { get; set; }
    public DateTime ReminderTime { get; set; }
    public bool IsReminderEnabled { get; set; }
    public bool IsActive { get; set; }
    public DateTime Start { get; set; }
    public string Name { get; set; }
    public string CustomSound { get; set; }
    public bool HasCustomSound { get; set; }
}