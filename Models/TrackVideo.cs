namespace GTRouteApp.Models;

public class TrackVideo
{
    public string? Slug { get; set; }
    public string? VideoName { get; set; }
    public string? VideoUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public int DurationInSeconds { get; set; }
    public string? VideoType { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
}