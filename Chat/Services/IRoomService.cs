using Chat.Models;
using Chat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services
{
    public interface IRoomService
    {
        Task<Room> GetAsync(int roomId);

        Task<IEnumerable<Room>> GetAllUserRoomsAsync(string userId);

        Task<IEnumerable<Room>> GetAllRoomsWithoutUserRoomsAsync(string userId);

        Task<IEnumerable<InviteToRoomViewModel>> GetRoomsForInvitation(string userId, string invitedUserId);

        Task CreateRoomAsync(string userId, string name);

    }
}
