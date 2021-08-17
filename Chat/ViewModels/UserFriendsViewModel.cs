using Chat.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.ViewModels
{
    public class UserFriendsViewModel
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public List<Friend> Friends { get; set; } = new List<Friend>();

    }

    public class Friend
    {
        [Display(Name = "FriendId")]
        public string FriendId { get; set; }

        public string FriendName { get; set; }
    }
}
