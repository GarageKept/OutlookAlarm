using GarageKept.OutlookAlarm.Alarm.AlarmManager;
using GarageKept.OutlookAlarm.Alarm.AlarmSources.Outlook;
using GarageKept.OutlookAlarm.Alarm.Audio;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using GarageKept.OutlookAlarm.Alarm.Settings;
using GarageKept.OutlookAlarm.Alarm.UI.Controls;
using GarageKept.OutlookAlarm.Alarm.UI.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GarageKept.OutlookAlarm.Alarm;

internal static class Program
{
#if !DEBUG
    private static readonly Mutex OutlookAlarmMutex = new(true, @"GarageKept.OutlookAlarm_ExclusiveMutex");
#endif

    static Program()
    {
        var host = CreateHostBuilder().Build();
        ServiceProvider = host.Services;

#if !DEBUG
        if (!OutlookAlarmMutex.WaitOne(TimeSpan.Zero, true))
            // Another instance is already running, exit the application
            MessageBox.Show(@"Another instance of the application is already running.");
#endif
    }

    internal static IServiceProvider? ServiceProvider { get; }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddSingleton<IAlarmContainerControl, AlarmContainerControl>();
            services.AddSingleton<IAlarmManager, OutlookAlarmManager>();
            services.AddSingleton<IAlarmSource, OutlookAlarmSource>();
            services.AddSingleton<IMainForm, MainForm>();
            services.AddSingleton<ISettings, OutlookAlarmSettings>();
            services.AddSingleton<ISettingsForm, SettingsForm>();

            services.AddTransient<IMediaPlayer, MediaPlayer>();
            services.AddTransient<IAlarmForm, AlarmForm>();
            services.AddTransient<IAlarmControl, AlarmControl>();
            services.AddTransient<IHolidayEditor, HolidayEditor>();
        });
    }

    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

#if !DEBUG
        if (!OutlookAlarmMutex.WaitOne(TimeSpan.Zero, true)) return;
#endif

        Application.Run(ServiceProvider?.GetRequiredService<IMainForm>() as Form);

#if !DEBUG
        OutlookAlarmMutex.ReleaseMutex();
#endif
    }
}