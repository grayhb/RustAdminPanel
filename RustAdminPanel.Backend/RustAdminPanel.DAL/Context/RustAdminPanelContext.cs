using Microsoft.EntityFrameworkCore;
using RustAdminPanel.Domain.Entities;

namespace RustAdminPanel.DAL.Context
{
    public class RustAdminPanelContext : DbContext
    {
        public RustAdminPanelContext(DbContextOptions<RustAdminPanelContext> options) : base(options)
        {

        }

        public DbSet<PlayerConnectionLog> PlayerConnectionLogs { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<PlayerProfile> PlayerProfiles { get; set; }

        public DbSet<PlayerReport> PlayerReports { get; set; }

    }
}
