using System;
namespace MarsXApps.Models.Customs
{
	public class MasterMachineModelImageModel
	{
		public MasterMachineModelImageModel()
		{
		}

        public int Id { get; set; }

        public int MasterMachineModelId { get; set; }

        public string Path { get; set; } = null!;

        public string Extension { get; set; } = null!;

        public string Size { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

        public virtual MasterMachineBrandModel MasterMachineBrandModel { get; set; }
    }
}

