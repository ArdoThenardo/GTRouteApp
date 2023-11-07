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

public class TrackDetailServiceTests
{
    IConfiguration _configuration;
    GTRouteAppSettings settings;
    MockHttpMessageHandler mockHttp;
    TrackDetailService service;
    ITestOutputHelper output;

    public TrackDetailServiceTests(ITestOutputHelper output)
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.test.json")
            .Build();

        settings = new GTRouteAppSettings();
        settings.BaseApi = _configuration.GetSection("AppSettings").GetValue<string>("BaseApi") ?? "";
        settings.CloudinaryApi = _configuration.GetSection("AppSettings").GetValue<string>("CloudinaryApi") ?? "";
        settings.ImageKitPublicKey = _configuration.GetSection("AppSettings").GetValue<string>("ImageKitPublicKey") ?? "";
        settings.ImageKitPrivateKey = _configuration.GetSection("AppSettings").GetValue<string>("ImageKitPrivateKey") ?? "";
        settings.ImageKitEndpointUrl = _configuration.GetSection("AppSettings").GetValue<string>("ImageKitEndpointUrl") ?? "";

        mockHttp = new MockHttpMessageHandler();
        
        var client = mockHttp.ToHttpClient();
        client.BaseAddress = new Uri(settings.BaseApi);
        
        service = new TrackDetailService(client, Options.Create(settings));

        this.output = output;
    }

    [Fact]
    public async Task GetBasic_TrackDetailTest()
    {
        var slug = "track-slug";

        mockHttp.When($"{settings.BaseApi}/detail/basic")
                .WithQueryString("slug", slug)
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleBasicTrackDetail);

        var response = await service.GetBasicTrackDetail(slug);

        Assert.NotNull(response);
        Assert.Equal("City Circuit Track Name", response.Name);
    }

    [Fact]
    public async Task GetEmptyBasic_TrackDetailTest()
    {
        var slug = "track-slug";

        mockHttp.When($"{settings.BaseApi}/detail/basic")
                .WithQueryString("slug", slug)
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleEmptyObjectJson);

        var response = await service.GetBasicTrackDetail(slug);
        
        Assert.Equal(ErrorMessage.NoData, service.GetRecentErrorMessage());
    }

    [Fact]
    public async Task ShouldError_GetBasic_TrackDetailTest()
    {
        var slug = "track-slug";

        mockHttp.When($"{settings.BaseApi}/detail/basic")
                .WithQueryString("slug", slug)
                .Respond(HttpStatusCode.InternalServerError, "application/json", SampleJson.SampleBasicTrackDetail);

        var response = await service.GetBasicTrackDetail(slug);

        Assert.Equal(ErrorMessage.LoadDetailFailed, service.GetRecentErrorMessage());
    }

    [Fact]
    public async Task GetFull_TrackDetailTest()
    {
        var slug = "track-slug";

        mockHttp.When($"{settings.BaseApi}/detail")
                .WithQueryString("slug", slug)
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleTrackDetail);
        
        var response = await service.GetTrackDetail(slug);

        Assert.NotNull(response);
        Assert.Equal("City Circuit Track Name", response.Name);
        Assert.True(response.Layouts.Count() > 0);
        Assert.True(response.Images.Count() > 0);
    }

    [Fact]
    public async Task ShouldEmpty_GetFull_TrackDetailTest()
    {
        var slug = "track-slug";

        mockHttp.When($"{settings.BaseApi}/detail")
                .WithQueryString("slug", slug)
                .Respond(HttpStatusCode.OK, "application/json", SampleJson.SampleEmptyObjectJson);
        
        var response = await service.GetTrackDetail(slug);

        Assert.Equal(ErrorMessage.NoData, service.GetRecentErrorMessage());
    }

    [Fact]
    public async Task ShouldError_GetFull_TrackDetailTest()
    {
        var slug = "track-slug";

        mockHttp.When($"{settings.BaseApi}/detail")
                .WithQueryString("slug", slug)
                .Respond(HttpStatusCode.InternalServerError, "application/json", SampleJson.SampleEmptyObjectJson);
        
        var response = await service.GetTrackDetail(slug);

        Assert.Equal(ErrorMessage.LoadDetailFailed, service.GetRecentErrorMessage());
    }
}