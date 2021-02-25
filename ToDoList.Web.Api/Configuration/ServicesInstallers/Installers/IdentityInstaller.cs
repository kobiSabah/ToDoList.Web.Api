using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using ToDoList.Web.Api.Data;

namespace ToDoList.Web.Api.Configuration.ServicesInstallers
{
    public class IdentityInstaller : IInstaller
    {
        public void InstallConfiguration(IServiceCollection service, IConfiguration configuration)
        {
            //Adds the default identity system configuration for the specified User and Role types
            service.AddIdentity<IdentityUser, IdentityRole>()
                // Add the database representation 
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            JwtSettings jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
            service.AddSingleton(jwtSettings);

            service.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret))
                };
            });

            service.Configure<IdentityOptions>(options =>
                {
                    // Password options
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = false;

                    // Lockout options
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(10);
                    options.Lockout.MaxFailedAccessAttempts = 5;

                    // Username options
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDERGHIJKLMNOPQRSTUVWXYZ1234567890$_#";
                    options.User.RequireUniqueEmail = false;

                });

        }
    }
}
