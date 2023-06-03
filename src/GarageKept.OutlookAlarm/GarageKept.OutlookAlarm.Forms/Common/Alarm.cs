using GarageKept.OutlookAlarm.Forms.Outlook;
using GarageKept.OutlookAlarm.Forms.UI.Forms;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Forms.Common;

public class Alarm
{
    public Alarm(Appointment item)
    {
        Appointment = item;
        AlarmTime = item.ReminderTime;
        State = AlarmState.Active;
        UpdateTimer();

        AlarmTimer.Tick += AlarmTimer_Tick;
        AlarmTimer.Start();
    }

    public DateTime AlarmTime { get; set; }

    public Appointment Appointment { get; set; }
    public AlarmState State { get; set; }
    public bool PlaySound { get; set; }
    public Timer AlarmTimer { get; set; } = new();
    public AlarmWindowForm? AlarmForm { get; set; }

    public void AlarmFormClosed(AlarmAction action)
    {
        switch (action)
        {
            case AlarmAction.Dismiss:
                Dismiss();
                break;
            case AlarmAction.FiveMinBefore:
                SnoozeBefore(5);
                break;
            case AlarmAction.SnoozeFiveMin:
                Snooze(5);
                break;
            case AlarmAction.SnoozeTenMin:
                Snooze(10);
                break;
            case AlarmAction.ZeroMinBefore:
                SnoozeBefore(0);
                break;
        }

        AlarmForm = null;
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

        AlarmTime = Appointment.Start.AddMinutes(-minutes);

        if (AlarmTime > Appointment.Start)
            AlarmTime = Appointment.Start;

        UpdateTimer();
    }

    public void UpdateTimer()
    {
        //AlarmTimer.Stop();
        AlarmTimer.Enabled = false;

        var ticksUntilAlarm = (int)((AlarmTime - DateTime.Now).TotalMilliseconds -
                                    Program.ApplicationSettings.AlarmWarningTime * 1000);

        if (ticksUntilAlarm <= 0)
            ticksUntilAlarm = 1;

        AlarmTimer.Interval = ticksUntilAlarm;

        AlarmTimer.Enabled = true;
        //AlarmTimer.Start();
    }

    private void AlarmTimer_Tick(object? sender, EventArgs? e)
    {
        if (AlarmTime >= DateTime.Now)
        {
            UpdateTimer();
            return;
        }

        AlarmTimer.Stop();

        if (State == AlarmState.Dismissed) return;

        if (AlarmForm != null) return;

        AlarmForm = new AlarmWindowForm(this, AlarmFormClosed);
        AlarmForm.Show();
    }
}