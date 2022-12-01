using System.ComponentModel.DataAnnotations;

namespace GTRouteApp.Data;

public class RaceTrackEditModel
{
    [Required(ErrorMessage = "Please enter the name of race track")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please select a country for the race track")]
    public string? CountryCode { get; set; }

    [Required(ErrorMessage = "Please insert a url to logo image of race track")]
    public string? LogoUrl { get; set; }

    [Required(ErrorMessage = "Please insert a url to cover image of race track")]
    public string? CoverUrl { get; set; }

    [Required(ErrorMessage = "Please select a category for the race track")]
    public string? Category { get; set; }

    [Required(ErrorMessage = "Please select a road type on the race track")]
    public string? RoadType { get; set; }

    [Required, Range(1, 20, ErrorMessage = "Please select a number of layout (min. 1) available for race track")]
    public int NumberOfLayouts { get; set; }
}