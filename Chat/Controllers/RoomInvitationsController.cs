using Chat.Extensions;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Controllers
{
    public class RoomInvitationsController : Controller
    {
        private readonly IRoomInvitationService _roomInvitationService;

        public RoomInvitationsController(IRoomInvitationService roomInvitationService)
        {
            _roomInvitationService = roomInvitationService;
        }

        [HttpPost]
        public async Task<IActionResult> AcceptRoomInvitation(int roomId)
        {
            await _roomInvitationService.AcceptRoomInvitationAsync(HttpContext.GetUserId(), roomId);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RejectRoomInvitation(int roomId)
        {
            await _roomInvitationService.RejectRoomInvitationAsync(HttpContext.GetUserId(), roomId);

            return RedirectToAction("Index", "Home");
        }
    }
}
