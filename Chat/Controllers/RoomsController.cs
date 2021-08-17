using Chat.Extensions;
using Chat.Hubs;
using Chat.Services;
using Chat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Controllers
{
    //[Authorize]
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly IMessageService _messageService;
        private readonly IRoomInvitationService _roomInvitationService;

        public RoomsController(IRoomService roomService, IHubContext<ChatHub> chatHub, IMessageService messageService, 
            IRoomInvitationService roomInvitationService)
        {
            _roomService = roomService;
            _chatHub = chatHub;
            _messageService = messageService;
            _roomInvitationService = roomInvitationService;
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> Index(int roomId)
        {
            var room = await _roomService.GetAsync(roomId);
            
            return View(room);
        }

        public IActionResult CreateRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                await _roomService.CreateRoomAsync(HttpContext.GetUserId(), room.Name);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult JoinRoom(int roomId)
        {
            return RedirectToAction("Rooms", "Index", new { roomId = roomId });
        }


        [HttpPost]
        public async Task<IActionResult> SendMessage(int roomId, string text)
        {
            var message = await _messageService.AddMessage(roomId, text);

            await _chatHub.Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", new
            {
                Text = message.Text,
                SentBy = message.SentBy,
                Timestamp = message.Timestamp.ToString("dd/MM/yyyy HH:mm:ss")
            });

            return Ok();
        }

        public async Task<IActionResult> InviteToRoom(string userId)
        {
            var rooms = await _roomService.GetRoomsForInvitation(HttpContext.GetUserId(), userId);

            return View(rooms);
        } 

        [HttpPost]
        public async Task<IActionResult> InviteToRoom(InviteToRoomViewModel model)
        {
            await _roomInvitationService.InviteToRoomAsync(model.UserId, model.RoomId);

            return RedirectToAction("Index", "Home");
        }
    }
}
