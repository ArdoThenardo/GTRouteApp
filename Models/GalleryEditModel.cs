using System.ComponentModel.DataAnnotations;

namespace GTRouteApp.Models;

public class GalleryEditModel
{
    [Required(ErrorMessage = "Please enter the name of image")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please insert the author or source of image")]
    public string? Author { get; set; }

    [Required(ErrorMessage = "Please insert the url to image")]
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "Please insert the description of the image")]
    public string? Description { get; set; }
}