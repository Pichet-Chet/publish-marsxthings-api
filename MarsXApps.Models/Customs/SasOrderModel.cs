using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarsXApps.Models.Customs
{
    public class SasOrderModel
    {
        public SasOrderModel()
        {
            SasOrderTransactionModel = new List<SasOrderTransactionModel>();
        }

        public int Id { get; set; }

        public Guid SysCustomerId { get; set; }

        public string Firstname { get; set; } = null!;

        public string? Lastname { get; set; }

        public string Telephone { get; set; } = null!;

        public string? MachineDetail { get; set; }

        public string ServiceType { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime ServiceDate { get; set; }

        public string? Address { get; set; }

        public int? MasterProvincesId { get; set; }

        public int? MasterDistrictsId { get; set; }

        public int? MasterSubdistrictsId { get; set; }

        public string? PostCode { get; set; }

        public string? ServicePeriodTime { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; } = null!;

        public bool? IsActive { get; set; }

        public int SasStatusId { get; set; }

        public string? SasStatusValue { get; set; }

        public string? CaecJobNo { get; set; }

        public string? CaecBranchCode { get; set; }

        public string? OrderNo { get; set; }

        public string? MachineModel { get; set; }

        public string? MachineSerialNo { get; set; }

        public string? MachineChassic { get; set; }

        public string? Username { get; set; }

        [NotMapped]
        public string? TransRemark { get; set; }


        public SysCustomerModel? SysCustomerModel { get; set; }

        public MasterProvinceModel? MasterProvinceModel { get; set; }

        public MasterDistrictModel? MasterDistrictModel { get; set; }

        public MasterSubdistrictModel? MasterSubdistrictModel { get; set; }

        public SasStatusModel? SasStatusModel { get; set; }

        public List<SasOrderTransactionModel>? SasOrderTransactionModel { get; set; }


    }
}

