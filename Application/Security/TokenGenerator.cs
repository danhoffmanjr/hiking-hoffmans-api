using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<User> userManager;
        private readonly SymmetricSecurityKey key;
        private readonly DataProtectorTokenProvider<User> tokenProvider;
        public TokenGenerator(UserManager<User> userManager, IConfiguration config, DataProtectorTokenProvider<User> tokenProvider)
        {
            this.tokenProvider = tokenProvider;
            this.userManager = userManager;
            this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:TokenKey"])); // TokenKey stored in appsettings.json for development. Will need to be stored on server in Production.
        }

        public string CreateToken(User user)
        {
            var userRole = userManager.GetRolesAsync(user).Result.FirstOrDefault();

            var tokenClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim("Role", userRole)
            };

            // generate signin credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(tokenClaims),
                Expires = DateTime.UtcNow.AddDays(1), // Change to .AddMinutes(30) for production
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}