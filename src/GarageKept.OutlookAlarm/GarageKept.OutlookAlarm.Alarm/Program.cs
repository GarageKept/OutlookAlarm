using GarageKept.OutlookAlarm.Alarm.AlarmSources.Outlook;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using GarageKept.OutlookAlarm.Alarm.UI.Forms;
using GarageKept.OutlookAlarm.Alarm.Alarm;
using GarageKept.OutlookAlarm.Alarm.Common;
using GarageKept.OutlookAlarm.Alarm.Interfaces;
using GarageKept.OutlookAlarm.Alarms.UI.Controls;
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
    }

    internal static ServiceCollection AppServices { get; private set; }
    internal static IServiceProvider ServiceProvider { get; }
    internal static IAlarmManager? MyAlarmManager { get; private set; }

    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        MyAlarmManager = ServiceProvider.GetService<IAlarmManager>();

        if (MyAlarmManager == null)
        {
            const int exitCode = 666;
            Environment.Exit(exitCode);
            return;
        }

        MyAlarmManager.Start();

        Application.Run(ServiceProvider.GetRequiredService<IMainForm>() as Form);
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<IAlarmSource, OutlookAlarmSource>();
                services.AddSingleton<IAlarmManager, AlarmManager>();
                services.AddSingleton<ISettings, Settings>();
                services.AddSingleton<IMainForm, MainForm>();
                services.AddTransient<IAlarmForm, AlarmForm>();
                services.AddSingleton<IAlarmContainerControl, IAlarmContainerControl>();
                services.AddTransient<IAlarmControl, AlarmControl>();
            });
    }
}