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
                recentError = "There is no data";
            
            featured.AddRange(fetched.Data ?? new List<FeaturedTrack>());

            return featured;
        }
        catch
        {
            recentError = "Unable to get data from server. Please try again at later time.";

            return new List<FeaturedTrack>();
        }
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}