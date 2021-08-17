using Chat.Database.ValueGenerators;
using Chat.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Database
{
    public class ChatDbContext : IdentityDbContext<User>
    {
        public DbSet<Room> Rooms { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<RoomUser> RoomUsers { get; set; }

        public DbSet<UserFriend> UserFriends { get; set; }

        public DbSet<FriendRequest> FriendRequests { get; set; }

        public DbSet<RoomInvitation> RoomInvitations { get; set; }

        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RoomUser>().HasKey(x => new { x.RoomId, x.UserId });

            builder.Entity<UserFriend>().HasKey(x => new { x.UserId, x.FriendId });

            builder.Entity<UserFriend>()
                .HasOne(x => x.User)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().Property(x => x.InvitationTag).HasValueGenerator<InvitationTagGenerator>();

            builder.Entity<FriendRequest>().HasKey(x => new { x.UserId, x.SenderId });
            builder.Entity<FriendRequest>()
                .HasOne(x => x.User)
                .WithMany(x => x.FriendsRequests)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
