using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Web.Api.Service;

namespace ToDoList.Web.Api.Configuration.ServicesInstallers.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallConfiguration(IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<ITaskService, TaskService>();
            service.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
