using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Microsoft.Office.Interop.Outlook;

namespace GarageKept.OutlookAlarm.Alarm.AlarmSources.Outlook;

public class Appointment : IAlarm
{
    public Appointment()
    {
        CustomSound = string.Empty;
        Id = string.Empty;
        Location = string.Empty;
        Name = string.Empty;
        Organizer = string.Empty;
        TeamsMeetingUrl = string.Empty;
    }

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
        Organizer = item.Organizer;
        Categories.AddRange(item.Categories.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
        Location = item.Location;
        TeamsMeetingUrl = ExtractTeamsMeetingUrlFromBody(item.Body);
    }

    public double Duration { get; set; }
    public bool IsOwnEvent { get; set; }
    public ResponseType Response { get; set; }
    public Color AlarmColor { get; set; }
    public List<string> Categories { get; set; } = new();
    public string CustomSound { get; set; }
    public DateTime End { get; set; }
    public bool HasCustomSound { get; set; }
    public string Id { get; set; }
    public bool IsActive { get; set; }

    public bool IsAudible { get; set; }
    public bool IsReminderEnabled { get; set; }
    public string Location { get; set; }
    public string Name { get; set; }
    public string Organizer { get; set; }
    public DateTime ReminderTime { get; set; }
    public DateTime Start { get; set; }
    public string TeamsMeetingUrl { get; set; }

    private static string ExtractTeamsMeetingUrlFromBody(string body)
    {
        // Implement your logic to extract the Teams meeting URL from the body
        // This can be done using regular expressions, string manipulation, or any other suitable method
        // Here's a simple example assuming the Teams meeting URL is enclosed within <TeamsMeetingURL> tags

        const string startTag = "https://teams.microsoft.com";
        const string endTag = ">";

        var startIndex = body.IndexOf(startTag, StringComparison.Ordinal) - startTag.Length;

        if (startIndex < 0) return string.Empty;

        var endIndex = body.IndexOf(endTag, startIndex, StringComparison.Ordinal);

        // Return null or an empty string if the Teams meeting URL is not found
        if (endIndex < 0) return string.Empty;

        startIndex += startTag.Length;
        return body[startIndex..endIndex];
    }
}