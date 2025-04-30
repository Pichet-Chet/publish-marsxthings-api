using System;
namespace MarsXApps.Models.Customs
{
	public class LogAccessApplicationTokenModel
	{
		public LogAccessApplicationTokenModel()
		{
		}

        public int Id { get; set; }

        public int SysApplicationTokenId { get; set; }

        public string AccessToken { get; set; } = null!;

        public DateTime RequestDate { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}

