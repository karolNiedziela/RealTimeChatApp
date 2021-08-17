namespace Chat.Models
{
    public class FriendRequest
    {
        public string UserId { get; set; }

        public User User { get; set; }
        
        public string SenderId { get; set; }

        public User Sender { get; set; }

        public RequestStatus RequestStatus { get; set; }
    }
}