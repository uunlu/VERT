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
    public class RegisterUser
    {
        public class Request : BaseRequest<Response>
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string PasswordConfirmation { get; set; }
        }

        public class Response : BaseResponse
        {
            public Guid UserId { get; set; }
        }

        public class Validator : BaseValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Email).EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.PasswordConfirmation)
                    .Equal(x => x.Password)
                    .WithMessage("Confirm password does not match password.");
            }
        }

        public class Handler : BaseHandler<Request, Response>
        {
            public override Task<Response> Handle(Request request)
            {
                var user = User.Register(request.Name, request.Email, request.Password);
                Session.Store(user);
                return Task.FromResult(new Response { UserId = user.Id });
            }
        }
    }
}
