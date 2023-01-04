using System.Net.Http.Json;

namespace GTRouteApp.Data;

public class BaseService
{
    protected const string _baseUrl = "https://localhost:7182";

    protected readonly HttpClient _http;

    public BaseService(HttpClient http)
    {
        this._http = http;
    }

    protected async Task<T> HitRequest<T>(string url) where T: new()
    {
        var fetched = await _http.GetFromJsonAsync<T>(url);

        return fetched ?? new();
    }

    protected async Task<HttpResponseMessage> PostRequest(FormUrlEncodedContent data, string url)
    {
        var response = await _http.PostAsync(url, data);
        
        return response;
    }

    protected async Task<HttpResponseMessage> PutRequest(FormUrlEncodedContent data, string url)
    {
        var response = await _http.PutAsync(url, data);
        
        return response;
    }

    protected async Task<HttpResponseMessage> DeleteRequest(string url)
    {
        var response = await _http.DeleteAsync(url);

        return response;
    }
}