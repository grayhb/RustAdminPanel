using Microsoft.EntityFrameworkCore;
using RustAdminPanel.DAL.Repositories;
using RustAdminPanel.Domain.Entities;

namespace RustAdminPanel.Services.PlayerReports
{
    public interface IPlayerReportsService
    {
        Task AddAsync(PlayerReportDto dto);

        Task<List<PlayerReport>> GetAsync(PlayerReportQuery query);
    }

    public class PlayerReportsService : IPlayerReportsService
    {
        private readonly IEntityRepository<PlayerReport> _playerReportRepository;

        public PlayerReportsService(IEntityRepository<PlayerReport> playerReportRepository)
        {
            _playerReportRepository = playerReportRepository;
        }

        public async Task AddAsync(PlayerReportDto dto)
        {
            // todo: распарсить объект и сохранить в поля ?

            var item = new PlayerReport()
            {
                Data = dto.data,
                CreatedAt = DateTime.Now
            };

            await _playerReportRepository.AddAsync(item);
        }

        public async Task<List<PlayerReport>> GetAsync(PlayerReportQuery query)
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

            return await dbQuery
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }
    }
}
