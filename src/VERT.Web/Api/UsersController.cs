using System;
using System.Threading.Tasks;
using System.Web.Http;
using VERT.Services.Features.Users;

namespace VERT.Web.Api
{
    [RoutePrefix("api/users")]
    public class UsersController : BaseApiController
    {
        [HttpPost]
        [Route("register")]
        public Task<RegisterUser.Response> RegisterUser(RegisterUser.Request request) =>
            Handle(request);

        [HttpPost]
        [Route("password")]
        public Task<ChangePassword.Response> ChangePassword(ChangePassword.Request request) =>
            Handle(request);

        [HttpGet]
        [Route("")]
        public Task<QueryUsers.Response> QueryUsers(QueryUsers.Request request) =>
            Handle(request ?? new QueryUsers.Request());

        [HttpGet]
        [Route("search")]
        public Task<SearchUsers.Response> SearchUsers(SearchUsers.Request request) =>
            Handle(request ?? new SearchUsers.Request());

        [HttpGet]
        [Route("{id}")]
        public Task<QueryUserDetails.Response> QueryUserDetails(Guid id) =>
           Handle(new QueryUserDetails.Request { UserId = id });
    }
}
