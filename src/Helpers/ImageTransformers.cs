using Microsoft.Extensions.Options;
using GTRouteApp.Models;
using GTRouteApp.Services;

namespace GTRouteApp.Helpers;

public class ImageTransformers
{
    private readonly CloudinaryService cloudinaryService;
    private readonly ImageKitService imageKitService;

    public ImageTransformers(IOptions<GTRouteAppSettings> settings)
    {
        this.cloudinaryService = new CloudinaryService(settings);
        this.imageKitService = new ImageKitService(settings);
    }

    public string GenerateThumbnail(string? url)
    {
        string imageUrl = url ?? "";
        if (imageUrl.StartsWith(GeneralConstants.CloudinaryBase))
        {
            var source = imageUrl.Remove(0, GeneralConstants.CloudinaryBase.Length);
            var transformedUrl = cloudinaryService.GenerateSmallThumbnailImageUrl(source);

            return transformedUrl;
        }
        else if (imageUrl.StartsWith(GeneralConstants.ImageKitBase))
        {
            var transformedUrl = imageKitService.GenerateSmallThumbnailImageUrl(imageUrl);

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
        if (imageUrl.StartsWith(GeneralConstants.CloudinaryBase))
        {
            var source = imageUrl.Remove(0, GeneralConstants.CloudinaryBase.Length);
            var transformedUrl = cloudinaryService.GenerateProgressiveImageUrl(source);

            return transformedUrl;
        }
        else if (imageUrl.StartsWith(GeneralConstants.ImageKitBase))
        {
            var transformedUrl = imageKitService.GenerateProgressiveImageUrl(imageUrl);

            return transformedUrl;
        }
        else
        {
            return imageUrl; // revert to original url, if url is not from cloudinary or others
        }
    }
}