using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    public class Seed
    {
        public static async Task RolesAndUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            // seed application roles
            if (!roleManager.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        Name = RoleNames.Admin,
                        Description = "This role has access to all application features and can access all data without exceptions.",
                    },
                    new Role
                    {
                        Name = RoleNames.User,
                        Description = "This role has readonly access to application features and cannot access pro user or admin features.",
                    },
                    new Role
                    {
                        Name = RoleNames.ProUser,
                        Description = "This role has full access to application features except for admin features.",
                    }
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            // seed default users for development testing
            if (!userManager.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        UserName = "dghoffman@gmail.com",
                        Email = "dghoffman@gmail.com",
                        DisplayName = "Dan",
                        IsActive = true,
                        EmailConfirmed = true
                    },
                    new User
                    {
                        UserName = "testUser@devmail.com",
                        Email = "testUser@devmail.com",
                        DisplayName = "User",
                        IsActive = true,
                        EmailConfirmed = true
                    },
                    new User
                    {
                        UserName = "testUser2@devmail.com",
                        Email = "testUser2@devmail.com",
                        DisplayName = "User 2",
                        IsActive = true,
                        EmailConfirmed = false
                    },
                    new User
                    {
                        UserName = "testProUser@devmail.com",
                        Email = "testProUser@devmail.com",
                        DisplayName = "Pro User",
                        IsActive = false,
                        EmailConfirmed = true
                    }
                };

                var signUpDateClaim = new Claim("SignUpDate", DateTime.UtcNow.ToShortDateString());

                foreach (var user in users)
                {
                    var result = await userManager.CreateAsync(user, "Pa$$w0rd");

                    if (result.Succeeded)
                    {
                        var newUser = await userManager.FindByEmailAsync(user.Email);
                        var userDisplayName = user.DisplayName.ToLower();
                        var addRoleToUserByDisplayName = new Dictionary<string, Action>();

                        addRoleToUserByDisplayName["dan"] = () =>
                        {
                            userManager.AddToRoleAsync(newUser, RoleNames.Admin).Wait();
                        };

                        addRoleToUserByDisplayName["user"] = () =>
                        {
                            userManager.AddToRoleAsync(newUser, RoleNames.User).Wait();
                        };

                        addRoleToUserByDisplayName["user 2"] = () =>
                        {
                            userManager.AddToRoleAsync(newUser, RoleNames.User).Wait();
                        };

                        addRoleToUserByDisplayName["pro user"] = () =>
                        {
                            userManager.AddToRoleAsync(newUser, RoleNames.ProUser).Wait();
                        };

                        addRoleToUserByDisplayName[userDisplayName]?.Invoke();
                        userManager.AddClaimAsync(newUser, signUpDateClaim).Wait();
                    }
                }
            }
        }

        public static async Task Trails(hhDbContext context)
        {
            // seed trails
            if (!context.Trails.Any())
            {
                var trails = new List<Trail>
                {
                    new Trail
                    {
                        Name = "Sample Trail",
                        Description = "Sample trail description text...",
                        Length = 8.4,
                        Difficulty = "Hard",
                        Type = "Loop",
                        Traffick = "Moderate",
                        Attractions = "Waterfall, overlook",
                        Suitabilities = "Dog friendly",
                        Trailhead = new TrailheadLocation
                        {
                            Street = "123 Sample St.",
                            Street2 = null,
                            City = "Chattanooga",
                            County = "Hamilton",
                            State = "TN",
                            PostalCode = "37411"
                        },
                        Image = "assets/sampleTrail.jpg",
                        Photos = null,
                        Events = null
                    }
                };

                await context.Trails.AddRangeAsync(trails);
                await context.SaveChangesAsync();
            }
        }
    }
}