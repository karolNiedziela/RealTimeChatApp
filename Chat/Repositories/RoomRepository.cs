using Chat.Database;
using Chat.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ChatDbContext _context;

        public RoomRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<Room> GetAsync(int roomId)
        {
            var room = await _context.Rooms.Include(x => x.Messages).SingleOrDefaultAsync(x => x.Id == roomId);

            return room;
        }

        public async Task<IEnumerable<Room>> GetAllUserRoomsAsync(string userId)
        {
            return await _context.Rooms.Where(x => x.Users.Any(x => x.UserId == userId)).ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAllRoomsWithoutUserRoomsAsync(string userId)
        {
            var rooms = await _context.Rooms.Where(x => !x.Users.Any(x => x.UserId == userId)).ToListAsync();

            return rooms;
        }

        public async Task<IEnumerable<Room>> GetRoomsForInvitation(string userId, string invitedUserId)
        {
            var rooms = await _context.Rooms.Where(x => x.Users.Any(x => x.UserId == userId && x.Role == UserRole.Owner) && 
            !x.Users.Any(x => x.UserId == invitedUserId && x.Role != UserRole.Owner)).ToListAsync();

            return rooms;
        }

        public async Task AddAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }
    }
}
