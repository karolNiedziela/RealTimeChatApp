using System.Threading.Tasks;
using Chat.Extensions;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.ViewComponents.UserSidebar
{
    public class UserSidebarViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public UserSidebarViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = await _userService.GetUserSidebarDataAsync(HttpContext.GetUserId());

            return View(currentUser);
        }
    }
}