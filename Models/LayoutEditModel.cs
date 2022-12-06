using System.ComponentModel.DataAnnotations;

namespace GTRouteApp.Data;

public class LayoutEditModel
{
    [Required(ErrorMessage = "Please enter the name of track layout")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please insert a url to map image of track layout")]
    public string? MapUrl { get; set; }

    [Required(ErrorMessage = "Please insert the length of track layout"), 
    Range(0.1, Double.MaxValue, ErrorMessage = "Please insert the length of track layout (min. 0.1)")]
    public double Length { get; set; }

    [Required(ErrorMessage = "Please insert the number of turns on track layout"), Range(0, int.MaxValue, ErrorMessage = "Please insert the number of turns (min. 0) on track layout")]
    public int Corners { get; set; }
}