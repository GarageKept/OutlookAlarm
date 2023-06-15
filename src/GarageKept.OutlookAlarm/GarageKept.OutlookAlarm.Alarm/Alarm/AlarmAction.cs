namespace GarageKept.OutlookAlarm.Alarm.Common;

public enum AlarmAction
{
    [Display("5 Minutes Before Start")] FiveMinBefore,
    [Display("0 Minutes Before Start")] ZeroMinBefore,
    [Display("5 Minutes")] SnoozeFiveMin,
    [Display("10 Minutes")] SnoozeTenMin,
    [Display("Dismissed")] Dismiss
}