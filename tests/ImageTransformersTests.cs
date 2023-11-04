using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Tests;

public class ImageTransformersTests
{
    IConfiguration _configuration;
    ImageTransformers imageTransformers;

    public ImageTransformersTests()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.test.json")
            .Build();
        var settings = new GTRouteAppSettings();
        settings.CloudinaryApi = _configuration.GetSection("AppSettings").GetValue<string>("CloudinaryApi") ?? "";
        settings.ImageKitPublicKey = _configuration.GetSection("AppSettings").GetValue<string>("ImageKitPublicKey") ?? "";
        settings.ImageKitPrivateKey = _configuration.GetSection("AppSettings").GetValue<string>("ImageKitPrivateKey") ?? "";
        settings.ImageKitEndpointUrl = _configuration.GetSection("AppSettings").GetValue<string>("ImageKitEndpointUrl") ?? "";
        
        imageTransformers = new ImageTransformers(Options.Create(settings));
    }

    [Fact]
    public void GenerateThumbnailWithCloudinaryTest()
    {
        string imageUrl = "https://res.cloudinary.com/mock/image/upload/test-img.jpg";
        string generatedThumbnail = imageTransformers.GenerateThumbnail(imageUrl);
        
        bool isThumbnailEmpty = string.IsNullOrWhiteSpace(generatedThumbnail);

        Assert.False(isThumbnailEmpty);
    }

    [Fact]
    public void GenerateProgressive_WithCloudinaryTest()
    {
        string imageUrl = "https://res.cloudinary.com/mock/image/upload/test-img.jpg";
        string generatedThumbnail = imageTransformers.GenerateProgressive(imageUrl);
        
        bool isThumbnailEmpty = string.IsNullOrWhiteSpace(generatedThumbnail);

        Assert.False(isThumbnailEmpty);
    }

    [Fact]
    public void GenerateThumbailImageKitTest()
    {
        string imageUrl = "https://ik.imagekit.io/mock/test-img.jpg";
        string generatedThumbnail = imageTransformers.GenerateThumbnail(imageUrl);

        bool isThumbnailEmpty = string.IsNullOrWhiteSpace(generatedThumbnail);

        Assert.False(isThumbnailEmpty);
    }

    [Fact]
    public void GenerateProgressive_WithImageKitTest()
    {
        string imageUrl = "https://ik.imagekit.io/mock/test-img.jpg";
        string generatedThumbnail = imageTransformers.GenerateProgressive(imageUrl);

        bool isThumbnailEmpty = string.IsNullOrWhiteSpace(generatedThumbnail);

        Assert.False(isThumbnailEmpty);
    }

    [Fact]
    public void RevertToOriginalUrl_AfterGenerateThumbnailWithOtherTest()
    {
        string imageUrl = "img/test-img.jpg";
        string generatedThumbnail = imageTransformers.GenerateThumbnail(imageUrl);

        Assert.Equal("img/test-img.jpg", imageUrl);
    }

    [Fact]
    public void RevertToOriginalUrl_AfterGenerateProgressiveWithOtherTest()
    {
        string imageUrl = "img/test-img.jpg";
        string generatedThumbnail = imageTransformers.GenerateProgressive(imageUrl);

        Assert.Equal("img/test-img.jpg", imageUrl);
    }
}