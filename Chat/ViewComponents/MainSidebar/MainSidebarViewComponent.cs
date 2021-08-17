using Chat.Database;
using Chat.Extensions;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.ViewComponents.Sidebar
{
    public class MainSidebarViewComponent : ViewComponent
    {
        private readonly IRoomService _roomService;

        public MainSidebarViewComponent(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.GetUserId();
            var rooms = await _roomService.GetAllUserRoomsAsync(userId);

            return View(rooms);
        }
    }
}
