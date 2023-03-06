using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Services;

public class RaceTrackService: BaseService
{
    // remote: /tracks
    // sample: sample-data/track.json
    private const string GetTracksUrl = $"{_baseUrl}/tracks";
    private CloudinaryService _cloudinaryService;
    private const int Limit = 8;
    private List<RaceTrack> tracks = new();
    private int numberOfTotalRaceTracks = 0;
    private int currentNumberOfRaceTracks = 0;
    private string recentError = "";

    public RaceTrackService(HttpClient http): base(http) 
    { 
        _cloudinaryService = new CloudinaryService();
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
            var updatedTracks = new List<RaceTrack>();
            if (tracksData.Data != null)
                updatedTracks = GenerateThumbnailsFromCovers(tracksData.Data);
            tracks.AddRange(updatedTracks ?? Enumerable.Empty<RaceTrack>().ToList());
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

    private List<RaceTrack> GenerateThumbnailsFromCovers(List<RaceTrack> tracks)
    {
        for (int i = 0; i < tracks.Count(); i++)
        {
            if (!string.IsNullOrWhiteSpace(tracks[i].CoverUrl))
            {
                string imageUrl = tracks[i].CoverUrl ?? "";
                string urlPartToTrim = "https://res.cloudinary.com/doo5vwi4i/image/upload/";
                string source = "";
                if (imageUrl.StartsWith(urlPartToTrim))
                {
                    source = imageUrl.Remove(0, urlPartToTrim.Length);
                    var transformedUrl = _cloudinaryService.GenerateSmallThumbnailImageUrl(source);
                    tracks[i].ThumbnailUrl = transformedUrl;
                }
                else
                {
                    tracks[i].ThumbnailUrl = imageUrl; // revert to original url, if url is not from cloudinary
                }
            }
            else
            {
                tracks[i].ThumbnailUrl = "";
            }
        }

        return tracks;
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}