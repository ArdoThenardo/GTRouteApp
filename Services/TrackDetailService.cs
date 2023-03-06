using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Services;

public class TrackDetailService: BaseService
{
    // remote: /detail?slug={slug}
    // sample: sample-data/track_detail.json
    private const string GetDetailUrl = $"{_baseUrl}/detail";

    private TrackDetail detail = new();
    private string recentError = "";

    public TrackDetailService(HttpClient http): base(http) { }

    public async Task<TrackDetail> GetTrackDetail(string slug)
    {
        detail = new();
        recentError = "";

        try
        {
            var fetched = await HitRequest<BaseModel<TrackDetail>>($"{GetDetailUrl}?slug={slug}");

            if (fetched.NumberOfData == 0 || fetched.Data == null)
                recentError = ErrorMessage.NoData;

            detail = fetched.Data ?? new();

            return detail;
        }
        catch
        {
            recentError = ErrorMessage.LoadDetailFailed;

            return new TrackDetail();
        }
    }

    public string GetRandomTrackImage(List<TrackImage> images)
    {
        if (images.Count() > 0)
        {
            var random = new Random();
            var randomizedIndex = 0;

            randomizedIndex = random.Next(images.Count());

            var randomUrl = images.ElementAt(randomizedIndex).ImageUrl;
            
            return randomUrl ?? "";
        }
        else
        {
            return "";
        }
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}