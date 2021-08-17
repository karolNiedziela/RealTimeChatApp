using Chat.Database;
using Chat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Chat.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _context;

        public UserRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(string userId)
            => await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);

        public async Task<User> GetByInvitationTagAsync(string firstPart, string secondPart)
            => await _context.Users.Where(x => x.UserName == firstPart && x.InvitationTag.ToString() == secondPart)
                .FirstOrDefaultAsync();

        public async Task<User> GetAllFriendsAsync(string userId)
        {
            return await _context.Users
                    .Include(x => x.Friends)
                        .ThenInclude(x => x.Friend)
                    .SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetFriendRequestAsync(string userId, string senderId)
        {
            return await _context.Users
                    .Include(x => x.FriendsRequests.Where(x => x.SenderId == senderId))
                    .SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetAllFriendRequestsAsync(string userId, RequestStatus requestStatus = RequestStatus.All)
        {
            if (requestStatus == RequestStatus.All)
            {
                return await _context.Users
                        .Include(x => x.FriendsRequests)
                            .ThenInclude(x => x.Sender)
                        .SingleOrDefaultAsync(x => x.Id == userId);
            }

            return await _context.Users
                    .Include(x => x.FriendsRequests.Where(x => x.RequestStatus == requestStatus))
                        .ThenInclude(x => x.Sender)
                    .SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetUserRoomInvitationAsync(string userId, int roomId)
        {
            return await _context.Users
                .Include(x => x.RoomInvitations.Where(x => x.RoomId == roomId))
                .SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetAllRoomsInvitationAsync(string userId, RequestStatus requestStatus = RequestStatus.All)
        {
            if (requestStatus == RequestStatus.All)
            {
                return await _context.Users
                    .Include(x => x.RoomInvitations)
                        .ThenInclude(x => x.Room)
                    .SingleOrDefaultAsync(x => x.Id == userId);
            }

            return await _context.Users
                    .Include(x => x.RoomInvitations.Where(x => x.Status == requestStatus))
                        .ThenInclude(x => x.Room)
                    .SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfExistsAsync<T>(Expression<Func<T, bool>> checkFunction) where T : class
        {
            return await _context.Set<T>().AnyAsync(predicate: checkFunction);
        }

        public async Task<int> CountAsync<T>(Expression<Func<T, bool>> countFunction) where T : class
        {
            return await _context.Set<T>().CountAsync(countFunction);
        }
    }
}
