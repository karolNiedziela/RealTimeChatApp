using Chat.Models;
using Chat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services
{
    public interface IRoomInvitationService
    {
        Task<List<UserRoomInvitationViewModel>> GetAllUserRoomInvitationAsync(string userId, RequestStatus requestStatus = RequestStatus.All);

        Task InviteToRoomAsync(string invitedUserId, int roomId);

        Task AcceptRoomInvitationAsync(string userId, int roomId);

        Task RejectRoomInvitationAsync(string userId, int roomId);
    }
}
