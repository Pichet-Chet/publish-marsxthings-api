using System;
namespace MarsXApps.Models.Customs
{
	public class MasterMachineSubGroupModel
	{
		public MasterMachineSubGroupModel()
		{
		}

        public int Id { get; set; }

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public int MasterMachineGroupId { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

        public virtual MasterMachineGroupModel MasterMachineGroupModel { get; set; }
    }
}

