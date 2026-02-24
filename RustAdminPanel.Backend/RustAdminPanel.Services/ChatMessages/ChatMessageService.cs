using Microsoft.EntityFrameworkCore;
using RustAdminPanel.DAL.Repositories;
using RustAdminPanel.Domain.Entities;

namespace RustAdminPanel.Services.ChatMessages
{
    public interface IChatMessageService
    {
        Task AddAsync(ChatMessageDto dto);

        Task<List<ChatMessage>> GetAsync(ChatMessageQuery query);
    }

    public class ChatMessageService : IChatMessageService
    {
        private readonly IEntityRepository<ChatMessage> _chatMessageRepository;

        public ChatMessageService(IEntityRepository<ChatMessage> chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task AddAsync(ChatMessageDto dto)
        {
            await _chatMessageRepository.AddAsync(new ChatMessage()
            {
                MessageDateTime = DateTime.Now,
                SteamId = dto.UserId,
                SteamName = dto.Username,
                Message = dto.Message,
                Time = dto.Time,
                Channel = dto.Channel
            });
        }

        public async Task<List<ChatMessage>> GetAsync(ChatMessageQuery query)
        {
            // todo: обработка запроса
            var dbQuery = _chatMessageRepository.GetQueryable();

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
                    dbQuery = dbQuery.Where(e => e.MessageDateTime >= from);
                }
            }

            if (!string.IsNullOrEmpty(query.To))
            {
                // меньше или равно дате
                if (DateTime.TryParse(query.To, out var to))
                {
                    dbQuery = dbQuery.Where(e => e.MessageDateTime <= to);
                }
            }

            if (!string.IsNullOrEmpty(query.MessageSearch))
            {
                // поиск по тексту сообщения
                dbQuery = dbQuery.Where(e => e.Message.ToLower().Contains(query.MessageSearch.ToLower()));
            }

            return await dbQuery
                .OrderByDescending(e => e.MessageDateTime)
                .ToListAsync();
        }
    }
}
