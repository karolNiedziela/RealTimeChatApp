using Chat.Models;
using Chat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Chat.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string userId);

        Task<User> GetByInvitationTagAsync(string firstPart, string secondPart);

        Task<User> GetAllFriendsAsync(string userId);

        Task<User> GetFriendRequestAsync(string userId, string senderId);

        Task<User> GetAllFriendRequestsAsync(string userId, RequestStatus requestStatus = RequestStatus.All);

        Task<User> GetUserRoomInvitationAsync(string userId, int roomId);

        Task<User> GetAllRoomsInvitationAsync(string userId, RequestStatus requestStatus = RequestStatus.All);

        Task UpdateAsync(User user);

        Task<bool> CheckIfExistsAsync<T>(Expression<Func<T, bool>> checkFunction) where T : class;

        Task<int> CountAsync<T>(Expression<Func<T, bool>> countFunction) where T : class;
    }
}
