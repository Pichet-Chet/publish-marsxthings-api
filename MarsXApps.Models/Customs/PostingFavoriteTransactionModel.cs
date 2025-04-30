using System;
namespace MarsXApps.Models.Customs
{
	public class PostingFavoriteTransactionModel
	{
		public PostingFavoriteTransactionModel()
		{
		}

        public int? Id { get; set; }

        public int PostingTransactionId { get; set; }

        public Guid SysCustomersId { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}

