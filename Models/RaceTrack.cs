namespace GTRouteApp.Models;

public class RaceTrack {
    public string? Slug { get; set; }
    public string? Name { get; set; }
    public string? LogoUrl { get; set; }
    public string? CoverUrl { get; set; }
    public string? Category { get; set; }
    public string? RoadType { get; set; }
    public int NumberOfLayouts { get; set; }
    public Country? Country { get; set; }
}