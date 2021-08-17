using Chat.Extensions;
using Chat.Hubs;
using Chat.Services;
using Chat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IUserFriendService _userFriendService;
        private readonly IRoomService _roomService;

        public FriendsController(IUserFriendService userFriendService, IRoomService roomService)
        {
            _userFriendService = userFriendService;
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserFriends()
        {
            var userFriends = await _userFriendService.GetAllUserFriendsAsync(HttpContext.GetUserId());

            return View(userFriends);
        }

        [HttpGet]
        public IActionResult SendFriendRequest() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(SendFriendRequestViewModel sendFriendRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("SendFriendRequest", sendFriendRequest);
            }

            await _userFriendService.SendFriendRequestAsync(HttpContext.GetUserId(), sendFriendRequest.SearchedFriend);

            if (!ModelState.IsValid)
            {
                return View("SendFriendRequest", sendFriendRequest);
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> AcceptFriendRequest(string userId)
        {
             await _userFriendService.AcceptFriendRequestAsync(HttpContext.GetUserId(), userId);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RejectFriendRequest(string userId)
        {
            await _userFriendService.RejectFriendRequestAsync(HttpContext.GetUserId(), userId);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFriends(string userId) 
        {
            await _userFriendService.RemoveFromFriendsAsync(HttpContext.GetUserId(), userId);

            return RedirectToAction("Index", "Home");
        }
    }
}
