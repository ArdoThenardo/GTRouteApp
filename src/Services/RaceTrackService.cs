using Microsoft.Extensions.Options;
using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Services;

public class RaceTrackService: BaseService
{
    // remote: /tracks
    private readonly string GetTracksUrl;
    // remote: /offroads
    private readonly string GetOffroadsUrl;
    private const int Limit = 8;
    private List<RaceTrack> tracks = new();
    private int numberOfTotalRaceTracks = 0;
    private int currentNumberOfRaceTracks = 0;
    private string raceTracksCategory = BrowseCategory.Categories[0];
    private BrowseSort raceTracksSortOption = BrowseSort.Alphabetical;
    private bool shouldSortByName = true;
    private bool isDescending = false;
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
                    ($"{GetOffroadsUrl}/?page={page}&limit={Limit}&"
                    +$"shouldSortByName={shouldSortByName}&isDescending={isDescending}");
            }
            else if (raceTracksCategory.Equals(BrowseCategory.Categories[0])) // Category: All
            {
                tracksData = await HitRequest<BaseModel<List<RaceTrack>>>
                    ($"{GetTracksUrl}/?page={page}&limit={Limit}&"
                    +$"shouldSortByName={shouldSortByName}&isDescending={isDescending}");
            }
            else // Category: Original Circuits || Real Circuits || City Circuits
            {
                var singularCategoryString = raceTracksCategory.Remove(raceTracksCategory.Length - 1, 1);
                tracksData = await HitRequest<BaseModel<List<RaceTrack>>>
                    ($"{GetTracksUrl}/?page={page}&limit={Limit}&category={singularCategoryString}&"
                    +$"shouldSortByName={shouldSortByName}&isDescending={isDescending}");
            }

            if (tracksData.NumberOfData == 0)
            {
                recentError = ErrorMessage.NoTrack;
            }
            tracks.AddRange(tracksData.Data ?? Enumerable.Empty<RaceTrack>().ToList());
            currentNumberOfRaceTracks = currentNumberOfRaceTracks + tracks.Count();
            numberOfTotalRaceTracks = tracksData.NumberOfData;

            return tracks;
        }
        catch
        {
            recentError = ErrorMessage.LoadTracksFailed;

            return Enumerable.Empty<RaceTrack>().ToList();
        }
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

    public void SetSortOption(BrowseSort sort)
    {
        raceTracksSortOption = sort;

        switch (raceTracksSortOption)
        {
            case BrowseSort.Alphabetical:
                shouldSortByName = true;
                isDescending = false;
                break;
            case BrowseSort.AlphabeticalReverse:
                shouldSortByName = true;
                isDescending = true;
                break;
            case BrowseSort.Standard:
                shouldSortByName = false;
                isDescending = true;
                break;
            case BrowseSort.StandardReverse:
                shouldSortByName = false;
                isDescending = false;
                break;
        }
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