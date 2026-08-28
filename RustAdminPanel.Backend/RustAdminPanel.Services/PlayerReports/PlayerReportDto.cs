namespace RustAdminPanel.Services.PlayerReports
{
    public class PlayerReportDto
    {
        public string PlayerId { get; set; }

        public string PlayerName { get; set; }

        public string TargetId { get; set; }

        public string TargetName { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }
    }
}
