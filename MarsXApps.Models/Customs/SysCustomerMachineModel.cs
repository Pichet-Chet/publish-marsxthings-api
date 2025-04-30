using System;
using System.ComponentModel.DataAnnotations;

namespace MarsXApps.Models.Customs
{
    public class SysCustomerMachineModel
    {
        public SysCustomerMachineModel()
        {
        }

        public int Id { get; set; }

        public Guid SysCustomerId { get; set; }

        public string MachineModel { get; set; } = null!;

        public string? MachineSerial { get; set; }

        public string? MachineChassic { get; set; }

        public string? MachineEngine { get; set; }

        public string? MachineDetail { get; set; }

        public string? MachineIcon { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

        public SysCustomerModel? SysCustomerModel { get; set; }

        public string? AliasName { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

    }

    public class SysCustomerMachineModelCreate
    {
        public Guid SysCustomerId { get; set; }

        public string? AliasName { get; set; }

        public string MachineModel { get; set; } = null!;

        public string? MachineSerial { get; set; }

        public string? MachineChassic { get; set; }

        public string? MachineEngine { get; set; }

        public string? MachineDetail { get; set; }

        public string CreatedBy { get; set; } = null!;

        public bool IsActive { get; set; }

    }

    public class SysCustomerMachineModelUpdate
    {

    }

    public class SysCustomerMachineModelVerify
    {
        public string Model { get; set; } = null!;

        public string? SerialNo { get; set; }

        public string? Chassic { get; set; }
    }

    public class SysCustomerMachineModelAll
    {
        public string? Key { get; set; }

        public string? Name { get; set; }
    }

}

