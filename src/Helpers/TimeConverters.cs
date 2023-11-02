namespace GTRouteApp.Helpers;

public class TimeConverters
{
    public static string ConvertSecondsToMinutes(int seconds)
    {
        TimeSpan timespan = TimeSpan.FromSeconds(Convert.ToDouble(seconds));
        string converted = timespan.ToString(@"mm\:ss");

        return converted;
    }
}