using Microsoft.Extensions.Options;
using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Services;

public class FeaturedService: BaseService
{
    // remote: /featured
    // sample: sample-data/featured.json
    private readonly string GetFeaturedUrl;
    // remote: /featured/media
    // sample: sample-data/featured_image.json
    private readonly string GetFeaturedImageUrl;

    private List<FeaturedTrack> featured = new();
    private string recentError = "";

    public FeaturedService(HttpClient http, IOptions<GTRouteAppSettings> settings): base(http, settings) 
    {   
        this.GetFeaturedUrl = $"{_baseUrl}/featured";
        this.GetFeaturedImageUrl = $"{_baseUrl}/featured/media";
    }

    public async Task<List<FeaturedTrack>> GetFeatured()
    {
        featured.Clear();
        recentError = "";

        try
        {
            var fetched = await HitRequest<BaseModel<List<FeaturedTrack>>>(GetFeaturedUrl);

            if (fetched.NumberOfData == 0)
                recentError = ErrorMessage.NoData;
            
            featured.AddRange(fetched.Data ?? new List<FeaturedTrack>());

            return featured;
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