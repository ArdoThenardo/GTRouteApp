using System.Globalization;
using IntelliTect.Multitool;

namespace GTRouteApp.Helpers;

public static class ReleaseDate
{
    public static string GetFormattedReleaseDate()
    {
        DateTime releaseDate = ReleaseDateAttribute.GetReleaseDate() ?? new DateTime();
        string formattedReleaseDate = TimeZoneInfo.ConvertTimeFromUtc(releaseDate, TimeZoneInfo.Utc)
                                        .ToString("MMMM d, yyyy - hh:mm tt", CultureInfo.InvariantCulture);

        return formattedReleaseDate;
    }
}