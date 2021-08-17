using Chat.Extensions;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.ViewComponents.AllFriends
{
    public class AllFriendsViewComponent : ViewComponent
    {
        private readonly IUserFriendService _userFriendService;

        public AllFriendsViewComponent(IUserFriendService userFriendService)
        {
            _userFriendService = userFriendService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userFriends = await _userFriendService.GetAllUserFriendsAsync(HttpContext.GetUserId());

            return View(userFriends);
        }
    }
}
