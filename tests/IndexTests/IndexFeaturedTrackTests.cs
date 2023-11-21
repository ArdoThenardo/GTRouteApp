using System.Text.Json;
using Xunit.Abstractions;
using GTRouteApp.Models;
using GTRouteApp.Components.Home;
using GTRouteApp.Tests.Sample;

namespace GTRouteApp.Tests;

public class IndexFeaturedTrackTests: TestContext
{
    JsonSerializerOptions serializerOptions;
    FakeNavigationManager navManager;
    ITestOutputHelper output;

    public IndexFeaturedTrackTests(ITestOutputHelper output)
    {
        serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        navManager = Services.GetRequiredService<FakeNavigationManager>();

        this.output = output;
    }

    [Fact]
    public void LoadIndexFeaturedTrack_WithParametersTest()
    {
        var featuredTracksContent = JsonSerializer.Deserialize<BaseModel<List<FeaturedTrack>>>(
            SampleJson.SampleFeaturedTrackJson, serializerOptions)!;
        var featuredImagesContent = JsonSerializer.Deserialize<BaseModel<List<FeaturedImage>>>(
            SampleJson.SampleFeaturedImageJson, serializerOptions)!;
        var featuredVideosContent = JsonSerializer.Deserialize<BaseModel<List<FeaturedVideo>>>(
            SampleJson.SampleFeaturedVideoJson, serializerOptions)!;

        var cut = RenderComponent<IndexFeaturedTrack>(parameters => parameters
            .Add(p => p.Tracks, featuredTracksContent.Data)
            .Add(p => p.FeaturedImages, featuredImagesContent.Data)
            .Add(p => p.FeaturedVideos, featuredVideosContent.Data));
        
        Assert.True(cut.Instance.Tracks.Count() > 0);
        Assert.True(cut.Instance.FeaturedImages.Count() > 0);
        Assert.True(cut.Instance.FeaturedVideos.Count() > 0);
    }

    [Fact]
    public void AccessFeaturedTrackTest()
    {
        var featuredTracksContent = JsonSerializer.Deserialize<BaseModel<List<FeaturedTrack>>>(
            SampleJson.SampleFeaturedTrackJson, serializerOptions)!;

        var cut = RenderComponent<IndexFeaturedTrack>(parameters => parameters
            .Add(p => p.Tracks, featuredTracksContent.Data));
        
        cut.Find("article").Click();

        Assert.Equal($"{navManager.BaseUri}browse/detail/{cut.Instance.Tracks.ElementAt(0).Slug}?origin_page=home", 
                    navManager.Uri);
    }
}