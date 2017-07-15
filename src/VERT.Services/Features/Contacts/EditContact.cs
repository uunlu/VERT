using System;
using System.Threading.Tasks;
using FluentValidation;
using Newtonsoft.Json;
using VERT.Core.Contacts;

namespace VERT.Services.Features.Contacts
{
    public class EditContact
    {
        public class Request : BaseRequest<Response>
        {
            [JsonIgnore]
            public Guid? ContactId { get; set; }

            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }

        public class Response : BaseResponse
        {
            public Guid ContactId { get; set; }
        }

        public class Validator : BaseValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        public class Handler : BaseHandler<Request, Response>
        {
            public override Task<Response> Handle(Request request)
            {
                var contact = new Contact
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone
                };
                Session.Store(contact);
                return Task.FromResult(new Response { ContactId = contact.Id });
            }
        }
    }
}
