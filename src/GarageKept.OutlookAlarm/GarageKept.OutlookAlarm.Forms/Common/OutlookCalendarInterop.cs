using Microsoft.Office.Interop.Outlook;
using Application = Microsoft.Office.Interop.Outlook.Application;

namespace GarageKept.OutlookAlarm.Forms.Common;

/// <summary>
///     Provides functionality to interact with the Outlook Calendar.
/// </summary>
public static class OutlookCalendarInterop
{
    /// <summary>
    ///     Retrieves a list of Outlook appointment items within the specified number of hours from now.
    /// </summary>
    /// <param name="hours">The number of hours into the future to retrieve events for.</param>
    /// <returns>A list of Outlook.AppointmentItem objects representing the events within the specified time range.</returns>
    public static List<AppointmentItem> GetEventsForNextXHours(int hours)
    {
        var outlookApp = new Application();
        var outlookNamespace = outlookApp.GetNamespace("MAPI");
        var calendarFolder = outlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);

        var startDateTime = DateTime.Now;
        var endDateTime = startDateTime.AddHours(hours);

        var calendarItems = calendarFolder.Items;
        calendarItems.IncludeRecurrences = true;
        calendarItems.Sort("[Start]", Type.Missing);
        calendarItems =
            calendarItems.Restrict(
                $"[Start] <= '{endDateTime:g}' AND [End] >= '{startDateTime:g}'");

        var events = calendarItems.Cast<AppointmentItem>().ToList();

        return events.Where(e => !e.AllDayEvent).ToList();
    }

    /// <summary>
    ///     Retrieves a list of Outlook appointment items within the specified number of hours from now asynchronously.
    /// </summary>
    /// <param name="hours">The number of hours into the future to retrieve events for.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of Outlook.AppointmentItem
    ///     objects representing the events within the specified time range.
    /// </returns>
    public static async Task<List<AppointmentItem>?> GetEventsForNextXHoursAsync(int hours)
    {
        return await Task.Run(() => GetEventsForNextXHours(hours));
    }
}