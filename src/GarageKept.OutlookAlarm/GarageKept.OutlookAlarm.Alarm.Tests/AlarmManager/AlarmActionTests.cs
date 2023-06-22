using GarageKept.OutlookAlarm.Alarm.AlarmManager;

namespace GarageKept.OutlookAlarm.Alarm.Tests.AlarmManager;

[TestClass]
public class AlarmActionTests
{
    [TestMethod]
    public void EnumValues_ShouldHaveCorrectDisplayAttributes()
    {
        Assert.AreEqual("5 Minutes Before Start", GetDisplayAttributeValue(AlarmAction.FiveMinBefore));
        Assert.AreEqual("0 Minutes Before Start", GetDisplayAttributeValue(AlarmAction.ZeroMinBefore));
        Assert.AreEqual("5 Minutes", GetDisplayAttributeValue(AlarmAction.SnoozeFiveMin));
        Assert.AreEqual("10 Minutes", GetDisplayAttributeValue(AlarmAction.SnoozeTenMin));
        Assert.AreEqual("Dismissed", GetDisplayAttributeValue(AlarmAction.Dismiss));
    }

    private static string? GetDisplayAttributeValue(AlarmAction action)
    {
        var fieldInfo = typeof(AlarmAction).GetField(action.ToString());
        var displayAttribute =
            fieldInfo?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
        return displayAttribute?.Value;
    }
}