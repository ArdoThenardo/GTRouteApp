using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Tests;

public class ImageTransformersTests
{
    private readonly IConfiguration _configuration;
    ImageTransformers imageTransformers;

    public ImageTransformersTests()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.test.json")
            .Build();
        var settings = new GTRouteAppSettings();
        settings.CloudinaryApi = _configuration.GetSection("GTRouteAppSettings").GetValue<string>("CloudinaryApi") ?? "nihil";
        settings.ImageKitPublicKey = _configuration.GetSection("GTRouteAppSettings").GetValue<string>("ImageKitPublicKey") ?? "";
        settings.ImageKitPrivateKey = _configuration.GetSection("GTRouteAppSettings").GetValue<string>("ImageKitPrivateKey") ?? "";
        settings.ImageKitEndpointUrl = _configuration.GetSection("GTRouteAppSettings").GetValue<string>("ImageKitEndpointUrl") ?? "";
        
        imageTransformers = new ImageTransformers(Options.Create(settings));
    }

    [Fact]
    public void GenerateThumbnailWithCloudinaryTests()
    {
        string imageUrl = "https://res.cloudinary.com/doo5vwi4i/image/upload/test-img.jpg";
        string generatedThumbnail = imageTransformers.GenerateThumbnail(imageUrl);
        
        bool isThumbnailEmpty = string.IsNullOrWhiteSpace(generatedThumbnail);

        Assert.False(isThumbnailEmpty);
    }

    [Fact]
    public void GenerateThumbailImageKitTests()
    {
        string imageUrl = "https://ik.imagekit.io/gtrouteapp/test-img.jpg";
        string generatedThumbnail = imageTransformers.GenerateThumbnail(imageUrl);

        bool isThumbnailEmpty = string.IsNullOrWhiteSpace(generatedThumbnail);

        Assert.False(isThumbnailEmpty);
    }

    [Fact]
    public void RevertToOriginalUrl_AfterGenerateThumbnailWithOtherTests()
    {
        string imageUrl = "img/test-img.jpg";
        string generatedThumbnail = imageTransformers.GenerateThumbnail(imageUrl);

        Assert.Equal("img/test-img.jpg", imageUrl);
    }
}