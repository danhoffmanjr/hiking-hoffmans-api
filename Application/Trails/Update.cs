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
    public class Update
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public double Length { get; set; }
            public string Difficulty { get; set; }
            public string Type { get; set; }
            public string Traffick { get; set; }
            public string Attractions { get; set; }
            public string Suitabilities { get; set; }
            public TrailheadLocation Trailhead { get; set; }
            public string Image { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Length).NotEmpty();
                RuleFor(x => x.Difficulty).NotEmpty();
                RuleFor(x => x.Type).NotEmpty();
                RuleFor(x => x.Traffick).NotEmpty();
                RuleFor(x => x.Attractions).NotEmpty();
                RuleFor(x => x.Suitabilities).NotEmpty();
                RuleFor(x => x.Trailhead.Id).NotEmpty();
                RuleFor(x => x.Trailhead.Country).NotEmpty();
                RuleFor(x => x.Trailhead.Street).NotEmpty().When(x => x.Trailhead.Country == "United States");
                RuleFor(x => x.Trailhead.City).NotEmpty().When(x => x.Trailhead.Country == "United States");
                RuleFor(x => x.Trailhead.State).NotEmpty().When(x => x.Trailhead.Country == "United States");
                RuleFor(x => x.Trailhead.County).NotEmpty().When(x => x.Trailhead.Country == "United States");
                RuleFor(x => x.Trailhead.PostalCode).NotEmpty().When(x => x.Trailhead.Country == "United States");
                RuleFor(x => x.Trailhead.Latitude).NotEmpty();
                RuleFor(x => x.Trailhead.Longitude).NotEmpty();
                RuleFor(x => x.Trailhead.InternationalLocation.Address).NotEmpty().When(x => x.Trailhead.Country != "United States");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<User> userManager;
            private readonly IUserAccessor userAccessor;
            private readonly ITrailsService trailsService;
            private readonly IRepository<Trail> repository;
            public Handler(UserManager<User> userManager, IUserAccessor userAccessor, ITrailsService trailsService, IRepository<Trail> repository)
            {
                this.repository = repository;
                this.userManager = userManager;
                this.userAccessor = userAccessor;
                this.trailsService = trailsService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUserId = userAccessor.GetCurrentUserId();

                var currentUser = await userManager.FindByIdAsync(currentUserId);
                if (currentUser == null) throw new RestException(HttpStatusCode.BadRequest, new { User = "User not found. Permission Denied." });
                if (await userManager.IsInRoleAsync(currentUser, RoleNames.User))
                {
                    throw new RestException(HttpStatusCode.Forbidden, new { Forbidden = "Insufficient role privileges. Permission Denied." });
                }

                Trail trailToEdit = await trailsService.FindByIdAsync(request.Id);

                trailToEdit.Name = request.Name;
                trailToEdit.Description = request.Description;
                trailToEdit.Length = request.Length;
                trailToEdit.Difficulty = request.Difficulty;
                trailToEdit.Type = request.Type;
                trailToEdit.Traffick = request.Traffick;
                trailToEdit.Attractions = request.Attractions;
                trailToEdit.Suitabilities = request.Suitabilities;
                trailToEdit.Image = request.Image ?? trailToEdit.Image;
                trailToEdit.Trailhead.Country = request.Trailhead.Country;
                trailToEdit.Trailhead.Street = request.Trailhead.Country == "United States" ? request.Trailhead.Street : null;
                trailToEdit.Trailhead.Street2 = request.Trailhead.Country == "United States" ? request.Trailhead.Street2 ?? trailToEdit.Trailhead.Street2 : null;
                trailToEdit.Trailhead.City = request.Trailhead.Country == "United States" ? request.Trailhead.City : null;
                trailToEdit.Trailhead.County = request.Trailhead.Country == "United States" ? request.Trailhead.County : null;
                trailToEdit.Trailhead.State = request.Trailhead.Country == "United States" ? request.Trailhead.State : null;
                trailToEdit.Trailhead.PostalCode = request.Trailhead.Country == "United States" ? request.Trailhead.PostalCode : null;
                trailToEdit.Trailhead.Latitude = request.Trailhead.Latitude;
                trailToEdit.Trailhead.Longitude = request.Trailhead.Longitude;
                trailToEdit.Trailhead.Altitude = request.Trailhead.Altitude ?? trailToEdit.Trailhead.Altitude;
                trailToEdit.Trailhead.InternationalLocation.Address = request.Trailhead.Country != "United States" ? request.Trailhead.InternationalLocation.Address : null;
                trailToEdit.DateModified = DateTimeOffset.UtcNow;
                trailToEdit.UpdatedBy = currentUser.Id;

                var succeeded = await repository.UpdateAsync(trailToEdit);
                if (succeeded) return Unit.Value;

                throw new RestException(HttpStatusCode.BadRequest, new { EditTrail = "Problem updating trail." });
            }
        }
    }
}