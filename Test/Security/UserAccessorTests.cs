using System;
using System.Collections.Generic;
using System.Security.Claims;
using Application.Interfaces;
using Application.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Test.Shared.Identity;
using Xunit;

namespace Test.Security
{
    public class UserAccessorTests
    {
        public UserAccessorTests() { }

        [Fact]
        public void GetCurrentUserId_AuthenticatedUser_ReturnsUserIdString()
        {
            //Arrang
            var userAccessorMock = MockHelpers.TestUserAccessor();

            //Act
            string userId = userAccessorMock.GetCurrentUserId();

            //Assert
            Assert.IsType<string>(userId);
            Assert.Equal("b43bffdf-93a6-49ce-ae88-12ae68077d36", userId);
        }
    }
}