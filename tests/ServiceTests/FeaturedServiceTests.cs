using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;
using GTRouteApp.Models;
using GTRouteApp.Services;
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
            .Respond(HttpStatusCode.OK, "application/json", "{ \"numberOfData\": 1, \"data\": [{ \"slug\": \"test\", \"name\": \"Test\" }] }");

        // call api to get result
        var response = await service.GetFeaturedTracks();

        // assert
        Assert.Single(response);
    }
}