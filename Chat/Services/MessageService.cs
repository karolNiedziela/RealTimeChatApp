using Chat.Database;
using Chat.Extensions;
using Chat.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services
{
    public class MessageService : IMessageService
    {
        private readonly ChatDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MessageService(ChatDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Message> AddMessage(int roomId, string text)
        {
            var message = new Message
            {
                RoomId = roomId,
                Text = text,
                SentBy = _httpContextAccessor.HttpContext.GetUserName(),
                Timestamp = DateTime.Now
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return message;
        }
    }
}
