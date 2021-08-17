using Chat.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class User : IdentityUser
    {
        public int InvitationTag { get; set; }

        public List<RoomUser> Rooms { get; set; } = new List<RoomUser>();

        public HashSet<UserFriend> Friends { get; set; } = new HashSet<UserFriend>();

        public List<FriendRequest> FriendsRequests { get; set; } = new List<FriendRequest>();

        public List<RoomInvitation> RoomInvitations { get; set; } = new List<RoomInvitation>();
    }
}
