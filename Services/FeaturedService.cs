using Microsoft.Extensions.Options;
using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Services;

public class FeaturedService: BaseService
{
    // remote: /featured
    // sample: sample-data/featured.json
    private readonly string GetFeaturedTracksUrl;
    // remote: /featured/media/video
    // sample: sample-data/featured_media_video.json
    private readonly string GetFeaturedMediaVideoUrl;
    // remote: /featured/media/image
    // sample: sample-data/featured_media_image.json
    private readonly string GetFeaturedMediaImageUrl;

    private List<FeaturedTrack> featuredTracks = new();
    private List<FeaturedVideo> featuredMediaVideo = new();
    private List<FeaturedImage> featuredMediaImage = new();
    private string recentError = "";

    public FeaturedService(HttpClient http, IOptions<GTRouteAppSettings> settings): base(http, settings) 
    {   
        this.GetFeaturedTracksUrl = $"{_baseUrl}/featured";
        this.GetFeaturedMediaVideoUrl = $"{_baseUrl}/featured/media/video";
        this.GetFeaturedMediaImageUrl = $"{_baseUrl}/featured/media/image";
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

    public async Task<List<FeaturedVideo>> GetFeaturedMediaVideo(int numberOfMedia)
    {
        featuredMediaVideo.Clear();

        try
        {
            var data = await HitRequest<BaseModel<List<FeaturedVideo>>>(
                $"{GetFeaturedMediaVideoUrl}?numberOfMedia={numberOfMedia}");

            featuredMediaVideo.AddRange(data.Data ?? new List<FeaturedVideo>());

            return featuredMediaVideo;
        }
        catch
        {
            return Enumerable.Empty<FeaturedVideo>().ToList();
        }
    }

    public async Task<List<FeaturedImage>> GetFeaturedMediaImage(int numberOfMedia)
    {
        featuredMediaImage.Clear();

        try 
        {
            var data = await HitRequest<BaseModel<List<FeaturedImage>>>(
                $"{GetFeaturedMediaImageUrl}?numberOfMedia={numberOfMedia}");
            
            featuredMediaImage.AddRange(data.Data ?? new List<FeaturedImage>());

            return featuredMediaImage;
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