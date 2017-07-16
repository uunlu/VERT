using MediatR;
using StructureMap.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using VERT.Services;

namespace VERT.WebApp
{
    [WebApiExceptionFilter]
    public abstract class BaseApiController : ApiController
    {
        [SetterProperty]
        public IMediator Mediator { get; set; }

        protected Task<TResponse> Handle<TResponse>(BaseRequest<TResponse> request)
            where TResponse : BaseResponse
        {
            if (request == null)
                throw new System.ArgumentNullException(nameof(request), "Mediator cannot send NULL requests.");

            return Mediator.Send(request);
        }
    }
}
