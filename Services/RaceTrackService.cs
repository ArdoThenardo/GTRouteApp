using Microsoft.Extensions.Options;
using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Services;

public class RaceTrackService: BaseService
{
    // remote: /tracks
    // sample: sample-data/track.json
    private readonly string GetTracksUrl;
    // remote: /offroads
    private readonly string GetOffroadsUrl;
    private const int Limit = 8;
    private List<RaceTrack> tracks = new();
    private int numberOfTotalRaceTracks = 0;
    private int currentNumberOfRaceTracks = 0;
    private string raceTracksCategory = BrowseCategory.Categories[0];
    private string recentError = "";

    public RaceTrackService(HttpClient http, IOptions<GTRouteAppSettings> settings): base(http, settings) 
    { 
        this.GetTracksUrl = $"{_baseUrl}/tracks";
        this.GetOffroadsUrl = $"{_baseUrl}/offroads";
    }

    public async Task<List<RaceTrack>> GetTracks(int page)
    {
        tracks.Clear();
        recentError = "";

        BaseModel<List<RaceTrack>> tracksData = new();

        try
        {
            if (raceTracksCategory.Equals(BrowseCategory.Categories[4])) // Category: Dirt / Snow
            {
                tracksData = await HitRequest<BaseModel<List<RaceTrack>>>
                    ($"{GetOffroadsUrl}/?page={page}&limit={Limit}");
            }
            else if (raceTracksCategory.Equals(BrowseCategory.Categories[0])) // Category: All
            {
                tracksData = await HitRequest<BaseModel<List<RaceTrack>>>
                    ($"{GetTracksUrl}/?page={page}&limit={Limit}");
            }
            else // Category: Original Circuits || Real Circuits || City Circuits
            {
                var singularCategoryString = raceTracksCategory.Remove(raceTracksCategory.Length - 1, 1);
                tracksData = await HitRequest<BaseModel<List<RaceTrack>>>
                    ($"{GetTracksUrl}/?page={page}&limit={Limit}&category={singularCategoryString}");
            }

            if (tracksData.NumberOfData == 0)
            {
                recentError = ErrorMessage.NoTrack;
            }
            tracks.AddRange(tracksData.Data ?? Enumerable.Empty<RaceTrack>().ToList());
            currentNumberOfRaceTracks = currentNumberOfRaceTracks + tracks.Count();
            numberOfTotalRaceTracks = tracksData.NumberOfData;

            return raceTracksCategory.Equals(BrowseCategory.Categories[0]) ? SortTracksByCategory() : tracks;
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

    public void SetRaceTracksCategory(string category)
    {
        raceTracksCategory = category;
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