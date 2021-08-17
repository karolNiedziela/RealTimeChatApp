using Chat.Models;
using Chat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services
{
    public interface IUserService
    {
        Task<UserSidebarViewModel> GetUserSidebarDataAsync(string userId);
    }
}
