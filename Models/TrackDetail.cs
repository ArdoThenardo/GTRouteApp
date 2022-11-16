namespace GTRouteApp.Data;

public class TrackDetail
{
    public string? Name { get; set; }
    public string? LogoUrl { get; set; }
    public string? Category { get; set; }
    public string? RoadType { get; set; }
    public Country? Country { get; set; }
    public List<TrackLayout> Layouts { get; set; } = new();
}