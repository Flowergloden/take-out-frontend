using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace take_out_frontend_rider;

public static class Profiles
{
    public static void Init()
    {
        GetStatus();
    }

    public class StatusData
    {
        public string User { get; set; }
        public string Phone { get; set; }
    }

    public static StatusData StatusDataInstance = new();

    public static bool HasOrder = false;

    public static Object OrderNow;

    private const string Server = "http://43.143.235.93:8080";

    private const string StatusDir = "rider/info/1";

    private static readonly HttpClient Client = new()
    {
        BaseAddress = new(Server),
    };

    public static async Task<string> GetClient(string dir)
    {
        var message = await Client.GetAsync(dir);
        message.EnsureSuccessStatusCode();
        var jsonResponse = await message.Content.ReadAsStringAsync();
        Console.Write($"{jsonResponse}\n");
        return jsonResponse;
    }

    public static async void GetStatus()
    {
        var info = await Profiles.GetClient(StatusDir);

        var jsonInfo = JsonDocument.Parse(info);
        var root = jsonInfo.RootElement;
        var dataElement = root.GetProperty("data");
        StatusDataInstance = new()
        {
            User = dataElement.GetProperty("name").GetString(),
            Phone = dataElement.GetProperty("phone").GetString(),
        };
        Console.WriteLine($"inner: {StatusDataInstance.User}");
    }
}