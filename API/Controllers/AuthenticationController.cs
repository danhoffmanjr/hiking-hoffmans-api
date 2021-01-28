using System.Threading.Tasks;
using Application.Authentication;
using Application.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthenticationController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }
    }
}