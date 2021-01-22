using System;
using System.Collections.Generic;
using System.Security.Claims;
using Application.Interfaces;
using Application.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Test.Security
{
    public class UserAccessorTests
    {
        private Mock<IHttpContextAccessor> httpContextAccessorMock;
        private IdentityErrorDescriber identityErrors;

        public UserAccessorTests()
        {
            httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            identityErrors = new IdentityErrorDescriber();
        }

        [Fact]
        public void GetCurrentUserId_AuthenticatedUser_ReturnsUserIdString()
        {
            //Arrange
            var claimsMock = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, "user_id_value") };
            var identityMock = new ClaimsIdentity(claimsMock, "TestAuthType");
            var claimsPrincipalMock = new ClaimsPrincipal(identityMock);

            httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipalMock);
            var userAccessorMock = new UserAccessor(httpContextAccessorMock.Object, identityErrors);

            //Act
            string userId = userAccessorMock.GetCurrentUserId();
            Console.WriteLine(userId);

            //Assert
            Assert.IsType<string>(userId);
            Assert.Equal("user_id_value", userId);
        }
    }
}