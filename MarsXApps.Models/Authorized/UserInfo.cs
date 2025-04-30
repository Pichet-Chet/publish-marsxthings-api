using System;
namespace MarsXApps.Models.Authorized
{
    public class UserInfo
    {
        public UserInfo()
        {
            Username = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            MobilePhone = string.Empty;
        }

        public Guid Uid { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string MobilePhone { get; set; }
    }
}

