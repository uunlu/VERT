using FluentValidation;
using MediatR;
using StructureMap;
using VERT.Services;

namespace VERT.Infrastructure.Integrations
{
    public class MediatorRegistry : Registry
    {
        public MediatorRegistry()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.AssemblyContainingType(typeof(BaseResponse));
                scanner.AssemblyContainingType(typeof(IAsyncRequestHandler<,>));
                scanner.AssemblyContainingType(typeof(IValidator<>));

                scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
                scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
                scanner.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
            });

            // Decorate feature handlers with mediator pipeline
            var handlerTypes = For(typeof(IAsyncRequestHandler<,>));
            handlerTypes.DecorateAllWith(typeof(MediatorPipelineHandler<,>));

            // Configure Mediator
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => ctx.GetInstance);
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
            For<IMediator>().Use<Mediator>();
        }
    }
}
