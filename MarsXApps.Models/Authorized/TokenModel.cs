using System;
namespace MarsXApps.Models.Authorized
{
	public class TokenModel
	{
		public TokenModel()
		{
		}

        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}

