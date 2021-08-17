using Chat.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Validations
{
    public class SendFriendRequestValidator : AbstractValidator<SendFriendRequestViewModel>
    {
        public SendFriendRequestValidator()
        {
            RuleFor(x => x.SearchedFriend).NotEmpty();

            RuleFor(x => x.SearchedFriend)
                .Must(x => x.Contains("#")).When(x => !string.IsNullOrEmpty(x.SearchedFriend))
                .WithMessage("Entered value does not contain #.");
        }
    }
}
