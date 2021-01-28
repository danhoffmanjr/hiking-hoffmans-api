using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Application.Users;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication
{
    public class Login
    {
        public class Query : IRequest<UserDto>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, UserDto>
        {
            private readonly UserManager<User> userManager;
            private readonly SignInManager<User> signInManager;
            private readonly ITokenGenerator tokenGenerator;
            public Handler(UserManager<User> userManager, SignInManager<User> signInManager, ITokenGenerator tokenGenerator)
            {
                this.tokenGenerator = tokenGenerator;
                this.signInManager = signInManager;
                this.userManager = userManager;
            }

            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user == null) throw new RestException(HttpStatusCode.Unauthorized, new { error = "Email or password is invalid." });
                if (!user.EmailConfirmed) throw new RestException(HttpStatusCode.BadRequest, new { email = "Email is not confirmed." });

                var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    string role = userManager.GetRolesAsync(user).Result.FirstOrDefault();

                    var jwtToken = tokenGenerator.CreateToken(user);
                    //var refreshToken = await userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultProvider, "RefreshToken");

                    return new UserDto(user, role, jwtToken);
                }

                throw new RestException(HttpStatusCode.Unauthorized, new { error = "Email or password is invalid." });
            }
        }
    }
}