namespace GTRouteApp.Helpers;

public static class GeneralConstants
{
    public const string CloudinaryBase = "https://res.cloudinary.com/doo5vwi4i/image/upload/";
    public const string ImageKitBase = "https://ik.imagekit.io/gtrouteapp";
    public const string PlaceholderImageUrl = "img/empty720.png";
}

public static class RaceTrackCategory
{
    public const string OriginalCircuit = "Original Circuit";
    public const string RealCircuit = "Real Circuit";
    public const string CityCircuit = "City Circuit";
    public const string DirtCircuit = "Dirt Circuit";
    public const string SnowCircuit = "Snow Circuit";
}

public static class BrowseCategory
{
    public static readonly string[] Categories = {
        "All",
        "Original Circuits",
        "Real Circuits",
        "City Circuits",
        "Dirt / Snow"
    };
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

public static class DetailSidebarMenu
{
    public const string General = "General";
    public const string Layout = "Track Layouts";
    public const string Showcase = "Video Showcase";
    public const string Gallery = "Image Gallery";
}

public static class SelectedSubdetail
{
    public const string General = "general";
    public const string Layout = "layout";
    public const string Showcase = "video";
    public const string Gallery = "image";
}