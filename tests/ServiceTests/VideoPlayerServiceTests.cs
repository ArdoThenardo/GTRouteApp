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

public class VideoPlayerServiceTests
{
    IConfiguration _configuration;
    GTRouteAppSettings settings;
    MockHttpMessageHandler mockHttp;
    VideoPlayerService service;
    ITestOutputHelper output;

    public VideoPlayerServiceTests(ITestOutputHelper output)
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.test.json")
            .Build();

        settings = new GTRouteAppSettings();
        settings.BaseApi = _configuration.GetSection("AppSettings").GetValue<string>("BaseApi") ?? "";

        mockHttp = new MockHttpMessageHandler();
        
        var client = mockHttp.ToHttpClient();
        client.BaseAddress = new Uri(settings.BaseApi);
        
        service = new VideoPlayerService(client, Options.Create(settings));

        this.output = output;
    }

    [Fact]
    public async Task GetVideoByIdTest()
    {
        var videoId = "video-id";

        mockHttp.When($"{settings.BaseApi}/video/{videoId}")
            .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleVideoData);
        
        var response = await service.GetVideoDataById(videoId);
        
        Assert.NotNull(response);
        Assert.Equal(response.Id, videoId);
        Assert.Equal("02:00", TimeConverters.ConvertSecondsToMinutes(response.DurationInSeconds.GetValueOrDefault()));
    }

    [Fact]
    public async Task GetVideo_ErrorTest()
    {
        var videoId = "video-id";

        mockHttp.When($"{settings.BaseApi}/video/{videoId}")
            .Respond(HttpStatusCode.InternalServerError, "application/json", SampleJson.SampleVideoData);
        
        var response = await service.GetVideoDataById(videoId);

        Assert.Equal(ErrorMessage.LoadVideoFailed, service.GetRecentErrorMessage());
    }

    [Fact]
    public async Task GetOtherVideosTest()
    {
        var videoId = "video-id";

        mockHttp.When($"{settings.BaseApi}/video/{videoId}/other")
            .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleOtherVideos);

        var response = await service.GetOtherVideosById(videoId);

        Assert.NotNull(response);
        Assert.Equal(2, response.Count());
    }

    [Fact]
    public async Task GetOtherVideos_EmptyTest()
    {
        var videoId = "video-id";

        mockHttp.When($"{settings.BaseApi}/video/{videoId}/other")
            .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleEmptyJson);

        var response = await service.GetOtherVideosById(videoId);

        Assert.Equal(ErrorMessage.NoRelatedVideos, service.GetRecentErrorForOtherVideos());
    }

    [Fact]
    public async Task GetOtherVideos_ErrorTest()
    {
        var videoId = "video-id";

        mockHttp.When($"{settings.BaseApi}/video/{videoId}/other")
            .Respond(HttpStatusCode.InternalServerError, "application/json", SampleJson.SampleOtherVideos);

        var response = await service.GetOtherVideosById(videoId);

        Assert.Equal(ErrorMessage.LoadOtherVideoFailed, service.GetRecentErrorForOtherVideos());
    }
}