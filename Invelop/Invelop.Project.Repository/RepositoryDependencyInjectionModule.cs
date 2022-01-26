using Invelop.Project.Repository.Person;
using Microsoft.Extensions.DependencyInjection;

namespace Invelop.Project.Repository
{
    public static class RepositoryDependencyInjectionModule
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddTransient<IPersonContactsRepository, PersonContactsRepository>();
        }
    }
}
