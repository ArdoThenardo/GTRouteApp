namespace GTRouteApp.Helpers;

public static class RaceTrackCategory
{
    public const string OriginalCircuit = "Original Circuit";
    public const string RealCircuit = "Real Circuit";
    public const string CityCircuit = "City Circuit";
    public const string DirtCircuit = "Dirt Circuit";
    public const string SnowCircuit = "Snow Circuit";
}

public static class ErrorMessage
{
    public const string NoData = "There is no data.";
    public const string LoadDataFailed = "Unable to get data from server. Please try again.";
    public const string NoTrack = "There is no track data.";
    public const string LoadTracksFailed = "Unable to get track data from server. Please try again.";
    public const string LoadMoreTracksFailed = "There was a problem when trying to load more race tracks. Please try again.";
    public const string LoadDetailFailed = "Unable to get track detail data froms server. Please try again.";
}