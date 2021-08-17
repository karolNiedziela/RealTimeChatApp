using Chat.Database;
using Chat.Extensions;
using Chat.Hubs;
using Chat.Models;
using Chat.Repositories;
using Chat.Validations;
using Chat.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserSidebarViewModel> GetUserSidebarDataAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            var userSidebarViewModel = new UserSidebarViewModel
            {
                Username = user.UserName,
                InvitationTag = user.InvitationTag
            };

            return userSidebarViewModel;
        }
    }
}
