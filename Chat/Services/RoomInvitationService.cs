using Chat.Hubs;
using Chat.Models;
using Chat.Repositories;
using Chat.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services
{
    public class RoomInvitationService : IRoomInvitationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHubContext<InvitationHub> _hubContext;

        public RoomInvitationService(IUserRepository userRepository, IHubContext<InvitationHub> hubContext)
        {
            _userRepository = userRepository;
            _hubContext = hubContext;
        }

        public async Task<List<UserRoomInvitationViewModel>> GetAllUserRoomInvitationAsync(string userId, RequestStatus requestStatus = RequestStatus.All)
        {
            var user = await _userRepository.GetAllRoomsInvitationAsync(userId, requestStatus);

            var userRoomInvitationViewModelList = user.RoomInvitations.Select(x => new UserRoomInvitationViewModel
            {
                RoomId = x.RoomId,
                Name = x.Room.Name
            })
            .ToList();

            return userRoomInvitationViewModelList;
        }

        public async Task InviteToRoomAsync(string invitedUserId, int roomId)
        {
            var user = await _userRepository.GetByIdAsync(invitedUserId);

            user.RoomInvitations.Add(new RoomInvitation
            {
                RoomId = roomId,
                Status = RequestStatus.NewInvitation
            });

            await _userRepository.UpdateAsync(user);

            await _hubContext.Clients.User(invitedUserId).SendAsync("ReceiveRoomInvitation");
        }

        public async Task AcceptRoomInvitationAsync(string userId, int roomId)
        {
            var user = await _userRepository.GetUserRoomInvitationAsync(userId, roomId);

            user.RoomInvitations.FirstOrDefault().Status = RequestStatus.Accepted;

            user.Rooms.Add(new RoomUser
            {
                RoomId = roomId,
                Role = UserRole.Member
            });

            await _userRepository.UpdateAsync(user);
        }

        public async Task RejectRoomInvitationAsync(string userId, int roomId)
        {
            var user = await _userRepository.GetUserRoomInvitationAsync(userId, roomId);

            user.RoomInvitations.FirstOrDefault().Status = RequestStatus.Rejected;

            await _userRepository.UpdateAsync(user);
        }
    }
}
