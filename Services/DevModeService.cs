namespace GTRouteApp.Data;

public class DevModeService: BaseService
{
    private const string GetValidationUrl = $"{_baseUrl}/get-validation-dev"; // or try "sample-data/passcode.json";
    private string recentError = "";

    public DevModeService(HttpClient http): base(http) { }

    public async Task<bool> ValidatePassCode(string code)
    {
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

    public string GetRecentErrorMessage()
    {
        return recentError;
    }
}