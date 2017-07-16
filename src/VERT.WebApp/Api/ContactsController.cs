using System;
using System.Threading.Tasks;
using System.Web.Http;
using VERT.Services.Features.Contacts;

namespace VERT.WebApp.Api
{
    [RoutePrefix("api/contacts")]
    public class ContactsController : BaseApiController
    {
        [HttpPost]
        [Route("")]
        public Task<EditContact.Response> EditContact(EditContact.Request request)
            => Handle(request);

        [HttpGet]
        [Route("")]
        public Task<QueryContacts.Response> QueryContacts(QueryContacts.Request request)
            => Handle(request ?? new QueryContacts.Request());
    }
}
