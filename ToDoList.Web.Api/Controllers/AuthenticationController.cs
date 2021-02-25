using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Web.Api.Contracts.V1.Requests;
using ToDoList.Web.Api.Service;

namespace ToDoList.Web.Api.Controllers
{
    public class AuthenticationController : Controller
    {
        private IIdentityService m_IdentityService;

        public AuthenticationController(IIdentityService identityService)
        {
            m_IdentityService = identityService;
        }

        [HttpPut(ApiRoutes.Account.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest newUser)
        {
            AuthenticationResult<string> result = await m_IdentityService.RegisterAsync(newUser);

            if(result.IsSuccessed)
            {
                return Ok(result.Context);
            }
            else
            {
                return BadRequest(result.Context);
            }
        }
    }
}
