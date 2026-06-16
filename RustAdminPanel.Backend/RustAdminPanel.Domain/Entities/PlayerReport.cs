using System.ComponentModel.DataAnnotations.Schema;

namespace RustAdminPanel.Domain.Entities
{
    [Table("PlayerReports")]
    public class PlayerReport : BaseEntity
    {
        public string Data { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
