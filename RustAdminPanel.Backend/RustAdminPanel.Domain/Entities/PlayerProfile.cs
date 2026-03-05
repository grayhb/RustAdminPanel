using System.ComponentModel.DataAnnotations.Schema;

namespace RustAdminPanel.Domain.Entities
{
    [Table("PlayerProfiles")]
    public class PlayerProfile : BaseEntity
    {
        public PlayerProfile()
        {
            SteamNames = new List<string>();
        }

        public string SteamId { get; set; }

        public string Avatar { get; set; }

        public string PersonaName { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<string> SteamNames { get; set; }

        public string Note { get; set; }

        public DateTime LastServerConnectionAt { get; set; }
    }
}
