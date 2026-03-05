namespace RustAdminPanel.Services.Steam
{
    public class SteamPlayerSummary
    {
        public string steamid { get; set; }

        public string personaname { get; set; }

        public string avatar { get; set; }
    }

    public class SteamPlayerSummariesResponse
    {
        public List<SteamPlayerSummary> players { get; set; }
    }

    public class SteamPlayerSummariesRawResponse
    {
        public SteamPlayerSummariesResponse response { get; set; }
    }

}
