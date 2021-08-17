using Chat.Extensions;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.ViewComponents.FriendRequests
{
    public class FriendRequestsViewComponent : ViewComponent
    {
        private readonly IUserFriendService _userFriendService;

        public FriendRequestsViewComponent(IUserFriendService userFriendService)
        {
            _userFriendService = userFriendService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var friendRequests = await _userFriendService.GetUserFriendRequestsAsync(HttpContext.GetUserId());

            return View(friendRequests);
        }
    }
}
