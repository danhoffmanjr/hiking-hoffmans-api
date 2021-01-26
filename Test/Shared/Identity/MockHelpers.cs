// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// https://github.com/dotnet/aspnetcore/blob/main/src/Identity/test/Shared/MockHelpers.cs

using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Security;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Test.Shared.Identity
{
    public static class MockHelpers
    {
        public static StringBuilder LogMessage = new StringBuilder();
        public static CancellationToken CancellationToken = new CancellationToken();

        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return mgr;
        }

        public static Mock<RoleManager<TRole>> MockRoleManager<TRole>(IRoleStore<TRole> store = null) where TRole : class
        {
            store = store ?? new Mock<IRoleStore<TRole>>().Object;
            var roles = new List<IRoleValidator<TRole>>();
            roles.Add(new RoleValidator<TRole>());
            return new Mock<RoleManager<TRole>>(store, roles, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null);
        }

        public static IUserAccessor TestUserAccessor()
        {
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var identityErrors = new IdentityErrorDescriber();

            var claimsMock = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, "b43bffdf-93a6-49ce-ae88-12ae68077d36") };
            var identityMock = new ClaimsIdentity(claimsMock, "TestAuthType");
            var claimsPrincipalMock = new ClaimsPrincipal(identityMock);

            httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipalMock);
            var userAccessorMock = new UserAccessor(httpContextAccessorMock.Object, identityErrors);
            return userAccessorMock;
        }

        public static UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null) where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());
            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);
            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
        }

        public static RoleManager<TRole> TestRoleManager<TRole>(IRoleStore<TRole> store = null) where TRole : class
        {
            store = store ?? new Mock<IRoleStore<TRole>>().Object;
            var roles = new List<IRoleValidator<TRole>>();
            roles.Add(new RoleValidator<TRole>());
            return new RoleManager<TRole>(store, roles,
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                null);
        }

        public static readonly User MockProUser = new User
        {
            Id = "b43bffdf-93a6-49ce-ae88-12ae68077d36",
            UserName = "testProUser@devmail.com",
            NormalizedUserName = "testProUser@devmail.com".ToUpper(),
            Email = "testProUser@devmail.com",
            NormalizedEmail = "testProUser@devmail.com".ToUpper(),
            DisplayName = "Test Pro User",
            IsActive = true,
            EmailConfirmed = true
        };
    }
}