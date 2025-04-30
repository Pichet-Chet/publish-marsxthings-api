using System;
namespace MarsXApps.Models.Customs
{
	public class PostingLimitModel
	{
		public PostingLimitModel()
		{
		}

        public int? Id { get; set; }

        public Guid SysCustomersId { get; set; }

        public int Limit { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}

