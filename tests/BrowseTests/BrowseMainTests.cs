using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;
using GTRouteApp.Helpers;
using GTRouteApp.Models;
using GTRouteApp.Services;
using GTRouteApp.Shared;
using GTRouteApp.Components.Browse;
using GTRouteApp.Tests.Sample;
using RichardSzalay.MockHttp;

namespace GTRouteApp.Tests;

public class BrowseMainTests: TestContext
{
    IConfiguration _configuration;
    GTRouteAppSettings settings;
    MockHttpMessageHandler mockHttp;
    RaceTrackService service;
    ITestOutputHelper output;

    public BrowseMainTests(ITestOutputHelper output)
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
        Services.AddSingleton(service);

        this.output = output;
    }

    [Fact]
    public async Task LoadBrowseMain_WithData_GalleryModeTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/tracks/")
                .WithQueryString(new Dictionary<string, string>{
                    { "page", "1" },
                    { "limit", "8" },
                    { "shouldSortByName", "True" },
                    { "isDescending", "False" }
                })
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleAllRaceTracks);

        // render browse page
        var cut = RenderComponent<BrowseMain>(parameters => parameters
            .Add(p => p.SelectedModeQuery, BrowseModeQuery.GalleryMode)
            .Add(p => p.SelectedSortQuery, BrowseSortQuery.Alphabetical));
        await Task.Delay(1000);

        // assert
        Assert.True(cut.HasComponent<BrowseSubheader>());
        Assert.True(cut.HasComponent<BrowseGallery>());
        Assert.Equal(BrowseSortQuery.Alphabetical, cut.Instance.SelectedSortQuery);
    }

    [Fact]
    public async Task LoadBrowseMain_WithData_ListModeTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/tracks/")
                .WithQueryString(new Dictionary<string, string>{
                    { "page", "1" },
                    { "limit", "8" },
                    { "shouldSortByName", "True" },
                    { "isDescending", "True" }
                })
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleAllRaceTracks);

        // render browse page
        var cut = RenderComponent<BrowseMain>(parameters => parameters
            .Add(p => p.SelectedModeQuery, BrowseModeQuery.ListMode)
            .Add(p => p.SelectedSortQuery, BrowseSortQuery.AlphabeticalReverse));
        await Task.Delay(1000);
        
        // assert
        Assert.True(cut.HasComponent<BrowseSubheader>());
        Assert.True(cut.HasComponent<BrowseList>());
        Assert.Equal(BrowseSortQuery.AlphabeticalReverse, cut.Instance.SelectedSortQuery);
    }

    [Fact]
    public async Task LoadBrowseMain_WithData_GridModeTest()
    {
        // set expected uri & response
        mockHttp.When($"{settings.BaseApi}/tracks/")
                .WithQueryString(new Dictionary<string, string>{
                    { "page", "1" },
                    { "limit", "8" },
                    { "shouldSortByName", "False" },
                    { "isDescending", "True" }
                })
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleAllRaceTracks);

        // render browse page
        var cut = RenderComponent<BrowseMain>(parameters => parameters
            .Add(p => p.SelectedModeQuery, BrowseModeQuery.GridMode)
            .Add(p => p.SelectedSortQuery, BrowseSortQuery.Standard));
        await Task.Delay(1000);

        // assert
        Assert.True(cut.HasComponent<BrowseSubheader>());
        Assert.True(cut.HasComponent<BrowseGrid>());
        Assert.Equal(BrowseSortQuery.Standard, cut.Instance.SelectedSortQuery);
    }

    [Fact]
    public void LoadBrowseMain_WithoutDataTest()
    {
        var cut = RenderComponent<BrowseMain>();

        Assert.True(cut.HasComponent<NoDataView>());
    }
}