using System;
using Microsoft.AspNetCore.Identity;

namespace MarsXApps.API.Auth
{
	public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

