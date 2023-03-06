using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Data;

public class FeaturedService: BaseService
{
    // remote: /featured
    // sample: sample-data/featured.json
    private const string GetFeaturedUrl = $"{_baseUrl}/featured";

    private List<FeaturedTrack> featured = new();
    private string recentError = "";

    public FeaturedService(HttpClient http): base(http) { }

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

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}