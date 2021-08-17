using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> GetAsync(int roomId);

        Task<IEnumerable<Room>> GetAllUserRoomsAsync(string userId);

        Task<IEnumerable<Room>> GetAllRoomsWithoutUserRoomsAsync(string userId);

        Task<IEnumerable<Room>> GetRoomsForInvitation(string userId, string invitedUserId);

        Task AddAsync(Room room);
    }
}
