using System.Globalization;
using GTRouteApp.Helpers;

namespace GTRouteApp.Tests;

public class ReleaseDateTests
{
    [Fact]
    public void GetTargettedReleaseDateBuildTest()
    {
        DateTime targetReleaseDate = new DateTime();
        string formattedTargetReleaseDate = TimeZoneInfo.ConvertTimeFromUtc(targetReleaseDate, TimeZoneInfo.Utc)
                                                .ToString("MMMM d, yyyy - hh:mm tt", CultureInfo.InvariantCulture);

        string formattedActualReleaseDate = ReleaseDate.GetFormattedReleaseDate();

        Assert.Equal(formattedActualReleaseDate, formattedTargetReleaseDate);
    }
}