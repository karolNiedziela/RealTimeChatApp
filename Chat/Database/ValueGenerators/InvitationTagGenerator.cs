using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Database.ValueGenerators
{
    public class InvitationTagGenerator : ValueGenerator<int>
    {
        public override bool GeneratesTemporaryValues { get; }

        public override int Next(EntityEntry entry)
        {
            ChatDbContext context = (ChatDbContext)entry.Context;

            var lastInvitationTag = context.Users.OrderByDescending(x => x.InvitationTag).Select(x => x.InvitationTag).FirstOrDefault();
            int invitationTag = 1000;
            if (lastInvitationTag != 0)
            {
                invitationTag = lastInvitationTag + 1;
            }

            return invitationTag;
        }
    }
}
