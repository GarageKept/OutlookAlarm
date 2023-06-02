using GarageKept.OutlookAlarm.Forms.Outlook;
using GarageKept.OutlookAlarm.Forms.UI.Forms;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Forms.Common;

public class Alarm
{
    public Alarm(Appointment item)
    {
        Time = item.Start;
        AlarmTime = Time.AddMinutes(-Program.ApplicationSettings.AlarmWarningTime);
        Id = item.Id;
        Subject = item.Subject;
        State = AlarmState.Active;
        AlarmTimer.Tick += AlarmTimer_Tick;
        PlaySound = item.Response is ResponseType.Accepted or ResponseType.Organized;

        UpdateTimer();
    }

    public DateTime Time { get; set; }
    public DateTime AlarmTime { get; set; }
    public AlarmState State { get; set; }
    public string Subject { get; set; }
    public string Id { get; set; }
    public bool PlaySound { get; set; }
    public Timer AlarmTimer { get; set; } = new();

    public AlarmWindowForm? AlarmForm { get; set; }

    private void AlarmTimer_Tick(object? sender, EventArgs? e)
    {
        AlarmTimer.Stop();

        if (State == AlarmState.Dismissed) return;

        if (AlarmForm == null) return;

        AlarmForm = new AlarmWindowForm(this, AlarmFormClosed);
        AlarmForm.Show();
    }

    public void Dismiss()
    {
        State = AlarmState.Dismissed;
        AlarmTimer.Stop();
        AlarmTimer.Dispose();
    }

    public void Snooze(int minutes)
    {
        State = AlarmState.Snoozed;

        AlarmTime = DateTime.Now.AddMinutes(minutes);
        State = AlarmState.Snoozed;

        UpdateTimer();
    }

    public void SnoozeBefore(int minutes)
    {
        State = AlarmState.Snoozed;

        AlarmTime = Time.AddMinutes(-minutes);

        if (AlarmTime > Time)
            AlarmTime = Time;

        UpdateTimer();
    }

    public void UpdateTimer()
    {
        //AlarmTimer.Stop();
        AlarmTimer.Enabled = false;

        var ticksUntilAlarm = (int)(AlarmTime - DateTime.Now).TotalMilliseconds -
                              Program.ApplicationSettings.AlarmWarningTime * 1000;

        if (ticksUntilAlarm <= 0)
            ticksUntilAlarm = 1;

        AlarmTimer.Interval = ticksUntilAlarm;

        AlarmTimer.Enabled = true;
        //AlarmTimer.Start();
    }

    public void AlarmFormClosed(AlarmAction action)
    {
        switch (action)
        {
            case AlarmAction.FiveMinBefore:
                SnoozeBefore(5);
                break;
            case AlarmAction.ZeroMinBefore:
                SnoozeBefore(0);
                break;
            case AlarmAction.SnoozeFiveMin:
                Snooze(5);
                break;
            case AlarmAction.SnoozeTenMin:
                Snooze(10);
                break;
            case AlarmAction.Dismiss:
                Dismiss();
                break;
            default:
                Console.WriteLine(@"Unhandled AlarmAction found: " + action);
                break;
        }
        
        AlarmForm = null;
    }
}