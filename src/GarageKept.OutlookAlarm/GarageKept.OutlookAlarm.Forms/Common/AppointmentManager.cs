using GarageKept.OutlookAlarm.Forms.Outlook;
using GarageKept.OutlookAlarm.Forms.UI.Forms;
using System.ComponentModel;
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

    public static CalendarEvents DismissedAppointments { get; set; } = new();

    public static CalendarEvents Appointments { get; set; } = new();

    public static event AppointmentsRefreshedHandler? Refresh;

    private static void RefreshTimerTick(object? sender, EventArgs? e)
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

        CleanupOldAppointments();

        var all = new CalendarEvents(OutlookCalendarInterop.GetAppointmentsInTheNextHours(Program.ApplicationSettings.FetchTime));

        var now = DateTime.Now;
        var twoHoursFromNow = now.AddHours(2);

        var filtered = all.Values.Where(ev =>
                (ev.Start <= now && ev.End >= now) || // Event is currently going on
                (ev.Start > now && ev.Start <= twoHoursFromNow) // Event will start in 2 hours
        ).Where(a => !DismissedAppointments.ContainsKey(a.Id)).ToList();

        Appointments.AddRange(filtered);

        AlarmManager.AddAlarm(Appointments);

        OnRefresh(e);
    }

    private static void CleanupOldAppointments()
    {
        var keysToRemove = (from appointment in Appointments
            where appointment.Value.End <= DateTime.Now
            select appointment.Key).ToList();

        foreach (var key in keysToRemove) Appointments.Remove(key);
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

    public static Appointment? GetNextAppointment()
    {
        var now = DateTime.Now;
        var nextAppointment = Appointments.Values.FirstOrDefault(item => item.Start > now);

        return nextAppointment;
    }

    public static Appointment? GetCurrentAppointment()
    {
        return Appointments.Values.FirstOrDefault(a => a.Start < DateTime.Now && a.End > DateTime.Now);
    }

    public static void Remove(Appointment? appointment)
    {
        var toRemove = Appointments.Values.Where(a => a.Id == appointment?.Id);
        
        foreach (var item in toRemove)
        {
            Appointments.Remove(item.Id);
            DismissedAppointments.Add(item);

            AlarmManager.RemoveAlarm(item.Id);
        }

        Program.MainWindow?.UpcomingAppointments.UpdateAppointmentControls(Appointments);
    }

    public static void ResetAll()
    {
        Appointments.AddRange(DismissedAppointments.Values);
        DismissedAppointments.Clear();

        AlarmManager.ResetAll();
    }
}