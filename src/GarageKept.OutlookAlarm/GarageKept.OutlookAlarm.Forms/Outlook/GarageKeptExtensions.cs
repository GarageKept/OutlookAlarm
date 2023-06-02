using GarageKept.OutlookAlarm.Forms.Common;
using Microsoft.Office.Interop.Outlook;

namespace GarageKept.OutlookAlarm.Forms.Outlook;

public static class GarageKeptExtensions
{
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