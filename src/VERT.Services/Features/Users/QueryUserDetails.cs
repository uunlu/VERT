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
    public class QueryUserDetails
    {
        public class Request : BaseRequest<Response>
        {
            public Guid UserId { get; set; }
        }

        public class Response : BaseResponse
        {
            public Guid UserId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public class Handler : BaseHandler<Request, Response>
        {
            public override async Task<Response> Handle(Request request)
            {
                var user = await Session.LoadAsync<User>(request.UserId);
                return new Response
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Email = user.Email
                };
            }
        }
    }
}
