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
    static Program()
    {
        var host = CreateHostBuilder().Build();
        ServiceProvider = host.Services;

        AlarmManager = ServiceProvider.GetRequiredService<IAlarmManager>();
    }

    internal static IServiceProvider ServiceProvider { get; }
    internal static IAlarmManager AlarmManager { get; private set; }
    internal static OutlookAlarmSettings AppSettings { get; private set; } = OutlookAlarmSettings.Load();

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddSingleton<IAlarmContainerControl, AlarmContainerControl>();
            services.AddSingleton<IAlarmManager, OutlookAlarmManager>();
            services.AddSingleton<IAlarmSource, OutlookAlarmSource>();
            services.AddSingleton<IMainForm, MainForm>();
            services.AddSingleton<ISettingsForm, SettingsForm>();

            services.AddTransient<IMediaPlayer, MediaPlayer>();
            services.AddTransient<IAlarmForm, AlarmForm>();
            services.AddTransient<IAlarmControl, AlarmControl>();
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

        Application.Run(ServiceProvider.GetRequiredService<IMainForm>() as Form);
    }
}