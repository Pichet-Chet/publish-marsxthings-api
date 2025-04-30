using System;
using System.ComponentModel.DataAnnotations;

namespace MarsXApps.Models.Authorized
{
    public class ChangePassword
    {
        public ChangePassword()
        {
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}

