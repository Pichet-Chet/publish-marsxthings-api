using System;
namespace MarsXApps.Models.Authorized
{
	public class ApplicationTokenReponse
	{
		public ApplicationTokenReponse()
		{

		}

		public string? Uid { get; set; }
        public DateTime? LastRequest { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? AccessTokenExpire { get; set; }
    }
}

