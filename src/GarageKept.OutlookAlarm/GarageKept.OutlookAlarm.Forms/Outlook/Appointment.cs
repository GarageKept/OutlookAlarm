using GarageKept.OutlookAlarm.Forms.Common;
using Microsoft.Office.Interop.Outlook;

namespace GarageKept.OutlookAlarm.Forms.Outlook;

public class Appointment
{
    public Appointment(_AppointmentItem item)
    {
        Subject = item.Subject;
        Start = item.Start;
        End = item.End;
        Duration = item.Duration;
        Id = item.EntryID;
        Response = item.ResponseStatus.ResponseTypeConverter() ;
    }

    public string Id { get; set; }
    public string Subject { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double Duration { get; set; }
    public ResponseType Response { get; set; }
}