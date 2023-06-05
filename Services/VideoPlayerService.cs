using Microsoft.Extensions.Options;
using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Services;

public class VideoPlayerService: BaseService
{
    // remote: /video/{id}
    private readonly string GetVideoByIdUrl;
    private string recentError = "";

    public VideoPlayerService(HttpClient http, IOptions<GTRouteAppSettings> settings): base(http, settings)
    {
        this.GetVideoByIdUrl = $"{_baseUrl}/video";
    }

    public async Task<TrackVideo> GetVideoDataById(string videoId)
    {
        TrackVideo videoData = new();

        try
        {
            var data = await HitRequest<BaseModel<TrackVideo>>($"{GetVideoByIdUrl}/{videoId}");
            videoData = data.Data ?? new TrackVideo();

            return videoData;
        }
        catch
        {
            recentError = ErrorMessage.LoadVideoFailed;

            return new TrackVideo();
        }
    }

    public async Task<List<TrackVideo>> GetOtherVideosById(string videoId)
    {
        List<TrackVideo> videos = new();

        try
        {
            var data = await HitRequest<BaseModel<List<TrackVideo>>>($"{GetVideoByIdUrl}/{videoId}/other");
            var otherVideos = data.Data ?? Enumerable.Empty<TrackVideo>().ToList();

            return otherVideos;
        }
        catch
        {
            return Enumerable.Empty<TrackVideo>().ToList();
        }
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}