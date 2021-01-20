using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Trails
{
    public class List
    {
        public class Query : IRequest<List<Trail>> { }

        public class Handler : IRequestHandler<Query, List<Trail>>
        {
            private readonly ITrailsService trailsService;
            public Handler(ITrailsService trailsService)
            {
                this.trailsService = trailsService;

            }
            public async Task<List<Trail>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await trailsService.ListAsync();
            }
        }
    }
}