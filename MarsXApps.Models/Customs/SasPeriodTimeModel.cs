using System;
namespace MarsXApps.Models.Customs
{
	public class SasPeriodTimeModel
	{
		public SasPeriodTimeModel()
		{
		}

        public int Id { get; set; }

        public string Value { get; set; } = null!;

        public int Seq { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }
    }
}

