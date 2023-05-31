using Microsoft.Office.Interop.Outlook;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Forms.Common;

public delegate void AppointmentsRefreshedHandler(object? sender, EventArgs? e);

public static class AppointmentManager
{
    private static readonly Timer RefreshTimer = new() { Interval = Program.ApplicationSettings.RefreshRate * 1000 };

    static AppointmentManager()
    {
        RefreshTimer.Tick += RefreshTimerTick;
    }

    public static CalendarEvents Appointments { get; set; } = new();

    public static event AppointmentsRefreshedHandler? Refresh;

    private static void RefreshTimerTick(object? sender, EventArgs? e)
    {
        var all = new CalendarEvents(
            OutlookCalendarInterop.GetEventsForNextXHours(Program.ApplicationSettings.FetchTime));

        var now = DateTime.Now;
        var twoHoursFromNow = now.AddHours(2);

        var filtered = all.Where(ev =>
                (ev.Start <= now && ev.End >= now) || // Event is currently going on
                (ev.Start > now && ev.Start <= twoHoursFromNow) // Event will start in 2 hours
        ).ToList();

        if (filtered.Count == 0)
        {
            var nextAppointment = OutlookCalendarInterop.GetEventsForNextXHours(24).MinBy(ev => ev.Start)
                                  ??
                                  OutlookCalendarInterop.GetEventsForNextXHours(3600).MinBy(ev => ev.Start);

            if (nextAppointment != null) filtered.Add(nextAppointment);
        }


        Appointments = new CalendarEvents(filtered);

        AlarmManager.AddAlarm(Appointments);

        OnRefresh(e);
    }

    private static void OnRefresh(EventArgs? e)
    {
        var handler = Refresh;

        handler?.Invoke(null, e);
    }

    public static void Start()
    {
        RefreshTimerTick(null, null);
        RefreshTimer.Start();
    }

    public static void ForceRefresh()
    {
        RefreshTimerTick(null, null);
    }

    public static AppointmentItem? GetNextAppointment()
    {
        var now = DateTime.Now;
        var nextAppointment = Appointments.FirstOrDefault(item => item.Start > now);

        return nextAppointment;
    }
}