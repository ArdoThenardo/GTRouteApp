using Microsoft.Extensions.Options;
using GTRouteApp.Models;
using GTRouteApp.Services;

namespace GTRouteApp.Helpers;

public class ImageTransformers
{
    private readonly CloudinaryService cloudinaryService;

    public ImageTransformers(IOptions<GTRouteAppSettings> settings)
    {
        this.cloudinaryService = new CloudinaryService(settings);
    }

    public string GenerateThumbnail(string? url)
    {
        string imageUrl = url ?? "";
        string urlPartToTrim = GeneralConstants.CloudinaryBase;
        string source = "";
        if (imageUrl.StartsWith(urlPartToTrim))
        {
            source = imageUrl.Remove(0, urlPartToTrim.Length);
            var transformedUrl = cloudinaryService.GenerateSmallThumbnailImageUrl(source);

            return transformedUrl;
        }
        else
        {
            return imageUrl; // revert to original url, if url is not from cloudinary
        }
    }

    public string GenerateProgressive(string? url)
    {
        string imageUrl = url ?? "";
        string urlPartToTrim = GeneralConstants.CloudinaryBase;
        string source = "";
        if (imageUrl.StartsWith(urlPartToTrim))
        {
            source = imageUrl.Remove(0, urlPartToTrim.Length);
            var transformedUrl = cloudinaryService.GenerateProgressiveImageUrl(source);

            return transformedUrl;
        }
        else
        {
            return imageUrl; // revert to original url, if url is not from cloudinary or others
        }
    }
}