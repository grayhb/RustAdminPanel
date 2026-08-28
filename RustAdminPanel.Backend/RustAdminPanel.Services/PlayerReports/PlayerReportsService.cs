using Microsoft.EntityFrameworkCore;
using RustAdminPanel.DAL.Repositories;
using RustAdminPanel.Domain.Entities;

namespace RustAdminPanel.Services.PlayerReports
{
    public interface IPlayerReportsService
    {
        //Task AddAsync(PlayerReportDto dto);

        Task AddFromServerAsync(ReportDto dto);

        Task<List<ReportRdo>> GetAsync(PlayerReportQuery query);
    }

    public class PlayerReportsService : IPlayerReportsService
    {
        private readonly IEntityRepository<PlayerReport> _playerReportRepository;
        private readonly IEntityRepository<PlayerProfile> _playerProfileRepository;

        public PlayerReportsService(IEntityRepository<PlayerReport> playerReportRepository, IEntityRepository<PlayerProfile> playerProfileRepository)
        {
            _playerReportRepository = playerReportRepository;
            _playerProfileRepository = playerProfileRepository;
        }

        //public async Task AddAsync(PlayerReportDto dto)
        //{
        //    var item = new PlayerReport()
        //    {

        //        PlayerId = dto.PlayerId,
        //        PlayerName = dto.PlayerName,
        //        TargetId = dto.TargetId,
        //        TargetName = dto.TargetName,
        //        Subject = dto.Subject,
        //        Message = dto.Message,
        //        Type = dto.Type,
        //        CreatedAt = DateTime.Now
        //    };

        //    await _playerReportRepository.AddAsync(item);
        //}

        public async Task AddFromServerAsync(ReportDto dto)
        {
            var item = new PlayerReport()
            {
                PlayerId = dto.userid,
                Data = dto.data,
                CreatedAt = DateTime.Now
            };

            await _playerReportRepository.AddAsync(item);
        }

        public async Task<List<ReportRdo>> GetAsync(PlayerReportQuery query)
        {
            var dbQuery = _playerReportRepository.GetQueryable();

            if (!string.IsNullOrEmpty(query.From))
            {
                // больше или равно дате
                if (DateTime.TryParse(query.From, out var from))
                {
                    dbQuery = dbQuery.Where(e => e.CreatedAt >= from);
                }
            }

            if (!string.IsNullOrEmpty(query.To))
            {
                // меньше или равно дате
                if (DateTime.TryParse(query.To, out var to))
                {
                    dbQuery = dbQuery.Where(e => e.CreatedAt <= to);
                }
            }

            var items = await dbQuery
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();

            var result = new List<ReportRdo>();

            // добавить логин
            foreach (var item in items)
            {
                result.Add(new ReportRdo()
                {
                    Data = item.Data,
                    CreatedAt = item.CreatedAt,
                    PlayerId = item.PlayerId,
                    PlayerName = await GetPlayerName(item.PlayerId)
                });
            }

            return result;
        }

        private async Task<string> GetPlayerName(string playerId)
        {
            return await _playerProfileRepository.GetQueryable()
                .Where(e => e.SteamId == playerId)
                .Select(e => e.SteamNames.Count == 0 ? e.PersonaName : e.SteamNames.Last())
                .FirstOrDefaultAsync() ?? "-";
        }
    }
}
