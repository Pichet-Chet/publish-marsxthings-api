using System;
namespace MarsXApps.Models.Customs
{
	public class PostingLikeModel
	{
		public PostingLikeModel()
		{
		}

        public int? Id { get; set; }

        public Guid SysCustomersId { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? PostingTransactionId { get; set; }
    }
}

