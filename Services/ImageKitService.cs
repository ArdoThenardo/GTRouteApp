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

    public void GenerateSmallThumbnailImageUrl(string source)
    {
        
    }

    public void GenerateProgressiveImageUrl(string source)
    {
        
    }
}