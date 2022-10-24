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

    private List<Country> countries = new();
    private bool isPostSuccess = false;
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

    public bool GetPostStatus()
    {
        return isPostSuccess;
    }

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}