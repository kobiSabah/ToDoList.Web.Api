using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ToDoList.Web.Api.Configuration.ServicesInstallers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallConfiguration(IServiceCollection service, IConfiguration configuration)
        {
            service.AddControllers();

            SwaggerSettings swaggerSettings = new SwaggerSettings();
            configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerSettings);

            OpenApiInfo documentationInfo = new OpenApiInfo
            {
                Title = swaggerSettings.Title,
                Description = swaggerSettings.Description,
                Version = swaggerSettings.Version
            };

            // Using swagger for better testing 
            service.AddSwaggerGen(config =>
            {
                config.SwaggerDoc(documentationInfo.Version, info: documentationInfo);
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });
            });
        }
    }
}
