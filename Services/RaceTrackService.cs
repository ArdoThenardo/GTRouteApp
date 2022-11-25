namespace GTRouteApp.Data;

public class RaceTrackService: BaseService
{
    // API Url to use:
    // local: $"{_baseUrl}/tracks";
    // sample json: "sample-data/track.json";
    private const string GetTracksUrl = $"{_baseUrl}/tracks";

    // API Url to use:
    // local: $"{_baseUrl}/get-track";
    // sample json: not available;
    private const string GetTrackBySlugUrl = $"{_baseUrl}/get-track";

    private List<RaceTrack> tracks = new();
    private string recentError = "";

    public RaceTrackService(HttpClient http): base(http) { }

    public async Task<List<RaceTrack>> GetTracks()
    {
        tracks.Clear();
        recentError = "";

        try
        {
            var fetched = await HitRequest<BaseModel<List<RaceTrack>>>(GetTracksUrl);
            
            if (fetched.NumberOfData == 0) {
                recentError = "There is no track data.";
            }
            tracks.AddRange(fetched.Data ?? Enumerable.Empty<RaceTrack>().ToList());

            return tracks;
        }
        catch
        {
            recentError = "Unable to get track data from server. Please try again at later time.";

            return Enumerable.Empty<RaceTrack>().ToList();
        }
    }

    public async Task<RaceTrack> GetTrackDetail(string slug)
    {
        recentError = "";
        
        try
        {
            var fetched = await HitRequest<BaseModel<RaceTrack>>($"{GetTrackBySlugUrl}?slug={slug}");

            if (fetched.NumberOfData == 0) {
                recentError = "There is no track data.";
            }
            var track = fetched.Data ?? new RaceTrack();

            return track;
        }
        catch
        {
            recentError = "Unable to get track data from server. Please try again at later time.";

            return new RaceTrack();
        }
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}