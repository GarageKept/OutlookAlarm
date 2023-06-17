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
        AppServices = new ServiceCollection();
        var host = CreateHostBuilder().Build();
        ServiceProvider = host.Services;
        
        AlarmManager = ServiceProvider.GetRequiredService<IAlarmManager>();
        AppSettings = ServiceProvider.GetRequiredService<ISettings>();
    }

    internal static ServiceCollection AppServices { get; private set; }
    internal static IServiceProvider ServiceProvider { get; }
    internal static IAlarmManager AlarmManager { get; private set; }
    internal static ISettings AppSettings { get; private set; }

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

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<IAlarmContainerControl, AlarmContainerControl>();
                services.AddSingleton<IAlarmManager, OutlookAlarmManager>();
                services.AddSingleton<IAlarmSource, OutlookAlarmSource>();
                services.AddSingleton<IMainForm, MainForm>();
                services.AddSingleton<ISettings, OutlookAlarmSettings>();
                services.AddSingleton<ISettingsForm, SettingsForm>();
                services.AddSingleton<IMedia, Media>();

                services.AddTransient<IAlarmForm, AlarmForm>();
                services.AddTransient<IAlarmControl, AlarmControl>();
            });
    }
}