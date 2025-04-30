using System;

namespace MarsXApps.Models.Customs
{
    public class SasAddressModel
    {
        public int Id { get; set; }

        public Guid SysCustomerId { get; set; }

        public string? Address { get; set; }

        public int MasterProvincesId { get; set; }

        public int MasterDistrictsId { get; set; }

        public int MasterSubdistrictsId { get; set; }

        public bool? IsMain { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual SysCustomerModel? SysCustomerModel { get; set; }

        public virtual MasterProvinceModel? MasterProvinces { get; set; }

        public virtual MasterDistrictModel? MasterDistricts { get; set; }

        public virtual MasterSubdistrictModel? MasterSubdistricts { get; set; }

    }
}

