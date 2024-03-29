using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;
using GTRouteApp.Models;
using GTRouteApp.Helpers;
using GTRouteApp.Services;
using GTRouteApp.Tests.Sample;
using RichardSzalay.MockHttp;

namespace GTRouteApp.Tests;

public class FeaturedServiceTests
{
    IConfiguration _configuration;
    GTRouteAppSettings settings;
    MockHttpMessageHandler mockHttp;
    FeaturedService service;
    ITestOutputHelper output;

    public FeaturedServiceTests(ITestOutputHelper output)
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.test.json")
            .Build();

        settings = new GTRouteAppSettings();
        settings.BaseApi = _configuration.GetSection("AppSettings").GetValue<string>("BaseApi") ?? "";

        mockHttp = new MockHttpMessageHandler();
    
        var client = mockHttp.ToHttpClient();
        client.BaseAddress = new Uri(settings.BaseApi);
        
        service = new FeaturedService(client, Options.Create(settings));

        this.output = output;
    }

    [Fact]
    public async Task Fetch_FeaturedTracksTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/featured")
            .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleFeaturedTrackJson);

        // call api to get result
        var response = await service.GetFeaturedTracks();

        // assert
        Assert.Equal(2, response.Count());
        Assert.Equal("test-track", response.ElementAt(0).Slug);
        Assert.Equal("Original Circuit", response.ElementAt(0).Category);
        Assert.Equal("gb", response.ElementAt(0).Country!.code);
        Assert.Equal("test-track-2", response.ElementAt(1).Slug);
        Assert.Equal("Real Circuit", response.ElementAt(1).Category);
        Assert.Equal("jp", response.ElementAt(1).Country!.code);
    }

    [Fact]
    public async Task FetchEmpty_FeaturedTracksTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/featured")
            .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleEmptyJson);

        // call api to get result
        var response = await service.GetFeaturedTracks();

        // assert
        Assert.Empty(response);
    }

    [Fact]
    public async Task FetchError_FeaturedTracksTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/featured")
            .Respond(HttpStatusCode.InternalServerError, "application/json", SampleJson.SampleFeaturedTrackJson);

        // call api to get result
        var response = await service.GetFeaturedTracks();

        // assert
        Assert.Empty(response);
    }

    [Fact]
    public async Task Fetch_FeaturedVideoTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/featured/media/video?numberOfMedia=2")
            .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleFeaturedVideoJson);

        // call api to get result
        var response = await service.GetFeaturedMediaVideo(2);

        // assert
        Assert.Equal(2, response.Count());
        Assert.Equal("video-id-002", response.ElementAt(1).VideoId);
        Assert.Equal("01:00", TimeConverters.ConvertSecondsToMinutes(response.ElementAt(1).DurationInSeconds.GetValueOrDefault()));
    }

    [Fact]
    public async Task FetchError_FeaturedVideoTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/featured/media/video?numberOfMedia=2")
            .Respond(HttpStatusCode.InternalServerError, "application/json", SampleJson.SampleFeaturedVideoJson);

        // call api to get result
        var response = await service.GetFeaturedMediaVideo(2);

        // assert
        Assert.Empty(response);
    }

    [Fact]
    public async Task Fetch_FeaturedImageTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/featured/media/image?numberOfMedia=2")
            .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleFeaturedImageJson);

        // call api to get result
        var response = await service.GetFeaturedMediaImage(2);

        // assert
        Assert.Equal(2, response.Count());
        Assert.Equal("image-02", response.ElementAt(1).ImageName);
        Assert.Equal("img/track-02.jpg", response.ElementAt(1).ImageUrl);
    }

    [Fact]
    public async Task FetchError_FeaturedImageTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/featured/media/video?numberOfMedia=2")
            .Respond(HttpStatusCode.InternalServerError, "application/json", SampleJson.SampleFeaturedImageJson);

        // call api to get result
        var response = await service.GetFeaturedMediaImage(2);

        // assert
        Assert.Empty(response);
    }
}