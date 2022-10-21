namespace GTRouteApp.Data;

public class RaceTrackService: BaseService
{
    // API Url to use:
    // local: $"{_baseUrl}/tracks";
    // sample json: "sample-data/track.json";
    private const string GetTracksUrl = $"{_baseUrl}/tracks";
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
            tracks.AddRange(fetched.Data ?? new List<RaceTrack>());

            return tracks;
        }
        catch
        {
            recentError = "Unable to get track data from server. Please try again at later time.";

            return new List<RaceTrack>();
        }
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}