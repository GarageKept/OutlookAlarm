using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Outlook;
using Application = Microsoft.Office.Interop.Outlook.Application;

namespace GarageKept.OutlookAlarm.Alarm.AlarmSources.Outlook;

public static class OutlookAlarmSourceExtensions
{
    public static Color GetCategoryColor(this string categoryName)
    {
        if (string.IsNullOrEmpty(categoryName)) return SystemColors.Control;

        categoryName = categoryName.GetFirstFromCsv();

        if (string.IsNullOrWhiteSpace(categoryName)) return Color.Empty;

        // Create an instance of the Outlook Application
        var outlookApp = new Application();

        // Get the default namespace
        var outlookNamespace = outlookApp.GetNamespace("MAPI");

        // Retrieve the Category from the Categories collection
        var category = outlookNamespace.Categories[categoryName];

        if(category is null) return Color.Empty;

        // Get the Category Color
        var categoryColor = category.Color.ToColor();

        // Release the COM objects
        Marshal.ReleaseComObject(category);
        Marshal.ReleaseComObject(outlookNamespace);
        Marshal.ReleaseComObject(outlookApp);

        return categoryColor;
    }

    public static string GetFirstFromCsv(this string csvString)
    {
        var csvValues = csvString.Split(',');

        return csvValues.Length > 0 ? csvValues[0] : string.Empty;
    }

    public static Color ToColor(this OlCategoryColor olCategoryColor)
    {
        return olCategoryColor switch
        {
            OlCategoryColor.olCategoryColorNone => SystemColors.Control,
            OlCategoryColor.olCategoryColorRed => Color.FromArgb(240, 125, 136),
            OlCategoryColor.olCategoryColorOrange => Color.FromArgb(255, 140, 0),
            OlCategoryColor.olCategoryColorPeach => Color.FromArgb(254, 203, 111),
            OlCategoryColor.olCategoryColorYellow => Color.FromArgb(255, 241, 0),
            OlCategoryColor.olCategoryColorGreen => Color.FromArgb(95, 190, 125),
            OlCategoryColor.olCategoryColorTeal => Color.FromArgb(51, 186, 177),
            OlCategoryColor.olCategoryColorOlive => Color.FromArgb(163, 179, 103),
            OlCategoryColor.olCategoryColorBlue => Color.FromArgb(85, 171, 229),
            OlCategoryColor.olCategoryColorPurple => Color.FromArgb(168, 149, 226),
            OlCategoryColor.olCategoryColorMaroon => Color.FromArgb(228, 139, 181),
            OlCategoryColor.olCategoryColorSteel => Color.FromArgb(185, 192, 203),
            OlCategoryColor.olCategoryColorDarkSteel => Color.FromArgb(76, 89, 110),
            OlCategoryColor.olCategoryColorGray => Color.FromArgb(171, 171, 171),
            OlCategoryColor.olCategoryColorDarkGray => Color.FromArgb(102, 102, 102),
            OlCategoryColor.olCategoryColorBlack => Color.FromArgb(71, 71, 71),
            OlCategoryColor.olCategoryColorDarkRed => Color.FromArgb(145, 10, 25),
            OlCategoryColor.olCategoryColorDarkOrange => Color.FromArgb(206, 75, 40),
            OlCategoryColor.olCategoryColorDarkPeach => Color.FromArgb(164, 115, 50),
            OlCategoryColor.olCategoryColorDarkYellow => Color.FromArgb(176, 169, 35),
            OlCategoryColor.olCategoryColorDarkGreen => Color.FromArgb(2, 104, 2),
            OlCategoryColor.olCategoryColorDarkTeal => Color.FromArgb(28, 99, 103),
            OlCategoryColor.olCategoryColorDarkOlive => Color.FromArgb(92, 106, 34),
            OlCategoryColor.olCategoryColorDarkBlue => Color.FromArgb(37, 64, 105),
            OlCategoryColor.olCategoryColorDarkPurple => Color.FromArgb(86, 38, 133),
            OlCategoryColor.olCategoryColorDarkMaroon => Color.FromArgb(128, 39, 93),
            _ => SystemColors.Control
        };
    }

    public static ResponseType ResponseTypeConverter(this OlResponseStatus status)
    {
        return status switch
        {
            OlResponseStatus.olResponseAccepted => ResponseType.Accepted,
            OlResponseStatus.olResponseDeclined => ResponseType.Declined,
            OlResponseStatus.olResponseNone => ResponseType.None,
            OlResponseStatus.olResponseNotResponded => ResponseType.NotResponded,
            OlResponseStatus.olResponseOrganized => ResponseType.Organized,
            OlResponseStatus.olResponseTentative => ResponseType.Tentative,
            _ => ResponseType.None
        };
    }
}