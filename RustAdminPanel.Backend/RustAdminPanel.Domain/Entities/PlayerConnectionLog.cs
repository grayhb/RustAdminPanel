using System.ComponentModel.DataAnnotations.Schema;

namespace RustAdminPanel.Domain.Entities
{
    [Table("PlayerConnectionLogs")]
    public class PlayerConnectionLog : BaseEntity
    {
        public string SteamName { get; set; }

        public string SteamId { get; set; }

        public string ConnectionIp { get; set; }

        public long ConnectionTimestamp { get; set; }

        public DateTime ConnectionDateTime { get; set; }
    }
}
