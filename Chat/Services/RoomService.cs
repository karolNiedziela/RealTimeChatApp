using Chat.Database;
using Chat.Models;
using Chat.Repositories;
using Chat.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Room> GetAsync(int roomId)
        {
            var room = await _roomRepository.GetAsync(roomId);

            return room;
        }

        public async Task<IEnumerable<Room>> GetAllUserRoomsAsync(string userId)
        {
            var rooms = await _roomRepository.GetAllUserRoomsAsync(userId);

            return rooms;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsWithoutUserRoomsAsync(string userId)
        {
            var rooms = await _roomRepository.GetAllRoomsWithoutUserRoomsAsync(userId);

            return rooms;
        }

        public async Task<IEnumerable<InviteToRoomViewModel>> GetRoomsForInvitation(string userId, string invitedUserId)
        {
            var rooms = await _roomRepository.GetRoomsForInvitation(userId, invitedUserId);

            var inviteToRoomVM = rooms.Select(x => new InviteToRoomViewModel
            {
                RoomId = x.Id,
                Name = x.Name,
                UserId = invitedUserId
            })
            .ToList();

            return inviteToRoomVM;
        }

        public async Task CreateRoomAsync(string userId, string name)
        {
            var room = new Room
            {
                Name = name,
            };

            room.Users.Add(new RoomUser
            {
                UserId = userId,
                Role = UserRole.Owner,
            });

            await _roomRepository.AddAsync(room);
        }
    }
}
