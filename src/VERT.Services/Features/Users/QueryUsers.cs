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
    public class QueryUsers
    {
        public class Request : BaseRequest<Response>
        {
        }

        public class Response : BaseResponse
        {
            public IEnumerable<Item> Items { get; set; } = Enumerable.Empty<Item>();

            public class Item
            {
                public Guid UserId { get; set; }
                public string Name { get; set; }
                public string Email { get; set; }
            }
        }

        public class Handler : BaseHandler<Request, Response>
        {
            public override async Task<Response> Handle(Request request)
            {
                var items = await Session
                    .Query<User>()
                    .Select(x => new Response.Item
                    {
                        UserId = x.Id,
                        Name = x.Name,
                        Email = x.Email
                    })
                    .ToListAsync();

                return new Response { Items = items };
            }
        }
    }
}
