using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Application.Errors;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IdentityErrorDescriber errors;
        public UserAccessor(IHttpContextAccessor httpContextAccessor, IdentityErrorDescriber errors)
        {
            this.errors = errors;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            var user = httpContextAccessor.HttpContext.User;
            if (user == null) throw new RestException(HttpStatusCode.BadRequest, new { User = "User cannot be null. Permission Denied." });

            var userId = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var errorCode = errors.InvalidUserName(nameof(user)).Code;

            if (userId == null) throw new RestException(HttpStatusCode.BadRequest, new { User = "Unauthenticated user. Permission Denied." });
            if (userId == errorCode) throw new RestException(HttpStatusCode.BadRequest, new { User = "Invalid user. Permission Denied." });

            return userId;
        }
    }
}