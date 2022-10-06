using System.Net.Http.Json;

namespace GTRouteApp.Data;

public class BaseService
{
    protected const string _baseUrl = "https://localhost:7253"; // choose between -> "https://localhost:7253" or "sample-data/track.json"

    protected readonly HttpClient _http;

    public BaseService(HttpClient http)
    {
        this._http = http;
    }
}