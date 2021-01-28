using System.Threading.Tasks;
using Application.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Unit>> Register(Register.Command command)
        {
            command.Origin = Request.Headers["origin"];
            await Mediator.Send(command);
            return Ok("Signup Successful - please check your email to confirm your email address.");
        }
    }
}