using Microsoft.EntityFrameworkCore;
using RustAdminPanel.DAL.Repositories;
using RustAdminPanel.Domain.Entities;

namespace RustAdminPanel.Services.PlayerConnections
{
    public interface IPlayerConnectionsService
    {
        Task AddAsync(PlayerConnectionDto dto);

        Task<List<PlayerConnectionLog>> GetAsync(PlayerConnectionQuery query);
    }

    public class PlayerConnectionsService : IPlayerConnectionsService
    {
        private readonly IEntityRepository<PlayerConnectionLog> _playerConnectionLogRepository;

        public PlayerConnectionsService(IEntityRepository<PlayerConnectionLog> playerConnectionLogRepository)
        {
            _playerConnectionLogRepository = playerConnectionLogRepository;
        }

        public async Task AddAsync(PlayerConnectionDto dto)
        {
            await _playerConnectionLogRepository.AddAsync(new PlayerConnectionLog()
            {
                SteamId = dto.SteamId,
                SteamName = dto.SteamName,
                ConnectionIp = dto.ConnectionIp,
                ConnectionTimestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds(),
                ConnectionDateTime = DateTime.Now
            });
        }

        public async Task<List<PlayerConnectionLog>> GetAsync(PlayerConnectionQuery query)
        {
            var dbQuery = _playerConnectionLogRepository.GetQueryable();

            if (!string.IsNullOrEmpty(query.SteamName))
            {
                dbQuery = dbQuery.Where(e => e.SteamName.ToLower() == query.SteamName.ToLower().Trim());
            }

            if (!string.IsNullOrEmpty(query.SteamId))
            {
                dbQuery = dbQuery.Where(e => e.SteamId == query.SteamId.Trim());
            }

            if (!string.IsNullOrEmpty(query.From))
            {
                // больше или равно дате
                if (DateTime.TryParse(query.From, out var from))
                {
                    dbQuery = dbQuery.Where(e => e.ConnectionDateTime >= from);
                }
            }

            if (!string.IsNullOrEmpty(query.To))
            {
                // меньше или равно дате
                if (DateTime.TryParse(query.To, out var to))
                {
                    dbQuery = dbQuery.Where(e => e.ConnectionDateTime <= to);
                }
            }

            return await dbQuery.ToListAsync();
        }
    }
}
