using System;
using Microsoft.AspNetCore.Identity;

namespace rl.zone.IdentityServer.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}