using GarageKept.OutlookAlarm.Forms.UI.Forms;
using Microsoft.Office.Interop.Outlook;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Forms.Common;

// Define a delegate type for the callback method
// public delegate void AlarmFormCallback(AlarmAction action);

public class Alarm
{
    public Alarm(_AppointmentItem item)
    {
        Time = item.Start;
        Id = item.EntryID;
        Subject = item.Subject;
        State = AlarmState.Active;

        var ticksUntilAlarm = (int)(Time - DateTime.Now).TotalMilliseconds -
                              Program.ApplicationSettings.AlarmWarningTime * 1000;

        if (ticksUntilAlarm <= Program.ApplicationSettings.AlarmWarningTime * 60 * 1000)
        {
            AlarmTimer_Tick(null, null);
        }
        else
        {
            AlarmTimer.Interval = ticksUntilAlarm;
            AlarmTimer.Tick += AlarmTimer_Tick;
        }
    }

    public DateTime Time { get; set; }
    public AlarmState State { get; set; }
    public string Subject { get; set; }
    public string Id { get; set; }
    public Timer AlarmTimer { get; set; } = new();

    private void AlarmTimer_Tick(object? sender, EventArgs? e)
    {
        var alarmForm = new AlarmWindowForm(this, AlarmFormClosed);

        alarmForm.Show();
    }

    public void Snooze(TimeSpan duration)
    {
        if (State == AlarmState.Active)
        {
            Time = DateTime.Now.Add(duration);
            State = AlarmState.Snoozed;
        }
    }

    public void Dismiss()
    {
        if (State != AlarmState.Dismissed) State = AlarmState.Dismissed;
    }

    public void AlarmFormClosed(AlarmAction action)
    {
        switch (action)
        {
            case AlarmAction.FiveMinBefore:
                AlarmTimer.Stop();
                AlarmTimer = new Timer
                {
                    Interval = (int)(Time - DateTime.Now).TotalMilliseconds -
                               5 * 60 * 1000
                };
                AlarmTimer.Start();
                State = AlarmState.Snoozed;
                break;
            case AlarmAction.ZeroMinBefore:
                AlarmTimer.Stop();
                AlarmTimer = new Timer
                {
                    Interval = (int)(Time - DateTime.Now).TotalMilliseconds -
                               10 * 60 * 1000
                };
                AlarmTimer.Start();
                State = AlarmState.Snoozed;
                break;
            case AlarmAction.SnoozeFiveMin:
                AlarmTimer.Stop();
                AlarmTimer = new Timer { Interval = 5 * 60 * 1000 };
                AlarmTimer.Start();
                State = AlarmState.Snoozed;
                break;
            case AlarmAction.SnoozeTenMin:
                AlarmTimer.Stop();
                AlarmTimer = new Timer { Interval = 10 * 60 * 1000 };
                AlarmTimer.Start();
                State = AlarmState.Snoozed;
                break;
            case AlarmAction.Dismiss:
                AlarmTimer.Stop();
                State = AlarmState.Dismissed;
                break;
        }
    }
}