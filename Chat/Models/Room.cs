using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();

        public List<RoomUser> Users { get; set; } = new List<RoomUser>();
    }
}
