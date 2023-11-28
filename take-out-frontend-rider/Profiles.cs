using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace take_out_frontend_rider;

public static class Profiles
{
    public static bool HasOrder = false;

    public static Object OrderNow;

    private const string Server = "http://43.143.235.93:8080";

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
}