namespace GTRouteApp.Data;

public class TrackDetail
{
    public string? Name { get; set; }
    public string? CoverUrl { get; set; }
    public string? LogoUrl { get; set; }
    public string? Category { get; set; }
    public string? RoadType { get; set; }
    public int NumberOfLayouts { get; set; } = 0;
    public Country? Country { get; set; }
    public List<TrackLayout> Layouts { get; set; } = new();
    public List<TrackImage> Images { get; set; } = new();
    public List<RaceTrack> OtherTracks { get; set; } = new();
}