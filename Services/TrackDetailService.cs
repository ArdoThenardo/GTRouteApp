using Microsoft.Extensions.Options;
using GTRouteApp.Models;
using GTRouteApp.Helpers;

namespace GTRouteApp.Services;

public class TrackDetailService: BaseService
{
    // remote: /detail?slug={slug}
    // sample: sample-data/track_detail.json
    private readonly string GetDetailUrl;
    private readonly CloudinaryService _cloudinaryService;
    private TrackDetail detail = new();
    private string recentError = "";

    public TrackDetailService(HttpClient http, IOptions<GTRouteAppSettings> settings): base(http, settings) 
    { 
        this.GetDetailUrl = $"{_baseUrl}/detail";
        this._cloudinaryService = new CloudinaryService(settings);
    }

    public async Task<TrackDetail> GetTrackDetail(string slug)
    {
        detail = new();
        recentError = "";

        try
        {
            var fetched = await HitRequest<BaseModel<TrackDetail>>($"{GetDetailUrl}?slug={slug}");

            if (fetched.NumberOfData == 0 || fetched.Data == null)
                recentError = ErrorMessage.NoData;

            detail = fetched.Data ?? new();
            if (detail.Layouts.Count() > 0)
                GenerateThumbnailsForLayouts(detail.Layouts);
            if (detail.Images.Count() > 0)
                GenerateThumbnailsForImages(detail.Images);

            return detail;
        }
        catch
        {
            recentError = ErrorMessage.LoadDetailFailed;

            return new TrackDetail();
        }
    }

    public void GenerateThumbnailsForImages(List<TrackImage> images)
    {
        for (int i = 0; i < images.Count(); i++)
        {
            if (!string.IsNullOrWhiteSpace(images[i].ImageUrl))
            {
                string imageUrl = images[i].ImageUrl ?? "";
                string urlPartToTrim = "https://res.cloudinary.com/doo5vwi4i/image/upload/";
                string source = "";
                if (imageUrl.StartsWith(urlPartToTrim))
                {
                    source = imageUrl.Remove(0, urlPartToTrim.Length);
                    var transformedUrl = _cloudinaryService.GenerateSmallThumbnailImageUrl(source);
                    images[i].ThumbnailUrl = transformedUrl;
                }
                else
                {
                    images[i].ThumbnailUrl = imageUrl; // revert to original url, if url is not from cloudinary
                }
            }
            else
            {
                images[i].ThumbnailUrl = "";
            }
        }

        detail.Images = images;
    }

    public void GenerateThumbnailsForLayouts(List<TrackLayout> layouts)
    {
        for (int i = 0; i < layouts.Count(); i++)
        {
            if (!string.IsNullOrWhiteSpace(layouts[i].MapUrl))
            {
                string imageUrl = layouts[i].MapUrl ?? "";
                string urlPartToTrim = "https://res.cloudinary.com/doo5vwi4i/image/upload/";
                string source = "";
                if (imageUrl.StartsWith(urlPartToTrim))
                {
                    source = imageUrl.Remove(0, urlPartToTrim.Length);
                    var transformedUrl = _cloudinaryService.GenerateSmallThumbnailImageUrl(source);
                    layouts[i].ThumbnailUrl = transformedUrl;
                }
                else
                {
                    layouts[i].ThumbnailUrl = imageUrl; // revert to original url, if url is not from cloudinary
                }
            }
            else
            {
                layouts[i].ThumbnailUrl = "";
            }
        }

        detail.Layouts = layouts;
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}