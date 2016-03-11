using FluentValidation;
using FluentValidation.Results;
using Marten;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VERT.Core;

namespace VERT.Infrastructure.Integrations
{
    /// <summary>
    /// Handles domain-agnostic concerns before or after executing feature handlers.
    /// Should be used as decorator over feature handlers.
    /// </summary>
    public class MediatorPipelineHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly IAsyncRequestHandler<TRequest, TResponse> _handler;
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IDocumentSession _session;

        public MediatorPipelineHandler(
            IAsyncRequestHandler<TRequest, TResponse> handler,
            IEnumerable<IValidator<TRequest>> validators,
            IDocumentSession session)
        {
            _handler = handler;
            _validators = validators;
            _session = session;
        }

        public async Task<TResponse> Handle(TRequest request)
        {
            var errors = Validate(request);
            if (errors.Any())
                throw new ValidationException(errors);

            TResponse response;
            try
            {
                response = await _handler.Handle(request);
            }
            catch (CoreException ex)
            {
                _session.Dispose();
                throw new ValidationException(new List<ValidationFailure>
                {
                    new ValidationFailure("CoreException", ex.Message)
                });
            }

            await _session.SaveChangesAsync();

            return response;
        }

        /// <summary>
        /// Validate the request by using requests' validator implementation if present.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private IEnumerable<ValidationFailure> Validate(TRequest request)
        {
            var context = new ValidationContext(request);
            return _validators
                .Select(validator => validator.Validate(context))
                .SelectMany(result => result.Errors);
        }
    }
}
