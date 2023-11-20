using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;
using GTRouteApp.Models;
using GTRouteApp.Services;
using GTRouteApp.Shared;
using GTRouteApp.Components.Home;
using GTRouteApp.Tests.Sample;
using RichardSzalay.MockHttp;

namespace GTRouteApp.Tests;

public class IndexTests: TestContext
{
    IConfiguration _configuration;
    GTRouteAppSettings settings;
    MockHttpMessageHandler mockHttp;
    FeaturedService service;
    ITestOutputHelper output;

    public IndexTests(ITestOutputHelper output)
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
        Services.AddSingleton(service);

        this.output = output;
    }

    [Fact]
    public async Task LoadIndexPage_WithDataTest()
    {
        // set expected uri & response for featured tracks
        mockHttp.When($"{settings.BaseApi}/featured")
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleFeaturedTrackJson);
        // set expected uri & response for featured videos
        mockHttp.When($"{settings.BaseApi}/featured/media/video?numberOfMedia=2")
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleFeaturedVideoJson);
        // set expected uri & response for featured images
        mockHttp.When($"{settings.BaseApi}/featured/media/image?numberOfMedia=4")
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleFeaturedImageJson);

        // render index / home page
        var cut = RenderComponent<GTRouteApp.Components.Home.Index>();
        await Task.Delay(1000);
        
        // assert
        Assert.True(cut.HasComponent<IndexSubheader>());
        Assert.True(cut.HasComponent<IndexFeaturedTrack>());
        Assert.True(cut.HasComponent<IndexFeaturedMediaVideo>());
        Assert.True(cut.HasComponent<IndexFeaturedMediaImage>());
    }

    [Fact]
    public void LoadIndexPage_WithoutDataTest()
    {
        // render index / home page
        var cut = RenderComponent<GTRouteApp.Components.Home.Index>();

        // assert
        Assert.True(cut.HasComponent<NoDataView>());
    }
}