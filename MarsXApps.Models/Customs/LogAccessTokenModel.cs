using System;
namespace MarsXApps.Models.Customs
{
	public class LogAccessTokenModel
	{
		public LogAccessTokenModel()
		{
		}

        public int Id { get; set; }

        public Guid? SysCustomersId { get; set; }

        public string? AccessToken { get; set; }

        public DateTime? RequestDate { get; set; }

        public DateTime? ExpireDate { get; set; }
    }
}

