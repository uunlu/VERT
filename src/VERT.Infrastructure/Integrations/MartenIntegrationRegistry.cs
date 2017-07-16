using Marten;
using StructureMap;
using VERT.Services;

namespace VERT.Infrastructure.Integrations
{
    public class MartenIntegrationRegistry : Registry
    {
        public MartenIntegrationRegistry(IBootstrapperConfig config)
        {
            ForSingletonOf<IDocumentStore>().Use(ctx => CreateStore(config));
            For<IDocumentSession>().Use(ctx => CreateSession(ctx));
        }

        private IDocumentStore CreateStore(IBootstrapperConfig config)
        {
            var store = DocumentStore.For(options =>
            {
                options.Connection(config.Database_ConnString);
                options.AutoCreateSchemaObjects = AutoCreate.All;
                options.DdlRules.TableCreation = CreationStyle.DropThenCreate;
                options.Schema.Include<MartenMappings>();
            });

            return store;
        }

        private IDocumentSession CreateSession(IContext ctx)
        {
            var store = ctx.GetInstance<IDocumentStore>();
            return store.OpenSession();
        }
    }
}
