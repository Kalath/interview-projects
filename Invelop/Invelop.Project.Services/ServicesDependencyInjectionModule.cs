using Invelop.Project.Services.Person;
using Microsoft.Extensions.DependencyInjection;

namespace Invelop.Project.Serv
{
    public static class ServicesDependencyInjectionModule
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddTransient<IPersonContactsService, PersonContactsService>();
        }
    }
}
