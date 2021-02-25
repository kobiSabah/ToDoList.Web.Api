using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Web.Api.Data;

namespace ToDoList.Web.Api.Configuration.ServicesInstallers
{
    public class DbInstaller : IInstaller
    {
        public void InstallConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
