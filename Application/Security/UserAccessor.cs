using System;
using System.Linq;
using System.Security.Claims;
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
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userId = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var errorCode = errors.InvalidUserName(nameof(user)).Code;
            return userId == null ? errorCode : userId;
        }
    }
}