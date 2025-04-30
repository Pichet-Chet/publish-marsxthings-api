using System;
using System.ComponentModel.DataAnnotations;

namespace MarsXApps.Models.Authorized
{
    public class Login
    {
        public Login()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        public string Username { get; set; } // User can sign in with username or mobile phone
        public string Password { get; set; }
    }


    public class LoginRespone : UserInfo
    {
        public LoginRespone()
        {
            Username = string.Empty;
            LastLogin = DateTime.Now;
            AccessToken = string.Empty;
            AccessTokenExpire = DateTime.Now;
            ImageBase64 = string.Empty;

        }

        public string Username { get; set; }
        public string? ImageUrl { get; set; }
        public string ImageBase64 { get; set; }
        public DateTime LastLogin { get; set; }
        public string AccessToken { get; set; }
        public DateTime? AccessTokenExpire { get; set; }
    }

    public class LoginWithToken
    {
        [Required]
        public string Uid { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;
    }
}

