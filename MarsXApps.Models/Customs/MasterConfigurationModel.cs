using System;
namespace MarsXApps.Models.Customs
{
	public class MasterConfigurationModel
	{
		public MasterConfigurationModel()
		{
		}

        public int? Id { get; set; }

        public string? Category { get; set; } = null!;

        public string? Description { get; set; }

        public string? Key { get; set; } = null!;

        public string? Value { get; set; } = null!;

        public bool? Isactive { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}

