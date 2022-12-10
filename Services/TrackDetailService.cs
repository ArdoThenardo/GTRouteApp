namespace GTRouteApp.Data;

public class TrackDetailService: BaseService
{
    // remote: /detail/{slug}
    // sample: sample-data/track_detail.json
    private const string GetDetailUrl = $"{_baseUrl}/detail/";

    private TrackDetail detail = new();
    private string recentError = "";

    public TrackDetailService(HttpClient http): base(http) { }

    public async Task<TrackDetail> GetTrackDetail(string slug)
    {
        detail = new();
        recentError = "";

        try
        {
            var fetched = await HitRequest<BaseModel<TrackDetail>>($"{GetDetailUrl}{slug}");

            if (fetched.NumberOfData == 0 || fetched.Data == null)
                recentError = "There is no data";

            detail = fetched.Data ?? new();

            return detail;
        }
        catch
        {
            recentError = "Unable to get track detail data froms server. Please try again at later time";

            return new TrackDetail();
        }
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}