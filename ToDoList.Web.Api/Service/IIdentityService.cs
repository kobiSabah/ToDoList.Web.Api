using System.Threading.Tasks;
using ToDoList.Web.Api.Contracts.V1.Requests;

namespace ToDoList.Web.Api.Service
{
    public interface IIdentityService
    {
         Task<AuthenticationResult<string>> RegisterAsync(RegisterUserRequest newUser);
         Task<AuthenticationResult<string>> LoginAsync(LoginRequest loginRequest);
    }
}