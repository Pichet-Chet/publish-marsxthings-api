using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace MarsXApps.Models.Customs
{
    public class PostingTransactionModel
    {
        public PostingTransactionModel()
        {
            CountLike = 0;
            CountComment = 0;
        }

        public int? Id { get; set; }

        public Guid SysCustomersId { get; set; }

        [NotMapped]
        public string? SysCustomerUsername { get; set; }

        [NotMapped]
        public string? SysCustomerFirstname { get; set; }

        [NotMapped]
        public string? SysCustomerLastname { get; set; }

        public int PostingTypeId { get; set; }

        [NotMapped]
        public string? PostingTypeNameTh { get; set; }

        [NotMapped]
        public string? PostingTypeNameEn { get; set; }

        [NotMapped]
        public string? FeatureImage { get; set; } = null!;

        public IFormFile? FeatureImageUpload { get; set; } = null!;

        public string Topic { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Feature { get; set; } = null!;

        public double Price { get; set; }

        public string? PriceStr { get; set; }

        public string? Address { get; set; }

        public int MasterProvincesId { get; set; }

        [NotMapped]
        public string? MasterProvincesNameTh { get; set; }

        [NotMapped]
        public string? MasterProvincesNameEn { get; set; }



        public int MasterDistrictsId { get; set; }

        [NotMapped]
        public string? MasterDistrictsNameTh { get; set; }

        [NotMapped]
        public string? MasterDistrictsNameEn { get; set; }



        public int MasterSubdistrictsId { get; set; }

        [NotMapped]
        public string? MasterSubdistrictsIdNameTh { get; set; }

        [NotMapped]
        public string? MasterSubdistrictsIdNameEn { get; set; }


        public int? PostCode { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime? UpdatedDate { get; set; }

        public int UnitPriceId { get; set; }


        [NotMapped]
        public string? UnitPriceNameTh { get; set; }

        [NotMapped]
        public string? UnitPriceNameEn { get; set; }


        public string ContactName { get; set; } = null!;

        public string ContactTel { get; set; } = null!;

        public string? ContactAddress { get; set; }

        public string? ContactFullAddressTh { get; set; }

        public string? ContactFullAddressEn { get; set; }


        public int ContactMasterProvincesId { get; set; }

        [NotMapped]
        public string? ContactMasterProvincesNameTh { get; set; }

        [NotMapped]
        public string? ContactMasterProvincesNameEn { get; set; }




        public int ContactMasterDistrictsId { get; set; }

        [NotMapped]
        public string? ContactMasterDistrictsNameTh { get; set; }

        [NotMapped]
        public string? ContactMasterDistrictsNameEn { get; set; }



        public int ContactMasterSubdistrictsId { get; set; }

        [NotMapped]
        public string? ContactMasterSubdistrictsNameTh { get; set; }

        [NotMapped]
        public string? ContactMasterSubdistrictsNameEn { get; set; }




        public int ContactPostCode { get; set; }


        [NotMapped]
        public bool isLike { get; set; }

        [NotMapped]
        public bool isFavorite { get; set; }

        [NotMapped]
        public int CountLike { get; set; }

        [NotMapped]
        public int CountComment { get; set; }
    }
}

