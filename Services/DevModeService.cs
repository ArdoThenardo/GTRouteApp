namespace GTRouteApp.Data;

public class DevModeService: BaseService
{
    private string GetValidationUrl = "sample-data/passcode.json";

    public DevModeService(HttpClient http): base(http) { }

    public async Task<bool> ValidatePassCode(string code)
    {
        var trueCode = await HitRequest<BaseModel<string>>(GetValidationUrl);

        return code == trueCode.Data ? true : false;
    }
}