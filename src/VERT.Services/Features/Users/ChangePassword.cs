using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marten;
using FluentValidation;
using VERT.Core;
using VERT.Core.Users;

namespace VERT.Services.Features.Users
{
    public class ChangePassword
    {
        public class Request : BaseRequest<Response>
        {
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
            public string NewPasswordConfirmation { get; set; }
        }

        public class Response : BaseResponse
        {
        }

        public class Validator : BaseValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.CurrentPassword).NotEmpty();
                RuleFor(x => x.NewPassword).NotEmpty();
                RuleFor(x => x.NewPasswordConfirmation)
                    .Equal(x => x.NewPassword)
                    .WithMessage("Confirm password does not match password.");
            }
        }

        public class Handler : BaseHandler<Request, Response>
        {
            public override async Task<Response> Handle(Request request)
            {
                var user = await Session.LoadAsync<User>(LoggedInUserId);
                user.ChangePassword(request.CurrentPassword, request.NewPassword);
                Session.Store(user);
                return new Response();
            }
        }
    }
}
