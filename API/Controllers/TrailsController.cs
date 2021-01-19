using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Trails;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class TrailsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Trail>>> List()
        {
            return await Mediator.Send(new List.Query());
        }
    }
}