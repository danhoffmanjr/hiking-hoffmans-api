using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class hhDbContext : IdentityDbContext<User, Role, string>
    {
        public hhDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Trail> Trails { get; set; }
    }
}