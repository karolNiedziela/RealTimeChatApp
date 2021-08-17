using Chat.Extensions;
using Chat.Models;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserFriendService _userFriendService;
        private readonly IRoomInvitationService _roomInvitationService;

        public HomeController(ILogger<HomeController> logger, IUserFriendService userFriendService, IRoomInvitationService roomInvitationService)
        {
            _logger = logger;
            _userFriendService = userFriendService;
            _roomInvitationService = roomInvitationService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.NumberOfFriendRequests =  (await _userFriendService.GetUserFriendRequestsAsync(HttpContext.GetUserId())).Count();
            ViewBag.NumberOfRoomInvitations = (await _roomInvitationService.GetAllUserRoomInvitationAsync(HttpContext.GetUserId(), 
                RequestStatus.NewInvitation)).Count;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var userFriends = await _userFriendService.GetAllUserFriendsAsync(HttpContext.GetUserId());

            return ViewComponent("AllFriends", userFriends);
        }

        [HttpGet]
        public IActionResult Waiting()
        {
            return ViewComponent("FriendRequests");
        }

        [HttpGet]
        public IActionResult Blocked()
        {
            return ViewComponent("BlockedFriends");
        }

        [HttpGet]
        public IActionResult RoomInvitation()
        {
            return ViewComponent("RoomInvitations");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
