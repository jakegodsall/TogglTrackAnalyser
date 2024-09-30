using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var client = new HttpClient();
        // Get the username and password from environment variables
        var username = Environment.GetEnvironmentVariable("TOGGL_USERNAME");
        var password = Environment.GetEnvironmentVariable("TOGGL_PASSWORD");
        // Encode the username and password to base64
        var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
        var base64String = Convert.ToBase64String(byteArray);
        // add request headers
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("accept/json"));
        // make the GET request
        var response = await client.GetAsync("https://api.track.toggl.com/api/v9/me/time_entries/current");
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var responseStatus = response.StatusCode;
            Console.WriteLine(responseStatus);
            Console.WriteLine(responseData);
        }
    }
}