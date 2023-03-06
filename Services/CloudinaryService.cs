using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace GTRouteApp.Services;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;
    private const string _cloudinaryUrl = "cloudinary://423466551733793:GPePr1ZfMQVdereR6P7XjqNt5Jw@doo5vwi4i";

    public CloudinaryService()
    {
        _cloudinary = new Cloudinary(_cloudinaryUrl);
        _cloudinary.Api.Secure = true;
    }

    public string GenerateSmallThumbnailImageUrl(string source)
    {
        var transformation = new Transformation().Width(50).Height(50).Crop("crop"); // 256 128 fill
        var transformedUrl = _cloudinary.Api.UrlImgUp.Source(source).Transform(transformation).BuildUrl();

        return transformedUrl;
    }
}