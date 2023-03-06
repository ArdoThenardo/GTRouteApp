namespace GTRouteApp.Models;

public class FeaturedTrack
{
    public string? Slug { get; set; }
    public string? Name { get; set; }
    public string? LogoUrl { get; set; }
    public string? CoverUrl { get; set; }
    public string? Category { get; set; }
    public Country? Country { get; set; }
    public string? Text { get; set; }
}