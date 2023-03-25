using Microsoft.Extensions.Options;
using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Services;

public class RaceTrackService: BaseService
{
    // remote: /tracks
    // sample: sample-data/track.json
    private readonly string GetTracksUrl;
    private const int Limit = 8;
    private List<RaceTrack> tracks = new();
    private int numberOfTotalRaceTracks = 0;
    private int currentNumberOfRaceTracks = 0;
    private string recentError = "";

    public RaceTrackService(HttpClient http, IOptions<GTRouteAppSettings> settings): base(http, settings) 
    { 
        this.GetTracksUrl = $"{_baseUrl}/tracks";
    }

    public async Task<List<RaceTrack>> GetTracksByPage(int page)
    {
        tracks.Clear();
        recentError = "";

        try
        {
            var tracksData = await HitRequest<BaseModel<List<RaceTrack>>>
                    ($"{GetTracksUrl}/?page={page}&limit={Limit}");

            if (tracksData.NumberOfData == 0)
            {
                recentError = ErrorMessage.NoTrack;
            }
            tracks.AddRange(tracksData.Data ?? Enumerable.Empty<RaceTrack>().ToList());
            currentNumberOfRaceTracks = currentNumberOfRaceTracks + tracks.Count();
            numberOfTotalRaceTracks = tracksData.NumberOfData;

            return SortTracksByCategory();
        }
        catch
        {
            recentError = ErrorMessage.LoadTracksFailed;

            return Enumerable.Empty<RaceTrack>().ToList();
        }
    }

    // TO-DO: GetTracksByCategory

    public async Task<List<RaceTrack>> GetAllTracks()
    {
        tracks.Clear();
        recentError = "";

        try
        {
            var fetched = await HitRequest<BaseModel<List<RaceTrack>>>(GetTracksUrl);
            
            if (fetched.NumberOfData == 0) {
                recentError = ErrorMessage.NoTrack;
            }
            tracks.AddRange(fetched.Data ?? Enumerable.Empty<RaceTrack>().ToList());

            return SortTracksByCategory();
        }
        catch
        {
            recentError = ErrorMessage.LoadTracksFailed;

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

    public bool IsRaceTracksReachMaximum()
    {
        if (currentNumberOfRaceTracks < numberOfTotalRaceTracks)
        {
            return false;
        }
        
        return true;
    }

    public void ResetRaceTracksList()
    {
        tracks.Clear();
        currentNumberOfRaceTracks = 0;
        numberOfTotalRaceTracks = 0;
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}