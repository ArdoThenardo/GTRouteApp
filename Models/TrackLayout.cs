namespace GTRouteApp.Data;

public class TrackLayout
{
    public string? Slug { get; set; }
    public string? LayoutName { get; set; }
    public string? MapUrl { get; set; }
    public double Length { get; set; }
    public int Corners { get; set; }
}