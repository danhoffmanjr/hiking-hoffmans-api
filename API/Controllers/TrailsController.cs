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

        [HttpGet("{id}")]
        public async Task<ActionResult<Trail>> GetTrailById(string id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost("create")]
        public async Task<ActionResult<Trail>> Create(Create.Query query)
        {
            return await Mediator.Send(query);
        }
    }
}