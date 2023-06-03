using GarageKept.OutlookAlarm.Forms.Common;
using Microsoft.Office.Interop.Outlook;

namespace GarageKept.OutlookAlarm.Forms.Outlook;

public class Appointment
{
    public Appointment(_AppointmentItem item)
    {
        CategoryColor = item.Categories.GetCategoryColor();
        Duration = item.Duration;
        End = item.End;
        Id = item.EntryID;
        IsOwnEvent = item.Organizer == item.RequiredAttendees;
        ReminderEnabled = item.ReminderSet;
        ReminderTime = Start.AddMinutes(-item.ReminderMinutesBeforeStart);
        Response = item.ResponseStatus.ResponseTypeConverter();
        Start = item.Start;
        Subject = item.Subject;
    }

    public Color CategoryColor { get; set; }
    public double Duration { get; set; }
    public DateTime End { get; set; }
    public string Id { get; set; }
    public bool IsOwnEvent { get; set; }
    public DateTime ReminderTime { get; set; }
    public bool ReminderEnabled { get; set; }
    public ResponseType Response { get; set; }
    public DateTime Start { get; set; }
    public string Subject { get; set; }
}