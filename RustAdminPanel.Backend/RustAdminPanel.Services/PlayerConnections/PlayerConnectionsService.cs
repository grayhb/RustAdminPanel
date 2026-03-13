using Microsoft.EntityFrameworkCore;
using RustAdminPanel.DAL.Repositories;
using RustAdminPanel.Domain.Entities;
using RustAdminPanel.Services.Profiles;

namespace RustAdminPanel.Services.PlayerConnections
{
    public interface IPlayerConnectionsService
    {
        Task AddAsync(PlayerConnectionDto dto);

        Task<List<PlayerConnectionLog>> GetAsync(PlayerConnectionQuery query);

        Task CreateProfilesFromLogsAsync();
    }

    public class PlayerConnectionsService : IPlayerConnectionsService
    {
        private readonly IEntityRepository<PlayerConnectionLog> _playerConnectionLogRepository;
        private readonly IProfileService _profileService;

        public PlayerConnectionsService(IEntityRepository<PlayerConnectionLog> playerConnectionLogRepository, IProfileService profileService)
        {
            _playerConnectionLogRepository = playerConnectionLogRepository;
            _profileService = profileService;
        }

        public async Task AddAsync(PlayerConnectionDto dto)
        {
            var item = new PlayerConnectionLog()
            {
                SteamId = dto.SteamId,
                SteamName = dto.SteamName,
                ConnectionIp = dto.ConnectionIp,
                ConnectionTimestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds(),
                ConnectionDateTime = DateTime.Now
            };

            await _playerConnectionLogRepository.AddAsync(item);

            await _profileService.CreateProfileAsync(dto.SteamId, dto.SteamName, item.ConnectionDateTime);
        }

        public async Task CreateProfilesFromLogsAsync()
        {
            var steamIds = await _playerConnectionLogRepository.GetQueryable()
                .Select(e => e.SteamId)
                .Distinct()
                .ToListAsync();

            foreach (var steamId in steamIds)
            {
                var lastConnection = await _playerConnectionLogRepository.GetQueryable()
                    .OrderByDescending(e => e.ConnectionDateTime)
                    .FirstOrDefaultAsync(e => e.SteamId == steamId);

                if (lastConnection == null) continue;

                await _profileService.CreateProfileAsync(lastConnection.SteamId, lastConnection.SteamName, lastConnection.ConnectionDateTime);
            }
        }

        public async Task<List<PlayerConnectionLog>> GetAsync(PlayerConnectionQuery query)
        {
            var dbQuery = _playerConnectionLogRepository.GetQueryable();

            if (!string.IsNullOrEmpty(query.Ip))
            {
                dbQuery = dbQuery.Where(e => e.ConnectionIp == query.Ip);
            }

            if (!string.IsNullOrEmpty(query.SteamName))
            {
                dbQuery = dbQuery.Where(e => e.SteamName.ToLower().IndexOf(query.SteamName.ToLower()) > -1);
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

            return await dbQuery
                .OrderByDescending(e => e.ConnectionTimestamp)
                .ToListAsync();
        }
    }
}
