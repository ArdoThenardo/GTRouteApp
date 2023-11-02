using Microsoft.Extensions.Options;
using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Services;

public class VideoPlayerService: BaseService
{
    // remote: /video/{id}
    private readonly string GetVideoByIdUrl;
    private string recentError = "";
    private string recentErrorForOtherVideos = "";

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

            if (otherVideos.Count() < 1)
                recentErrorForOtherVideos = ErrorMessage.NoRelatedVideos;

            return otherVideos;
        }
        catch
        {
            recentErrorForOtherVideos = ErrorMessage.LoadOtherVideoFailed;

            return Enumerable.Empty<TrackVideo>().ToList();
        }
    }

    public void ResetRecentErrorMessage()
    {
        recentError = "";
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }

    public string GetRecentErrorForOtherVideos()
    {
        return recentErrorForOtherVideos;
    }
}