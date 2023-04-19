namespace GTRouteApp.Models;

public class FeaturedImage
{
    public string? Slug { get; set; }
    public string? TrackName { get; set; }
    public string? LogoUrl { get; set; }
    public string? Category { get; set; }
    public Country Country { get; set; } = new Country();
    public string? ImageName { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
}