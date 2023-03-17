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
                TransformMapLayouts(detail.Layouts);
            if (detail.Images.Count() > 0)
                TransformImages(detail.Images);

            return detail;
        }
        catch
        {
            recentError = ErrorMessage.LoadDetailFailed;

            return new TrackDetail();
        }
    }

    public void TransformImages(List<TrackImage> images)
    {
        for (int i = 0; i < images.Count(); i++)
        {
            var url = images[i].ImageUrl; 
            if (!string.IsNullOrWhiteSpace(url))
            {
                images[i].ThumbnailUrl = GenerateThumbnail(url); 
                images[i].ImageUrl = GenerateProgressive(url);
            }
        }

        detail.Images = images;
    }

    public void TransformMapLayouts(List<TrackLayout> layouts)
    {
        for (int i = 0; i < layouts.Count(); i++)
        {
            if (!string.IsNullOrWhiteSpace(layouts[i].MapUrl))
            {
                var url = layouts[i].MapUrl;
                layouts[i].ThumbnailUrl = GenerateThumbnail(url);
            }
        }

        detail.Layouts = layouts;
    }

    public string GenerateThumbnail(string? url)
    {
        string imageUrl = url ?? "";
        string urlPartToTrim = GeneralConstants.CloudinaryBase;
        string source = "";
        if (imageUrl.StartsWith(urlPartToTrim))
        {
            source = imageUrl.Remove(0, urlPartToTrim.Length);
            var transformedUrl = _cloudinaryService.GenerateSmallThumbnailImageUrl(source);

            return transformedUrl;
        }
        else
        {
            return imageUrl; // revert to original url, if url is not from cloudinary
        }
    }

    public string GenerateProgressive(string? url)
    {
        string imageUrl = url ?? "";
        string urlPartToTrim = GeneralConstants.CloudinaryBase;
        string source = "";
        if (imageUrl.StartsWith(urlPartToTrim))
        {
            source = imageUrl.Remove(0, urlPartToTrim.Length);
            var transformedUrl = _cloudinaryService.GenerateProgressiveImageUrl(source);

            return transformedUrl;
        }
        else
        {
            return imageUrl; // revert to original url, if url is not from cloudinary or others
        }
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}