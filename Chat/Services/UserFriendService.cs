using Chat.Extensions;
using Chat.Hubs;
using Chat.Models;
using Chat.Repositories;
using Chat.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services
{
    public class UserFriendService : IUserFriendService
    {
        private readonly IUserRepository _userRepository;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IHubContext<InvitationHub> _hubContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserFriendService(IUserRepository userRepository, IActionContextAccessor actionContextAccessor, 
            IHubContext<InvitationHub> hubContext, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _actionContextAccessor = actionContextAccessor;
            _hubContext = hubContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<FriendRequest>> GetUserFriendRequestsAsync(string userId)
        {
            var user = await _userRepository.GetAllFriendRequestsAsync(userId, RequestStatus.NewInvitation);

            return user.FriendsRequests;
        }

        public async Task<UserFriendsViewModel> GetAllUserFriendsAsync(string userId)
        {
            var userFriends = await _userRepository.GetAllFriendsAsync(userId);

            var userFriendsViewModel = new UserFriendsViewModel
            {
                UserId = userId,
                Username = userFriends.UserName,
                Friends = userFriends.Friends.Select(x => new Friend
                {
                    FriendId = x.FriendId,
                    FriendName = x.Friend.UserName
                }).ToList()
            };

            return userFriendsViewModel;
        }

        public async Task SendFriendRequestAsync(string senderId, string searchedFriend)
        {
            var splittedString = searchedFriend.Split('#');

            var friend = await _userRepository.GetByInvitationTagAsync(splittedString[0], splittedString[1]);

            if (await _userRepository.CheckIfExistsAsync<FriendRequest>(x => x.UserId == friend.Id && x.SenderId == senderId))
            {
                _actionContextAccessor.ActionContext.ModelState.AddModelError("All", "Friend request already sent.");
                return;
            }

            friend.FriendsRequests.Add(new FriendRequest
            {
                UserId = friend.Id,
                SenderId = senderId,
                RequestStatus = RequestStatus.NewInvitation
            });

            await _userRepository.UpdateAsync(friend);

            await _hubContext.Clients.User(friend.Id).SendAsync("ReceiveInvitation");
        }

        public async Task AcceptFriendRequestAsync(string userId, string acceptedUserId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            user.Friends.Add(new UserFriend
            {
                FriendId = acceptedUserId
            });

            await _userRepository.UpdateAsync(user);

            await ChangeFriendRequestStatusAsync(userId, acceptedUserId, RequestStatus.Accepted);

            var secondUser = await _userRepository.GetByIdAsync(acceptedUserId);

            secondUser.Friends.Add(new UserFriend
            { 
                FriendId = userId
            });

            await _userRepository.UpdateAsync(secondUser);

            await _hubContext.Clients.User(acceptedUserId).SendAsync("AcceptedInvitation", _httpContextAccessor.HttpContext.GetUserName());
        }

        public async Task RejectFriendRequestAsync(string userId, string rejectedUserId)
        {
            await ChangeFriendRequestStatusAsync(userId, rejectedUserId, RequestStatus.Rejected);
        }

        public async Task RemoveFromFriendsAsync(string userId, string userIdToRemove)
        {
            var user = await _userRepository.GetAllFriendsAsync(userId);

            user.Friends.Remove(user.Friends.Single(x => x.FriendId == userIdToRemove));

            await _userRepository.UpdateAsync(user);
        }

        private async Task ChangeFriendRequestStatusAsync(string userId, string senderId, RequestStatus requestStatus)
        {
            var user = await _userRepository.GetFriendRequestAsync(userId, senderId);

            user.FriendsRequests.FirstOrDefault().RequestStatus = requestStatus;

            await _userRepository.UpdateAsync(user);
        }
    }
}
