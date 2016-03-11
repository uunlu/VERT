using FluentValidation;
using Marten;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VERT.Services
{
    public abstract class BaseRequest<TResponse> : IRequest<TResponse>
        where TResponse : BaseResponse
    {
    }

    public abstract class BaseResponse
    {
    }

    public class BaseValidator<TRequest> : AbstractValidator<TRequest>
    {
    }

    public abstract class BaseHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : BaseRequest<TResponse>
        where TResponse : BaseResponse
    {
        /// <summary>
        /// Property injected IDocumentSession.
        /// </summary>
        [StructureMap.Attributes.SetterProperty]
        public IDocumentSession Session { get; set; }

        public Guid LoggedInUserId { get; set; } // get from claims

        public abstract Task<TResponse> Handle(TRequest request);
    }
}
