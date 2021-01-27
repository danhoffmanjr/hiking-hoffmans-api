using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Trails
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class QueryValidator : AbstractValidator<Command>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<User> userManager;
            private readonly IUserAccessor userAccessor;
            private readonly IRepository<Trail> repository;
            public Handler(UserManager<User> userManager, IUserAccessor userAccessor, IRepository<Trail> repository)
            {
                this.repository = repository;
                this.userManager = userManager;
                this.userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // bool isValid = Guid.TryParse(request.Id, out Guid trailId);
                // if (!isValid) throw new RestException(HttpStatusCode.BadRequest, new { Id = "Invalid Trail Id Provided." });

                var currentUserId = userAccessor.GetCurrentUserId();

                var currentUser = await userManager.FindByIdAsync(currentUserId);
                if (currentUser == null) throw new RestException(HttpStatusCode.BadRequest, new { User = "User not found. Permission Denied." });
                if (await userManager.IsInRoleAsync(currentUser, RoleNames.User))
                {
                    throw new RestException(HttpStatusCode.Forbidden, new { Forbidden = "Insufficient role privileges. Permission Denied." });
                }

                var succeeded = await repository.DeleteAsync(request.Id);
                if (succeeded) return Unit.Value;

                throw new RestException(HttpStatusCode.BadRequest, new { EditTrail = "Problem deleting trail." });
            }
        }
    }
}