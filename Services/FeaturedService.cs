using Microsoft.Extensions.Options;
using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Services;

public class FeaturedService: BaseService
{
    // remote: /featured
    // sample: sample-data/featured.json
    private readonly string GetFeaturedTracksUrl;
    // remote: /featured/media
    // sample: sample-data/featured_image.json
    private readonly string GetFeaturedImageUrl;

    private List<FeaturedTrack> featuredTracks = new();
    private string recentError = "";

    public FeaturedService(HttpClient http, IOptions<GTRouteAppSettings> settings): base(http, settings) 
    {   
        this.GetFeaturedTracksUrl = $"{_baseUrl}/featured";
        this.GetFeaturedImageUrl = $"{_baseUrl}/featured/media";
    }

    public async Task<List<FeaturedTrack>> GetFeaturedTracks()
    {
        featuredTracks.Clear();
        recentError = "";

        try
        {
            var fetched = await HitRequest<BaseModel<List<FeaturedTrack>>>(GetFeaturedTracksUrl);

            if (fetched.NumberOfData == 0)
                recentError = ErrorMessage.NoData;
            
            featuredTracks.AddRange(fetched.Data ?? new List<FeaturedTrack>());

            return featuredTracks;
        }
        catch
        {
            recentError = ErrorMessage.LoadDataFailed;

            return new List<FeaturedTrack>();
        }
    }

    public async Task<FeaturedImage> GetFeaturedImage()
    {
        try
        {
            var data = await HitRequest<BaseModel<FeaturedImage>>(GetFeaturedImageUrl);

            return data.Data ?? new FeaturedImage();
        }
        catch
        {
            return new FeaturedImage();
        }
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}