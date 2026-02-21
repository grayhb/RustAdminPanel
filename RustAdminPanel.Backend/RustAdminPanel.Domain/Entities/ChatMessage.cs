using System.ComponentModel.DataAnnotations.Schema;

namespace RustAdminPanel.Domain.Entities
{
    [Table("ChatMessages")]
    public class ChatMessage : BaseEntity
    {
        public long Channel { get; set; }

        public string SteamName { get; set; }

        public string SteamId { get; set; }

        public string Message { get; set; }
        
        public long Time { get; set; }

        public DateTime MessageDateTime { get; set; }
    }
}
