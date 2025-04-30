using System;
using MarsXApps.Models.Extensions;

namespace MarsXApps.Models.Customs
{
    public class SysCustomersAddressModel
    {
        public SysCustomersAddressModel()
        {

            Id = 0;

            IsActive = true;

            CreatedDate = DateTime.Now.MarsX();

            UpdatedDate = DateTime.Now.MarsX();

        }

        public int Id { get; set; }

        public Guid SysCustomerId { get; set; }

        public string Address { get; set; }

        public int MasterProvincesId { get; set; }

        public int MasterDistrictsId { get; set; }

        public int MasterSubdistrictsId { get; set; }

        public string PostCode { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public bool? IsMain { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string? NameTh { get; set; }

        public string? NameEn { get; set; }

        public SysCustomerModel? SysCustomerModel { get; set; }

        public MasterProvinceModel? MasterProvinceModel { get; set; }

        public MasterDistrictModel? MasterDistrictModel { get; set; }

        public MasterSubdistrictModel? MasterSubdistrictModel { get; set; }
    }
}

