namespace GTRouteApp.Data;

public class RaceTrackService: BaseService
{
    // remote: /tracks
    // sample: sample-data/track.json
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
            tracks.AddRange(fetched.Data ?? Enumerable.Empty<RaceTrack>().ToList());

            return SortTracksByCategory();
        }
        catch
        {
            recentError = "Unable to get track data from server. Please try again at later time.";

            return Enumerable.Empty<RaceTrack>().ToList();
        }
    }

    private List<RaceTrack> SortTracksByCategory()
    {
        List <RaceTrack> sortedTracks = new();
        var originalTracks = tracks.Where(t => t.Category == RaceTrackCategory.OriginalCircuit).ToList();
        var realTracks = tracks.Where(t => t.Category == RaceTrackCategory.RealCircuit).ToList();
        var cityTracks = tracks.Where(t => t.Category == RaceTrackCategory.CityCircuit).ToList();
        var dirtTracks = tracks.Where(t => t.Category == RaceTrackCategory.DirtCircuit).ToList();
        var snowTracks = tracks.Where(t => t.Category == RaceTrackCategory.SnowCircuit).ToList();

        sortedTracks.AddRange(originalTracks);
        sortedTracks.AddRange(realTracks);
        sortedTracks.AddRange(cityTracks);
        sortedTracks.AddRange(dirtTracks);
        sortedTracks.AddRange(snowTracks);

        return sortedTracks;
    }
    
    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}