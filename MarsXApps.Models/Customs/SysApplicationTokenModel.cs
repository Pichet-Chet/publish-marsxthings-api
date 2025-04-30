using System;
namespace MarsXApps.Models.Customs
{
	public class SysApplicationTokenModel
	{
		public SysApplicationTokenModel()
		{
		}

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Key { get; set; }

        public string? CompanyName { get; set; }

        public string? ContactName { get; set; }

        public string? ContactTel { get; set; }

        public string? AccessToken { get; set; }

        public DateTime? AccessTokenExpire { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

        public bool IsActive { get; set; }

        public Guid Uid { get; set; }

        public DateTime? LastRequest { get; set; }
    }
}

