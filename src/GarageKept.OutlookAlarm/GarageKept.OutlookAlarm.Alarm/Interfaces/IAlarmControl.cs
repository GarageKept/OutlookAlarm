﻿namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IAlarmControl
{
    IAlarm? Alarm { get; set; }
    bool Visible { get; set; }
    void StopTimers();
    void UpdateDisplay();
}