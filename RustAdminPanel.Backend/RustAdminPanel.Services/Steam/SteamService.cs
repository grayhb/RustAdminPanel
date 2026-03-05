using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text.Json;

namespace RustAdminPanel.Services.Steam
{
    public interface ISteamService
    {
        Task<SteamPlayerSummary?> GetPlayerSummaryAsync(string steamId);
    }

    public class SteamService : ISteamService
    {
        private IConfiguration _configuration;

        public SteamService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<SteamPlayerSummary?> GetPlayerSummaryAsync(string steamId)
        {
            var stringResponse = await GetSteamApiResponse(SteamInterfaces.ISteamUser, SteamMethods.GetPlayerSummaries, SteamVersionApi.v2, $"steamids={steamId}");

            if (stringResponse == null)
                return null;

            try
            {
                var rawResponse = JsonSerializer.Deserialize<SteamPlayerSummariesRawResponse>(stringResponse);

                if (rawResponse == null) return null;

                return rawResponse.response.players.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }

        private async Task<string> GetSteamApiResponse(string steamInterface, string steamMethod, string apiVersion, string queryString)
        {
            var steamApiUrl = _configuration["SteamApiUrl"];

            if (steamApiUrl == null)
                throw new Exception("В appsettings отсутствует параметр SteamApiUrl");

            var steamApiKey = _configuration["SteamApiKey"];

            if (steamApiKey == null)
                throw new Exception("В appsettings отсутствует параметр SteamApiKey");

            var client = new HttpClient();

            // https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=CB196341FF83BA2E69537B02F986D49B&steamids=76561198736558390&format=json

            var url = $"{_configuration["SteamApiUrl"]}/{steamInterface}/{steamMethod}/{apiVersion}/?key={steamApiKey}&format=json&{queryString}";

            return await client.GetStringAsync(url);
        }

        private static class SteamInterfaces
        {
            public static string ISteamUser => "ISteamUser";
        }

        private static class SteamMethods
        {
            public static string GetPlayerSummaries => "GetPlayerSummaries";
        }

        private static class SteamVersionApi
        {
            public static string v1 => "v1";

            public static string v2 => "v2";
        }

    }
}
