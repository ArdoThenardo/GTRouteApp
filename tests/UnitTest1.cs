using System.Globalization;
using GTRouteApp.Helpers;

namespace GTRouteApp.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        int timeInSeconds = 120;
        string minutesInString = TimeConverters.ConvertSecondsToMinutes(timeInSeconds);

        bool result = minutesInString.Equals("02:00");

        Assert.True(result, "120s should be formatted to 02:00");
    }

    [Fact]
    public void Test2()
    {
        DateTime targetReleaseDate = new DateTime();
        string formattedReleaseDate = ReleaseDate.GetFormattedReleaseDate();

        bool result = formattedReleaseDate.Equals(TimeZoneInfo.ConvertTimeFromUtc(targetReleaseDate, TimeZoneInfo.Utc)
                                                    .ToString("MMMM d, yyyy - hh:mm tt", CultureInfo.InvariantCulture));
        
        Assert.True(result, "Latest build date is as same as target release date.");
    }
}