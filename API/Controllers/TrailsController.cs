using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Trails;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TrailsController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Trail>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Trail>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost("create")]
        public async Task<ActionResult<Trail>> Create(Create.Query request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("/update/{id}")]
        public async Task<ActionResult<Unit>> Update(Guid id)
        {
            return await Mediator.Send(new Update.Command { Id = id });
        }

        [HttpDelete("/delete/{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }
    }
}