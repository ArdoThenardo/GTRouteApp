using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GTRouteApp.Models;

namespace GTRouteApp.Services;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;
    private readonly string _cloudinaryUrl;

    public CloudinaryService(IOptions<GTRouteAppSettings> settings)
    {
        _cloudinaryUrl = settings.Value.CloudinaryApi;
        _cloudinary = new Cloudinary(_cloudinaryUrl);
        _cloudinary.Api.Secure = true;
    }

    public string GenerateSmallThumbnailImageUrl(string source)
    {
        var transformation = new Transformation().Width(256).Height(128).Crop("fill");
        var transformedUrl = _cloudinary.Api.UrlImgUp.Source(source).Transform(transformation).BuildUrl();

        return transformedUrl;
    }
}