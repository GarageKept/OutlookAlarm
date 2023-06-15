using GarageKept.OutlookAlarm.Forms.Alarm;
using GarageKept.OutlookAlarm.Forms.Common;
using GarageKept.OutlookAlarm.Forms.Interfaces;
using GarageKept.OutlookAlarm.Forms.UI.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace GarageKept.OutlookAlarm.Forms;

/// <summary>
///     Defines the entry point of the application with associated settings.
/// </summary>
internal static class Program
{
    public static ServiceCollection AppServices { get; private set; }
    public static IServiceProvider? ServiceProvider { get; private set; }
    private static IAlarmManager? MyAlarmManager { get; set; }
    //private static IMainForm? MainWindow { get; set; }
    //private static ISettingsForm? SettingsWindow { get; set; }

    static Program()
    {
        // Create the DI container
        AppServices = new ServiceCollection();
        
        ApplicationConfiguration.Initialize();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        
        var host = CreateHostBuilder().Build();
        ServiceProvider = host.Services;

                MyAlarmManager = ServiceProvider.GetService<IAlarmManager>();
    }
    
    [STAThread]
    private static void Main()
    {   
        MyAlarmManager?.Start();

        // Start the application with the main form.

        Application.Run(ServiceProvider?.GetService<ISettingsForm>() as Form);
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services)=>{
                services.AddSingleton<ISettings, Settings>();
                services.AddSingleton<IAlarmManager, AlarmManager2>();
                //services.AddTransient<IMainForm, MainForm>();
                services.AddTransient<ISettingsForm, SettingsForm>();
            });
    }
}