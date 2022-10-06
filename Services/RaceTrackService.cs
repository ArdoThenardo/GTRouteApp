using System.Net.Http.Json;

namespace GTRouteApp.Data;

public class RaceTrackService: BaseService
{
    private List<RaceTrack> tracks = new();

    public RaceTrackService(HttpClient http): base(http) { }

    public async Task<List<RaceTrack>> GetTracks()
    {
        tracks.Clear();

        var fetched = await _http.GetFromJsonAsync<BaseModel<List<RaceTrack>>>($"{_baseUrl}/tracks");
        if (fetched is not null)
        {
            tracks.AddRange(fetched.Data ?? new List<RaceTrack>());
            return tracks;
        }
        else
        {
            return new List<RaceTrack>();
        }
    }
}