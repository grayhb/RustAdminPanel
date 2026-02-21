using RustAdminPanel.DAL.Repositories;
using RustAdminPanel.Domain.Entities;

namespace RustAdminPanel.Services.PlayerConnections
{
    public interface IPlayerConnectionsService
    {
        Task AddAsync(PlayerConnectionDto dto);

        Task<List<PlayerConnectionLog>> GetAsync();
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

        public async Task<List<PlayerConnectionLog>> GetAsync()
        {
            return await _playerConnectionLogRepository.GetAllAsync();
        }
    }
}
