using Invelop.Project.Repository;
using Invelop.Project.Serv;

namespace Invelop.Project.Client
{
    public static class WebApplicationExtensions
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            RepositoryDependencyInjectionModule.Initialize(services);
            ServicesDependencyInjectionModule.Initialize(services);
        }
    }
}
