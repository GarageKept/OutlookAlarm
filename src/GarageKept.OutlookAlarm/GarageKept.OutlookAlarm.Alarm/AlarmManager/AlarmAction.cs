namespace GarageKept.OutlookAlarm.Alarm.AlarmManager;

public enum AlarmAction
{
    [Display("15 Minutes Before Start")] FifteenMinBefore,
    [Display("10 Minutes Before Start")] TenMinBefore,
    [Display("5 Minutes Before Start")] FiveMinBefore,
    [Display("0 Minutes Before Start")] ZeroMinBefore,
    [Display("5 Minutes")] SnoozeFiveMin,
    [Display("10 Minutes")] SnoozeTenMin,
    [Display("Dismissed")] Dismiss,
    [Display("Dismissed and Remove")] Remove
}