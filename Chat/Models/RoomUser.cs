using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class RoomUser
    {
        public int RoomId { get; set; }

        public Room Room { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }

        public UserRole Role { get; set; }
    }
}
