
using System;
namespace MarsXApps.Models.Customs.Request.SasOrder
{
	public class SasOrderUpdateStatusWithBranchCodeModel
	{
		public SasOrderUpdateStatusWithBranchCodeModel()
		{
		}

        public int Id { get; set; }

        public string SasStatusValue { get; set; }

		public string BranchCode { get; set; }

		public string? CaecUserName { get; set; }
        public string? Remark { get; set; }

    }
}

