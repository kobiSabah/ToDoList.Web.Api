using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Web.Api.Configuration.ServicesInstallers
{
    public interface IInstaller
    {
        void InstallConfiguration(IServiceCollection service, IConfiguration configuration);
    }
}
