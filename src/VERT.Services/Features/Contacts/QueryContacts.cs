using System;
using System.Threading.Tasks;
using FluentValidation;
using Newtonsoft.Json;
using VERT.Core.Contacts;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Marten;

namespace VERT.Services.Features.Contacts
{
    public class QueryContacts
    {
        public class Request : BaseRequest<Response>
        {
        }

        public class Response : BaseResponse
        {
            public IEnumerable<Item> Items { get; set; } = Enumerable.Empty<Item>();

            public class Item
            {
                public Guid ContactId { get; set; }
                public string Name { get; set; }
                public string Email { get; set; }
                public string Phone { get; set; }
            }
        }

        public class Handler : BaseHandler<Request, Response>
        {
            public override async Task<Response> Handle(Request request)
            {
                var items = await Session
                    .Query<Contact>()
                    .Select(x => new Response.Item
                    {
                        ContactId = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        Phone = x.Phone
                    })
                    .ToListAsync();

                return new Response { Items = items };
            }
        }
    }
}
