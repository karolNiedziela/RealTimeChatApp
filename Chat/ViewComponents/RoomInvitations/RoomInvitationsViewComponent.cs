using Chat.Extensions;
using Chat.Models;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.ViewComponents.RoomInvitations
{
    public class RoomInvitationsViewComponent : ViewComponent
    {
        private readonly IRoomInvitationService _roomInvitationService;

        public RoomInvitationsViewComponent(IRoomInvitationService roomInvitationService)
        {
            _roomInvitationService = roomInvitationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roomInvitations = await _roomInvitationService.GetAllUserRoomInvitationAsync(HttpContext.GetUserId(), RequestStatus.NewInvitation);

            return View(roomInvitations);
        }
    }
}
