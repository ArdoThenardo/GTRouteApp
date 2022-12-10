using System.Net.Http.Json;

namespace GTRouteApp.Data;

public class DevModeService: BaseService
{
    // API Url to use:
    // local: $"{_baseUrl}/get-validation-dev"; 
    // sample json: "sample-data/passcode.json";
    private const string GetValidationUrl = $"{_baseUrl}/get-validation-dev";

    // API Url to use:
    // local:  $"{_baseUrl}/countries";
    // sample json: "sample-data/country.json";
    private const string GetCountryListUrl = $"{_baseUrl}/countries";

    // API Url to use:
    // local: $"{_baseUrl}/add-track";
    // sample json: not available;
    private const string PostNewTrackUrl = $"{_baseUrl}/add-track";

    // API Url to use:
    // local: $"{_baseUrl}/edit-track?slug=track-slug";
    // sample json: not available;
    private const string UpdateTrackUrl = $"{_baseUrl}/edit-track";

    // API Url to use:
    // local: $"{_baseUrl}/delete-track?slug=track-slug";
    // sample json: not available;
    private const string DeleteTrackUrl = $"{_baseUrl}/delete-track";

    // API Url to use:
    // local: $"{_baseUrl}/detail/layout?slug=track-slug";
    // sample json: "sample-data/layouts.json";
    private const string GetLayoutsUrl = $"{_baseUrl}/detail/layout";

    // API Url to use:
    // local: $"{_baseUrl}/detail/add-layout?slug=track-slug";
    // sample json: not available;
    private const string AddLayoutUrl = $"{_baseUrl}/detail/add-layout";

    // API Url to use:
    // local: $"{_baseUrl}/detail/delete?slug=track-slug&name=layout-name";
    // sample json: not available;
    private const string DeleteLayoutUrl = $"{_baseUrl}/detail/delete-layout";

    // API Url to use:
    // local: $"{_baseUrl}/detail/all-images?slug=track-slug";
    // sample json: not available;
    private const string GetImagesUrl = $"{_baseUrl}/detail/all-images";

    // API Url to use:
    // remote: $"{_baseUrl}/detail/add-image?slug=track-slug";
    // sample json: not available;
    private const string AddImageUrl = $"{_baseUrl}/detail/add-image";

    // API Url to use:
    // remote: $"{_baseUrl}/detail/delete-image?slug=track-slug&name=img-name";
    // sample json: not available;
    private const string DeleteImageUrl = $"{_baseUrl}/detail/delete-image";

    private List<Country> countries = new();
    private List<TrackLayout> layouts = new();
    private List<TrackImage> images = new();
    private bool isPostSuccess = false;
    private bool isUpdateSuccess = false;
    private string recentError = "";

    public DevModeService(HttpClient http): base(http) { }

    public async Task<bool> ValidatePassCode(string code)
    {
        countries.Clear();
        recentError = "";

        try
        {
            var trueCode = await HitRequest<BaseModel<string>>(GetValidationUrl);

            if (code != trueCode.Data)
            {
                recentError = "Please type a right passcode";
            }

            return code == trueCode.Data ? true : false;
        }
        catch
        {
            recentError = "Unable to validate a passcode. Please try again at later time";

            return false;
        }
    }

    public async Task<List<Country>> GetCountryList()
    {
        recentError = "";

        try
        {
            var fetched = await HitRequest<BaseModel<List<Country>>>(GetCountryListUrl);

            if (fetched.NumberOfData == 0)
            {
                recentError = "There is no country on list";
            }
            countries.AddRange(fetched.Data ?? new List<Country>());

            return countries;
        }
        catch
        {
            recentError = "Unable to load list of country. Please reload this page to try again.";

            return new List<Country>();
        }
    }

    public async Task<string> PostNewTrack(RaceTrackEditModel editModel)
    {
        var encodedContent = GenerateEncodedContent(editModel);

        try
        {
            var response = await PostRequest(encodedContent, PostNewTrackUrl);

            if (response.IsSuccessStatusCode)
            {
                isPostSuccess = true;

                var contentValue = await response.Content.ReadFromJsonAsync<BaseModel<RaceTrack>>();
            
                var msg = $"{contentValue?.Data?.Name} ({contentValue?.Data?.Category}, {contentValue?.Data?.Country?.name}) was succesfully added.";

                return msg;
            }
            else
            {
                isPostSuccess = false;

                var contentValue = await response.Content.ReadFromJsonAsync<BaseModel<string>>();

                var msg = $"There is a error from server. Message: {contentValue?.Data}";

                return msg;
            }
        }
        catch
        {
            isPostSuccess = false;

            var msg = "Unable to add new track. Please try again at later time";

            return msg;
        }
    }

    public async Task<string> UpdateTrack(string slug, RaceTrackEditModel editModel)
    {
        var encodedContent = GenerateEncodedContent(editModel);

        try
        {
            var response = await PutRequest(encodedContent, $"{UpdateTrackUrl}?slug={slug}");

            if (response.IsSuccessStatusCode)
            {
                isUpdateSuccess = true;

                var msg = "Track information was successfully updated.";

                return msg;
            }
            else
            {
                isUpdateSuccess = false;

                var contentValue = await response.Content.ReadFromJsonAsync<BaseModel<string>>();

                var msg = $"There is a error from server. Message: {contentValue?.Data}";

                return msg;
            }
        }
        catch
        {
            isUpdateSuccess = false;

            var msg = "Unable to update track information. Please try again at later time";

            return msg;
        }
    }

    public async Task<string> DeleteTrack(string slug)
    {
        try
        {
            var response = await DeleteRequest($"{DeleteTrackUrl}?slug={slug}");

            if (response.IsSuccessStatusCode)
            {
                var msg = "Track information is successfully deleted.";

                return msg;
            }
            else
            {
                var contentValue = await response.Content.ReadFromJsonAsync<BaseModel<string>>();

                var msg = $"There is a error from server. Message: {contentValue?.Data}";

                return msg;
            }
        }
        catch
        {
            var msg = "Unable to delete track information. Please try again at later time";

            return msg;
        }
    }

    public async Task<List<TrackLayout>> GetLayouts(string slug)
    {
        layouts.Clear();
        recentError = "";

        try
        {
            var fetched = await HitRequest<BaseModel<List<TrackLayout>>>($"{GetLayoutsUrl}?slug={slug}");

            if (fetched.NumberOfData == 0)
            {
                recentError = "There is no layout on list";
            }
            layouts.AddRange(fetched.Data ?? Enumerable.Empty<TrackLayout>().ToList());

            return layouts;
        }
        catch
        {
            recentError = "Unable to load list of layouts. Please reload this page to try again.";

            return Enumerable.Empty<TrackLayout>().ToList();
        }
    }

    public async Task<string> PostNewLayout(LayoutEditModel editModel, string slug)
    {
        var encodedContent = GenerateEncodedContent(editModel);

        try
        {
            var response = await PostRequest(encodedContent, $"{AddLayoutUrl}?slug={slug}");

            if (response.IsSuccessStatusCode)
            {
                isPostSuccess = true;

                var contentValue = await response.Content.ReadFromJsonAsync<BaseModel<TrackLayout>>();
            
                var msg = $"{contentValue?.Data?.Name} (length: {contentValue?.Data?.Length}, corners: {contentValue?.Data?.Corners}) was succesfully added.";

                return msg;
            }
            else
            {
                isPostSuccess = false;

                var contentValue = await response.Content.ReadFromJsonAsync<BaseModel<string>>();

                var msg = $"There is a error from server. Message: {contentValue?.Data}";

                return msg;
            }
        }
        catch
        {
            isPostSuccess = false;

            var msg = "Unable to add new layout. Please try again at later time.";

            return msg;
        }
    }

    public async Task<string> DeleteLayout(string slug, string name)
    {
        try
        {
            var response = await DeleteRequest($"{DeleteLayoutUrl}?slug={slug}&name={name}");

            if (response.IsSuccessStatusCode)
            {
                var msg = "Layout information is successfully deleted.";

                return msg;
            }
            else
            {
                var contentValue = await response.Content.ReadFromJsonAsync<BaseModel<string>>();

                var msg = $"There is a error from server. Message: {contentValue?.Data}";

                return msg;
            }
        }
        catch
        {
            var msg = "Unable to delete layout information. Please try again at later time";

            return msg;
        }
    }

    public async Task<List<TrackImage>> GetImagesBySlug(string slug)
    {
        images.Clear();
        recentError = "";

        try
        {
            var fetched = await HitRequest<BaseModel<List<TrackImage>>>($"{GetImagesUrl}?slug={slug}");

            if (fetched.NumberOfData == 0)
            {
                recentError = "There is no images";
            }
            images.AddRange(fetched.Data ?? Enumerable.Empty<TrackImage>().ToList());

            return images;
        }
        catch
        {
            recentError = "Unable to load images for this track. Please reload this page to try again.";

            return Enumerable.Empty<TrackImage>().ToList();
        }
    }

    public async Task<string> PostNewImage(GalleryEditModel editModel, string slug)
    {
        var encodedContent = GenerateEncodedContent(editModel);

        try
        {
            var response = await PostRequest(encodedContent, $"{AddImageUrl}?slug={slug}");

            if (response.IsSuccessStatusCode)
            {
                isPostSuccess = true;

                var contentValue = await response.Content.ReadFromJsonAsync<BaseModel<TrackImage>>();
            
                var msg = $"{contentValue?.Data?.ImageName} by {contentValue?.Data?.Author} was succesfully added.";

                return msg;
            }
            else
            {
                isPostSuccess = false;

                var contentValue = await response.Content.ReadFromJsonAsync<BaseModel<string>>();

                var msg = $"There is a error from server. Message: {contentValue?.Data}";

                return msg;
            }
        }
        catch
        {
            isPostSuccess = false;

            var msg = "Unable to add new layout. Please try again at later time.";

            return msg;
        }
    }

    public async Task<string> DeleteImage(string slug, string name)
    {
        try
        {
            var response = await DeleteRequest($"{DeleteImageUrl}?slug={slug}&name={name}");

            if (response.IsSuccessStatusCode)
            {
                var msg = "Image information is successfully deleted.";

                return msg;
            }
            else
            {
                var contentValue = await response.Content.ReadFromJsonAsync<BaseModel<string>>();

                var msg = $"There is a error from server. Message: {contentValue?.Data}";

                return msg;
            }
        }
        catch
        {
            var msg = "Unable to delete image information. Please try again at later time";

            return msg;
        }
    }

    private FormUrlEncodedContent GenerateEncodedContent(RaceTrackEditModel editModel)
    {
        var data = new[]
        {
            new KeyValuePair<string, string>("name", editModel.Name ?? ""),
            new KeyValuePair<string, string>("country_code", editModel.CountryCode ?? ""),
            new KeyValuePair<string, string>("logo_url", editModel.LogoUrl ?? ""),
            new KeyValuePair<string, string>("cover_url", editModel.CoverUrl ?? ""),
            new KeyValuePair<string, string>("category", editModel.Category ?? ""),
            new KeyValuePair<string, string>("road_type", editModel.RoadType ?? ""),
            new KeyValuePair<string, string>("num_of_layouts", editModel.NumberOfLayouts.ToString()),
        };

        var encodedContent = new FormUrlEncodedContent(data);

        return encodedContent;
    }

    private FormUrlEncodedContent GenerateEncodedContent(LayoutEditModel editModel)
    {
        var data = new[]
        {
            new KeyValuePair<string, string>("name", editModel.Name ?? ""),
            new KeyValuePair<string, string>("mapUrl", editModel.MapUrl ?? ""),
            new KeyValuePair<string, string>("length", editModel.Length.ToString() ?? ""),
            new KeyValuePair<string, string>("corners", editModel.Corners.ToString() ?? "")
        };

        var encodedContent = new FormUrlEncodedContent(data);

        return encodedContent; 
    }

    private FormUrlEncodedContent GenerateEncodedContent(GalleryEditModel editModel)
    {
        var data = new[]
        {
            new KeyValuePair<string, string>("image_name", editModel.Name ?? ""),
            new KeyValuePair<string, string>("image_url", editModel.ImageUrl ?? ""),
            new KeyValuePair<string, string>("description", editModel.Description ?? ""),
            new KeyValuePair<string, string>("author", editModel.Author ?? "")
        };

        var encodedContent = new FormUrlEncodedContent(data);

        return encodedContent;
    }

    public bool GetPostStatus()
    {
        return isPostSuccess;
    }

    public bool GetUpdateStatus()
    {
        return isUpdateSuccess;
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}