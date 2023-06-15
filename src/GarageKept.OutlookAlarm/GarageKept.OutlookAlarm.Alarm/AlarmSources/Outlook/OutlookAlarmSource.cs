using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Microsoft.Office.Interop.Outlook;
using Application = Microsoft.Office.Interop.Outlook.Application;

namespace GarageKept.OutlookAlarm.Alarm.AlarmSources.Outlook;

/// <summary>
///     Provides functionality to interact with the Outlook Calendar.
/// </summary>
public class OutlookAlarmSource : IAlarmSource
{
    /// <summary>
    ///     Retrieves a list of Outlook appointment items within the specified number of hours from now.
    /// </summary>
    /// <param name="hours">The number of hours into the future to retrieve events for.</param>
    /// <returns>A list of Outlook.AppointmentItem objects representing the events within the specified time range.</returns>
    public IEnumerable<IAlarm> GetAlarms(int hours)
    {
        var outlookApp = new Application();
        var outlookNamespace = outlookApp.GetNamespace("MAPI");
        var calendarFolder = outlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);

        var startDateTime = DateTime.Now;
        var endDateTime = startDateTime.AddHours(hours);

        var calendarItems = calendarFolder.Items;
        calendarItems.IncludeRecurrences = true;
        calendarItems.Sort("[Start]", Type.Missing);
        calendarItems = calendarItems.Restrict($"[Start] <= '{endDateTime:g}' AND [End] >= '{startDateTime:g}'");

        var events = calendarItems.Cast<AppointmentItem>()
            .Where(e => !e.AllDayEvent).ToList();

        var appointments = events.Select(appointmentItem => new Appointment(appointmentItem));

        return appointments;
    }
}