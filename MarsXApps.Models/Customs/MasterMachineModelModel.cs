using System;
namespace MarsXApps.Models.Customs
{
    public class MasterMachineModelModel
    {
        public MasterMachineModelModel()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int MasterMachineGroupId { get; set; }

        public int MasterMachineSubGroupId { get; set; }

        public int MasterMachineBrandId { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

        public virtual MasterMachineGroupModel MasterMachineGroupModel { get; set; }

        public virtual MasterMachineSubGroupModel MasterMachineSubGroupModel { get; set; }

        public virtual MasterMachineBrandModel MasterMachineBrandModel { get; set; }

    }
}

