
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Web.Api.Configuration;
using ToDoList.Web.Api.Contracts.V1.Requests;
using ToDoList.Web.Api.Data;

namespace ToDoList.Web.Api.Service
{
    public class IdentityService : IIdentityService
    {
        private UserManager<IdentityUser> m_UserManager;
        private JwtSettings m_JwtSettings;

        public IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            m_UserManager = userManager;
            m_JwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult<string>> RegisterAsync(RegisterUserRequest newUser)
        {
            IdentityUser user = await m_UserManager.FindByEmailAsync(newUser.Email);
            AuthenticationResult<string> result = new AuthenticationResult<string>();

            // The user not exist
            if (user == null)
            {
                user = new IdentityUser
                {
                    Email = newUser.Email,
                    UserName = newUser.Username
                };

                IdentityResult identityResult = await m_UserManager.CreateAsync(user, newUser.Password);

                if(identityResult.Succeeded)
                {
                    return genarateJwtToken(user);
                }
                else
                {
                    return result = new AuthenticationResult<string>
                    {
                        IsSuccessed = false,
                        Errors = identityResult.Errors.Select(e => e.Description),
                    };
                }             
            }

            return result = new AuthenticationResult<string>
            {
                IsSuccessed = false,
                Errors = new[] { "The user already exist" }
            };
        }

        public async Task<AuthenticationResult<string>> LoginAsync(LoginRequest loginRequest)
        {
            IdentityUser user = await m_UserManager.FindByEmailAsync(loginRequest.Email);
            AuthenticationResult<string> result = new AuthenticationResult<string>();

            if(user == null)
            {
                result.IsSuccessed = false;
                result.Errors = new[] { "Email or password incorrect " };
            }
            else
            {

                bool isCorrect = await m_UserManager.CheckPasswordAsync(user, loginRequest.Password);
                if(!isCorrect)
                {
                    result.IsSuccessed = false;
                    result.Errors = new[] { "Email or password incorrect " };
                }
                else
                {
                    result.IsSuccessed = true;
                    result.Context = user.Id;
                }
            }

            return result;
        }

        private AuthenticationResult<string> genarateJwtToken(IdentityUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(m_JwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("Id", user.Id)
                    }),

                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult<string>
            {
                IsSuccessed = true,
                Context = tokenHandler.WriteToken(token)
            };
        }
    }
}
