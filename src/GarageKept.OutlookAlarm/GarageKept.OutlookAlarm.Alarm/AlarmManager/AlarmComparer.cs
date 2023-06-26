using GarageKept.OutlookAlarm.Alarm.Interfaces;

namespace GarageKept.OutlookAlarm.Alarm.AlarmManager;

public static class AlarmComparer
{
    public static bool AreEqual(IAlarm? alarm1, IAlarm? alarm2)
    {
        if (alarm1 == null && alarm2 == null) return true;

        if (alarm1 == null || alarm2 == null) return false;

        return string.Equals(alarm1.Name, alarm2.Name) && string.Equals(alarm1.Id, alarm2.Id) &&
               alarm1.Start == alarm2.Start && alarm1.End == alarm2.End && alarm1.ReminderTime == alarm2.ReminderTime &&
               alarm1.IsReminderEnabled == alarm2.IsReminderEnabled && alarm1.IsActive == alarm2.IsActive &&
               alarm1.IsAudible == alarm2.IsAudible && Equals(alarm1.AlarmColor, alarm2.AlarmColor) &&
               string.Equals(alarm1.CustomSound, alarm2.CustomSound) &&
               alarm1.HasCustomSound == alarm2.HasCustomSound && string.Equals(alarm1.Organizer, alarm2.Organizer) &&
               AreEqual(alarm1.Categories, alarm2.Categories) && string.Equals(alarm1.Location, alarm2.Location) &&
               string.Equals(alarm1.TeamsMeetingUrl, alarm2.TeamsMeetingUrl);
    }

    private static bool AreEqual(IEnumerable<string>? list1, IEnumerable<string>? list2)
    {
        if (list1 == null && list2 == null) return true;

        if (list1 == null || list2 == null) return false;

        var enumerable1 = list1 as string[] ?? list1.ToArray();
        var enumerable2 = list2 as string[] ?? list2.ToArray();

        if (enumerable1.Length != enumerable2.Length) return false;

        return !enumerable1.Where((t, i) => !string.Equals(t, enumerable2[i])).Any();
    }
}