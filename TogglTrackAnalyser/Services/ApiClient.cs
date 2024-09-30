using System.Text;

namespace TogglTrackAnalyser.Services;

public class ApiClient
{
    private static readonly HttpClient HttpClient;

    static ApiClient()
    {
        HttpClient = new HttpClient();
    }

    public static async Task<string> GetDataAsync(string endpoint)
    {
        var responseMessage = await HttpClient.GetAsync(endpoint);
        responseMessage.EnsureSuccessStatusCode();
        return await responseMessage.Content.ReadAsStringAsync();
    }

    private static string GetBase64EncodedBasicAuthString(string username, string password)
    {
        var asciiEncodedByteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
        return Convert.ToBase64String(asciiEncodedByteArray);
    }
}