using Chat.Models;
using Chat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services
{
    public interface IUserFriendService
    {
        Task<IEnumerable<FriendRequest>> GetUserFriendRequestsAsync(string userId);

        Task<UserFriendsViewModel> GetAllUserFriendsAsync(string userId);

        Task AcceptFriendRequestAsync(string userId, string acceptedUserId);

        Task RejectFriendRequestAsync(string userId, string rejectedUserId);

        Task RemoveFromFriendsAsync(string userId, string userIdToRemove);

        Task SendFriendRequestAsync(string senderId, string searchedFriend);
    }
}
