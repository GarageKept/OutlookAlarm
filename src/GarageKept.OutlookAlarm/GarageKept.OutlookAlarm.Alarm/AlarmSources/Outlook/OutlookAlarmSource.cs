using System.Runtime.InteropServices;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using Microsoft.Office.Interop.Outlook;
using Application = Microsoft.Office.Interop.Outlook.Application;
using Exception = System.Exception;

namespace GarageKept.OutlookAlarm.Alarm.AlarmSources.Outlook;

/// <summary>
///     Provides functionality to interact with the Outlook Calendar.
/// </summary>
public class OutlookAlarmSource : IAlarmSource
{

    private Application OutlookApp { get; }
    private NameSpace OutlookNamespace { get; }
    private MAPIFolder CalendarFolder { get; }

    public OutlookAlarmSource()
    {
        OutlookApp = new Application();
        OutlookNamespace = OutlookApp.GetNamespace(@"MAPI");
        CalendarFolder = OutlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);
    }

    /// <summary>
    ///     Retrieves a list of Outlook appointment items within the specified number of hours from now.
    /// </summary>
    /// <param name="hours">The number of hours into the future to retrieve events for.</param>
    /// <returns>A list of Outlook.AppointmentItem objects representing the events within the specified time range.</returns>
    public IEnumerable<IAlarm> GetAlarms(int hours)
    {
        try
        {
            var startDateTime = DateTime.Now;
            var endDateTime = startDateTime.AddHours(hours);

            var calendarItems = CalendarFolder.Items;
            calendarItems.IncludeRecurrences = true;
            calendarItems.Sort("[Start]", Type.Missing);
            calendarItems = calendarItems.Restrict($"[Start] <= '{endDateTime:g}' AND [End] >= '{startDateTime:g}' AND [End] > '{DateTime.Now:g}'");

            var events = calendarItems.Cast<AppointmentItem>().ToList();
            var removedAllDay = events.Where(e => !e.AllDayEvent).ToList();

            var appointments = removedAllDay.Select(appointmentItem => new Appointment(appointmentItem));

            // Release and dispose COM objects
            Marshal.ReleaseComObject(calendarItems);

            return appointments.ToList();
        }
        catch (Exception ex)
        {
            return new List<IAlarm>(1) { new Appointment { Name = ex.Message, Start = DateTime.Now, End = DateTime.MaxValue } };
        }
    }

    void IDisposable.Dispose()
    {
        Marshal.ReleaseComObject(OutlookApp);
        Marshal.ReleaseComObject(OutlookNamespace);
        Marshal.ReleaseComObject(CalendarFolder);
    }
}