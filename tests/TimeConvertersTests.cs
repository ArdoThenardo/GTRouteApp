using GTRouteApp.Helpers;

namespace GTRouteApp.Tests;

public class TimeConvertersTests
{
    [Fact]
    public void ConvertSecondToMinutesSecondsStringTest()
    {
        int seconds = 120;
        string minutesInString = TimeConverters.ConvertSecondsToMinutes(seconds);

        Assert.Equal("02:00", minutesInString);
    }
}