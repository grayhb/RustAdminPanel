using Microsoft.EntityFrameworkCore;
using RustAdminPanel.Domain.Entities;

namespace RustAdminPanel.DAL.Context
{
    public class RustAdminPanelContext : DbContext
    {
        public RustAdminPanelContext(DbContextOptions<RustAdminPanelContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<PlayerConnectionLog> PlayerConnectionLogs { get; set; }

    }
}
