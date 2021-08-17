using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services
{
    public interface IMessageService
    {
        Task<Message> AddMessage(int roomId, string text);
    }
}
