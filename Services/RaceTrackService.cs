using System.Net.Http.Json;

namespace GTRouteApp.Data;

public class RaceTrackService: BaseService
{
    private List<RaceTrack> tracks = new();

    public RaceTrackService(HttpClient http): base(http) { }

    public async Task<List<RaceTrack>> GetTracks()
    {
        tracks.Clear();
        
        var fetched = await HitRequest<BaseModel<List<RaceTrack>>>($"{_baseUrl}/tracks"); // or try: "sample-data/track.json"
        tracks.AddRange(fetched.Data ?? new List<RaceTrack>());

        return tracks;
    }
}