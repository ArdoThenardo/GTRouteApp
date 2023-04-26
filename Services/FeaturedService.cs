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
    // sample: sample-data/featured_media.json
    private readonly string GetFeaturedMediaUrl;

    private List<FeaturedTrack> featuredTracks = new();
    private List<FeaturedImage> featuredMedia = new();
    private string recentError = "";

    public FeaturedService(HttpClient http, IOptions<GTRouteAppSettings> settings): base(http, settings) 
    {   
        this.GetFeaturedTracksUrl = $"{_baseUrl}/featured";
        this.GetFeaturedMediaUrl = $"{_baseUrl}/featured/media";
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

    public async Task<List<FeaturedImage>> GetFeaturedMedia(int numberOfMedia)
    {
        featuredMedia.Clear();

        try 
        {
            var data = await HitRequest<BaseModel<List<FeaturedImage>>>(
                $"{GetFeaturedMediaUrl}?numberOfMedia={numberOfMedia}");
            
            featuredMedia.AddRange(data.Data ?? new List<FeaturedImage>());

            return featuredMedia;
        }
        catch
        {
            return Enumerable.Empty<FeaturedImage>().ToList();
        }
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}