namespace GTRouteApp.Models;

public class FeaturedVideo
{
    public string? Slug { get; set; }
    public string? TrackName { get; set; }
    public string? LogoUrl { get; set; }
    public string? Category { get; set; }
    public Country Country { get; set; } = new Country();
    public string? VideoName { get; set; }
    public string? VideoUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public int? DurationInSeconds { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
}