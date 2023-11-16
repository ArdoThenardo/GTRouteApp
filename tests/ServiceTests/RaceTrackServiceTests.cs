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

public class RaceTrackServiceTests
{
    IConfiguration _configuration;
    GTRouteAppSettings settings;
    MockHttpMessageHandler mockHttp;
    RaceTrackService service;
    ITestOutputHelper output;

    public RaceTrackServiceTests(ITestOutputHelper output)
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.test.json")
            .Build();

        settings = new GTRouteAppSettings();
        settings.BaseApi = _configuration.GetSection("AppSettings").GetValue<string>("BaseApi") ?? "";

        mockHttp = new MockHttpMessageHandler();
        
        var client = mockHttp.ToHttpClient();
        client.BaseAddress = new Uri(settings.BaseApi);
        
        service = new RaceTrackService(client, Options.Create(settings));

        this.output = output;
    }

    [Fact]
    public async Task FetchAll_WithoutPageAndLimit_RaceTracksTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/tracks/all")
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleAllRaceTracks);

        // call api to get result
        var response = await service.GetAllTracks();

        // assert
        Assert.NotNull(response);
        Assert.True(response.Count() > 0);
    }

    [Fact]
    public async Task FetchAll_RaceTracksTest()
    {
        service.SetRaceTracksCategory(BrowseCategory.Categories[0]); // All

        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/tracks/")
                .WithQueryString(new Dictionary<string, string>{
                    { "page", "1" },
                    { "limit", "8" },
                    { "shouldSortByName", "True" },
                    { "isDescending", "False" }
                })
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleAllRaceTracks);

        // call api to get result
        var response = await service.GetTracks(1);
        
        // assert
        Assert.NotNull(response);
        Assert.NotNull(response.ElementAt(0));
    }

    [Fact]
    public async Task FetchOriginalCircuit_RaceTracksTest()
    {
        service.SetRaceTracksCategory(BrowseCategory.Categories[1]); // Original Circuit

        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/tracks/")
                .WithQueryString(new Dictionary<string, string> {
                    { "page", "1" },
                    { "limit", "8" },
                    { "category", "Original Circuit" },
                    { "shouldSortByName", "True" },
                    { "isDescending", "False" }
                })
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleOriginalRaceTracks);

        // call api to get result
        var response = await service.GetTracks(1);

        // assert
        Assert.NotNull(response);
        Assert.Equal("Original Circuit", response.ElementAt(0).Category);
    }

    [Fact]
    public async Task FetchOffroad_RaceTracksTest()
    {
        service.SetRaceTracksCategory(BrowseCategory.Categories[4]); // Dirt / Snow

        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/offroads/")
                .WithQueryString(new Dictionary<string, string>{
                    { "page", "1" },
                    { "limit", "8" },
                    { "shouldSortByName", "True" },
                    { "isDescending", "False" }
                })
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleOffroadRaceTracks);

        // call api to get result
        var response = await service.GetTracks(1);
        
        // assert
        Assert.NotNull(response);
        Assert.Equal("Dirt Circuit", response.ElementAt(0).Category);
    }

    [Fact]
    private async Task FetchEmpty_AllWithoutPageAndLimit_RaceTracksTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/tracks/all")
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleEmptyJson);

        // call api to get result
        var response = await service.GetAllTracks();

        // assert
        Assert.Empty(response);
    }

    [Fact]
    private async Task FetchEmpty_RaceTracksTest()
    {
        service.SetRaceTracksCategory(BrowseCategory.Categories[0]); // All

        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/tracks/")
                .WithQueryString(new Dictionary<string, string>{
                    { "page", "1" },
                    { "limit", "8" },
                    { "shouldSortByName", "True" },
                    { "isDescending", "False" }
                })
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleEmptyJson);

        // call api to get result
        var response = await service.GetTracks(1);

        // assert
        Assert.Empty(response);
    }

    [Fact]
    private async Task FetchError_AllWithoutPageAndLimit_RaceTracksTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/tracks/all")
                .Respond(HttpStatusCode.InternalServerError, "application/json", SampleJson.SampleAllRaceTracks);

        // call api to get result
        var response = await service.GetAllTracks();

        // assert
        Assert.Empty(response);
        Assert.Equal(ErrorMessage.LoadTracksFailed, service.GetRecentErrorMessage());
    }

    [Fact]
    private async Task FetchError_RaceTracksTest()
    {
        service.SetRaceTracksCategory(BrowseCategory.Categories[0]); // All

        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/tracks/")
                .WithQueryString(new Dictionary<string, string>{
                    { "page", "1" },
                    { "limit", "8" },
                    { "shouldSortByName", "True" },
                    { "isDescending", "False" }
                })
                .Respond(HttpStatusCode.InternalServerError, "application/json", SampleJson.SampleAllRaceTracks);

        // call api to get result
        var response = await service.GetTracks(1);

        // assert
        Assert.Empty(response);
        Assert.Equal(ErrorMessage.LoadTracksFailed, service.GetRecentErrorMessage());
    }
}