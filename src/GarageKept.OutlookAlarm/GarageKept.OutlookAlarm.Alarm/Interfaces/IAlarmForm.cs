﻿namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

internal interface IAlarmForm
{
    void Show();
    IAlarm Alarm { get; set; }
}