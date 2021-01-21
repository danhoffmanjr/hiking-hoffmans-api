using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Trails
{
    public class Details
    {
        public class Query : IRequest<Trail>
        {
            public string Id { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, Trail>
        {
            private readonly ITrailsService trailsService;
            public Handler(ITrailsService trailsService)
            {
                this.trailsService = trailsService;

            }
            public async Task<Trail> Handle(Query request, CancellationToken cancellationToken)
            {
                return await trailsService.FindByIdAsync(request.Id);
            }
        }
    }
}