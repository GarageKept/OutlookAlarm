using GarageKept.OutlookAlarm.Alarm.Interfaces;

namespace GarageKept.OutlookAlarm.Alarm.AlarmManager;

public class AlarmEventArgs : EventArgs
{
    public IAlarm? Alarm;
    public AlarmEvent Event;

    public AlarmEventArgs(IAlarm alarm, AlarmEvent @event)
    {
        Alarm = alarm;
        Event = @event;
    }

    public AlarmEventArgs(AlarmEvent @event) { Event = @event; }
}