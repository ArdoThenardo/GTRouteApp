using Microsoft.Extensions.Options;
using Imagekit.Sdk;
using GTRouteApp.Models;

namespace GTRouteApp.Services;

public class ImageKitService
{
    private readonly ImagekitClient _imageKitClient;

    public ImageKitService(IOptions<GTRouteAppSettings> settings)
    {
        _imageKitClient = new ImagekitClient(
            settings.Value.ImageKitPublicKey, 
            settings.Value.ImageKitPrivateKey, 
            settings.Value.ImageKitEndpointUrl);
    }

    public string GenerateSmallThumbnailImageUrl(string source)
    {
        var transformation = new Transformation().Width(256).Height(128).Crop("maintain_ratio");
        string transformedUrl = _imageKitClient.Url(transformation).Src(source).Generate();
        
        return transformedUrl;
    }

    public string GenerateProgressiveImageUrl(string source)
    {
        var transformation = new Transformation().Progressive(true);
        string transformedUrl = _imageKitClient.Url(transformation).Src(source).Generate();

        return transformedUrl;
    }
}