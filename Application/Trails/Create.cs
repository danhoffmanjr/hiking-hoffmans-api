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
    public class Create
    {
        public class Query : IRequest<Trail>
        {
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
            public string Address
            {
                get
                {
                    if (this.Trailhead.Country != "United States")
                    {
                        return this.Trailhead.InternationalLocation.Address;
                    }

                    return $@"{this.Trailhead.Street} {(String.IsNullOrEmpty(this.Trailhead.Street2) ? String.Empty : this.Trailhead.Street2)} {this.Trailhead.City}, {this.Trailhead.State} {this.Trailhead.PostalCode}";
                }
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Length).NotEmpty();
                RuleFor(x => x.Difficulty).NotEmpty();
                RuleFor(x => x.Type).NotEmpty();
                RuleFor(x => x.Traffick).NotEmpty();
                RuleFor(x => x.Attractions).NotEmpty();
                RuleFor(x => x.Suitabilities).NotEmpty();
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

        public class Handler : IRequestHandler<Query, Trail>
        {
            private readonly UserManager<User> userManager;
            private readonly IUserAccessor userAccessor;
            private readonly ITrailsService trailsService;
            public Handler(UserManager<User> userManager, IUserAccessor userAccessor, ITrailsService trailsService)
            {
                this.userManager = userManager;
                this.userAccessor = userAccessor;
                this.trailsService = trailsService;
            }

            public async Task<Trail> Handle(Query request, CancellationToken cancellationToken)
            {
                var currentUserId = userAccessor.GetCurrentUserId();
                if (currentUserId == "InvalidUserName") throw new RestException(HttpStatusCode.BadRequest, new { User = "Invalid user. Permission Denied." });

                var currentUser = await userManager.FindByIdAsync(currentUserId);
                if (await userManager.IsInRoleAsync(currentUser, RoleNames.User))
                {
                    throw new RestException(HttpStatusCode.Forbidden, new { Forbidden = "Invalid user role. Permission Denied." });
                }

                var trailNameExists = trailsService.CheckExistsByName(request.Name);
                if (trailNameExists) throw new RestException(HttpStatusCode.BadRequest, new { Trail = "Name already exists." });

                var trailAddressExists = trailsService.CheckExistsByAddress(request.Address);
                if (trailAddressExists) throw new RestException(HttpStatusCode.BadRequest, new { Trail = "Trailhead location already exists." });

                var newTrail = new Trail
                {
                    Name = request.Name,
                    Description = request.Description,
                    Length = request.Length,
                    Difficulty = request.Difficulty,
                    Type = request.Type,
                    Traffick = request.Traffick,
                    Attractions = request.Attractions,
                    Suitabilities = request.Suitabilities,
                    Trailhead = new TrailheadLocation
                    {
                        Country = request.Trailhead.Country,
                        County = request.Trailhead.Country == "United States" ? request.Trailhead.County : null,
                        Street = request.Trailhead.Country == "United States" ? request.Trailhead.Street : null,
                        Street2 = request.Trailhead.Country == "United States" ? request.Trailhead.Street2 : null,
                        City = request.Trailhead.Country == "United States" ? request.Trailhead.City : null,
                        State = request.Trailhead.Country == "United States" ? request.Trailhead.State : null,
                        PostalCode = request.Trailhead.Country == "United States" ? request.Trailhead.PostalCode : null,
                        Latitude = request.Trailhead.Latitude,
                        Longitude = request.Trailhead.Longitude,
                        Altitude = request.Trailhead.Altitude,
                        InternationalLocation = request.Trailhead.Country != "United States" ? new InternationalAddress
                        {
                            Address = request.Trailhead.InternationalLocation.Address
                        } : null
                    },
                    CreatedBy = currentUser.Id,
                    DateCreated = DateTime.UtcNow,
                    IsActive = true
                };

                var succeeded = await trailsService.AddAsync(newTrail);
                if (!succeeded) throw new Exception("Error Saving New Trail.");

                var savedTrail = await trailsService.FindByNameAsync(newTrail.Name);
                return savedTrail;
            }
        }
    }
}