using System;
namespace MarsXApps.Models.Customs.Request.SasOrder
{
	public class SasOrderUpdateStatusWithCaecJobNoModel
	{
		public SasOrderUpdateStatusWithCaecJobNoModel()
		{
		}

		public int Id { get; set; }

		public string SasStatusValue { get; set; } = null!;

        public string CaecJobNo { get; set; } = null!;

        public string CaecUserName { get; set; } = null!;
        public string? Remark { get; set; }

    }
}

