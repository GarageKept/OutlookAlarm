using Microsoft.Office.Interop.Outlook;

namespace GarageKept.OutlookAlarm.Forms.Common;

/// <summary>
///     Represents a collection of calendar events.
/// </summary>
public class CalendarEvents : List<AppointmentItem>
{
    /// <summary>
    ///     Initializes a new instance of the CalendarEvents class and loads events for the next 24 hours.
    /// </summary>
    public CalendarEvents()
    {
    }

    public CalendarEvents(IEnumerable<AppointmentItem>? items)
    {
        if (items != null) AddRange(items);
    }

    /// <summary>
    ///     Adds an AppointmentItem to the list if it doesn't already exist.
    /// </summary>
    /// <param name="item">The AppointmentItem to add.</param>
    public new void Add(AppointmentItem item)
    {
        if (this.All(existingItem => existingItem.GlobalAppointmentID != item.GlobalAppointmentID)) base.Add(item);
    }

    /// <summary>
    ///     Adds a range of AppointmentItems to the list without adding duplicates.
    /// </summary>
    /// <param name="items">The AppointmentItems to add.</param>
    public new void AddRange(IEnumerable<AppointmentItem> items)
    {
        foreach (var item in items) Add(item);
    }

    /// <summary>
    ///     Returns the next AppointmentItem from the collection based on the start time.
    /// </summary>
    /// <returns>The next AppointmentItem in the collection.</returns>
    public AppointmentItem? GetNextAppointment()
    {
        return this.MinBy(a => a.StartUTC);
    }
}