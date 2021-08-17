using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class Message
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public string SentBy { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
