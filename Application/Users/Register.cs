using System;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Validators;
using Data;
using Domain;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;

namespace Application.Users
{
    public class Register
    {
        public class Command : IRequest
        {
            public string DisplayName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public string Origin { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty().Password();
                RuleFor(x => x.ConfirmPassword).NotEmpty().Password().Equal(x => x.Password).WithMessage("Password and Confirmation Password do not match.");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly hhDbContext context;
            private readonly UserManager<User> userManager;
            private readonly IEmailService emailService;
            public Handler(hhDbContext context, UserManager<User> userManager, IEmailService emailService)
            {
                this.emailService = emailService;
                this.userManager = userManager;
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await context.Users.AnyAsync(x => x.NormalizedEmail == request.Email.ToUpper()))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists." });
                }

                if (await context.Users.AnyAsync(x => x.DisplayName.ToLower() == request.DisplayName.ToLower()))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { DisplayName = "Display name already exists." });
                }

                var user = new User
                {
                    UserName = request.Email,
                    Email = request.Email,
                    DisplayName = request.DisplayName
                };

                var result = await userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded) throw new Exception("Error creating User during signup.");

                var newUser = await userManager.FindByEmailAsync(user.Email);

                var registrationDateClaim = new Claim("RegistrationDate", DateTime.UtcNow.ToShortDateString());
                await userManager.AddClaimAsync(user, registrationDateClaim);

                // Add to User role by default
                await userManager.AddToRoleAsync(newUser, RoleNames.User);

                var emailToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                emailToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailToken));

                var verifyUrl = $"{request.Origin}/authentication/verifyEmail?token={emailToken}&email={request.Email}";

                var emailBody = $"<h3>VERIFY EMAIL</h3><p>Thanks for registering with the Hiking Hoffmans app!<p><br />"
                                + $"<p>Please click the below link to verify your email address:</p><p><a href='{verifyUrl}'>{verifyUrl}</a></p>"
                                + $"<p><strong>You will not be able to login to Hiking Hoffmans until your email is verified via the link above.</strong></p>";

                await emailService.SendAsync(request.Email, "Hiking Hoffmans - Email Verification Required", emailBody, true);

                return Unit.Value;
            }
        }
    }
}