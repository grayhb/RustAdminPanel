using Microsoft.EntityFrameworkCore;
using RustAdminPanel.DAL.Repositories;
using RustAdminPanel.Domain.Entities;
using RustAdminPanel.Services.Steam;

namespace RustAdminPanel.Services.Profiles
{
    public interface IProfileService
    {
        Task<List<PlayerProfile>> GetAsync(PlayerProfileQuery playerProfileQuery);

        Task CreateProfileAsync(string steamId, string steamName, DateTime connectedAt);

        Task RefreshSteamDataAsync();

        Task<PlayerProfile> UpdateAsync(ProfileUpdateDto dto);
    }

    public class ProfileService : IProfileService
    {
        private readonly IEntityRepository<PlayerProfile> _profileRepository;
        private readonly ISteamService _steamService;

        public ProfileService(IEntityRepository<PlayerProfile> profileRepository, ISteamService steamService)
        {
            _profileRepository = profileRepository;
            _steamService = steamService;
        }

        public async Task CreateProfileAsync(string steamId, string steamName, DateTime connectedAt)
        {
            var profile = await _profileRepository.GetQueryable().SingleOrDefaultAsync(e => e.SteamId == steamId);

            if (profile == null)
            {
                profile = new PlayerProfile()
                {
                    SteamId = steamId,
                    PersonaName = steamName,
                    SteamNames = new List<string> { steamName },
                    UpdatedAt = DateTime.Now,
                    LastServerConnectionAt = connectedAt,
                    Avatar = "https://avatars.steamstatic.com/fef49e7fa7e1997310d705b2a6158ff8dc1cdfeb.jpg",
                    Note = ""
                };

                var steamProfile = await _steamService.GetPlayerSummaryAsync(steamId);

                if (steamProfile != null)
                {
                    profile.Avatar = steamProfile.avatar;
                }

                await _profileRepository.AddAsync(profile);
            }
            else
            {
                profile.LastServerConnectionAt = connectedAt;

                if (!profile.SteamNames.Contains(steamName))
                {
                    profile.SteamNames.Add(steamName);
                }

                if ($"{profile.UpdatedAt:dd.MM.yyyy}" != $"{DateTime.Now:dd.MM.yyyy}")
                {
                    var steamProfile = await _steamService.GetPlayerSummaryAsync(steamId);

                    if (steamProfile != null)
                    {
                        profile.Avatar = steamProfile.avatar;
                    }
                }

                profile.UpdatedAt = DateTime.Now;

                await _profileRepository.UpdateAsync(profile);
            }
        }

        public async Task<List<PlayerProfile>> GetAsync(PlayerProfileQuery playerProfileQuery)
        {
            var dbQuery = _profileRepository.GetQueryable();

            if (!string.IsNullOrEmpty(playerProfileQuery.SteamName))
            {
                dbQuery = dbQuery.Where(e => e.PersonaName.ToLower().IndexOf(playerProfileQuery.SteamName.ToLower()) > -1);
            }

            if (!string.IsNullOrEmpty(playerProfileQuery.SteamId))
            {
                dbQuery = dbQuery.Where(e => e.SteamId == playerProfileQuery.SteamId.Trim());
            }

            if (!string.IsNullOrEmpty(playerProfileQuery.From))
            {
                // больше или равно дате
                if (DateTime.TryParse(playerProfileQuery.From, out var from))
                {
                    dbQuery = dbQuery.Where(e => e.LastServerConnectionAt >= from);
                }
            }

            if (!string.IsNullOrEmpty(playerProfileQuery.To))
            {
                // меньше или равно дате
                if (DateTime.TryParse(playerProfileQuery.To, out var to))
                {
                    dbQuery = dbQuery.Where(e => e.LastServerConnectionAt <= to);
                }
            }

            return await dbQuery
                .OrderByDescending(e => e.LastServerConnectionAt)
                .ToListAsync();
        }

        public async Task RefreshSteamDataAsync()
        {
            var items = await _profileRepository.GetAllAsync();

            foreach(var item in items)
            {
                var steamProfile = await _steamService.GetPlayerSummaryAsync(item.SteamId);

                if (steamProfile == null) continue;

                item.UpdatedAt = DateTime.Now;
                item.Avatar = steamProfile.avatar;
                item.PersonaName = steamProfile.personaname;

                if (!item.SteamNames.Contains(steamProfile.personaname))
                    item.SteamNames.Add(steamProfile.personaname);
            }

            await _profileRepository.UpdateAsync(items);
        }

        public async Task<PlayerProfile> UpdateAsync(ProfileUpdateDto dto)
        {
            var existItem = await _profileRepository.GetByIdAsync(dto.Id);

            if (existItem == null)
                throw new Exception("Запись не найдена");

            existItem.Note = dto.Note;

            await _profileRepository.UpdateAsync(existItem);

            return existItem;
        }
    }
}
