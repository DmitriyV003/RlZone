using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rl.zone.IdentityServer.Models;

namespace rl.zone.IdentityServer
{
    public sealed class ApplicationDbContext : IdentityDbContext<AppUser, Role, Guid>
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER4;Database=RlZoneApiIdentity;Trusted_Connection=True;");
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }
    }
}