using System;
namespace MarsXApps.Models.Customs
{
    public class PostCodeDetectionModel
    {
        public PostCodeDetectionModel()
        {
            subDistricts = new List<SubDistrictDetectionModel>();
        }

        public int ProviceId { get; set; }

        public string ProviceNameTh { get; set; }

        public string ProviceNameEn { get; set; }

        public int DistrictId { get; set; }

        public string DistrictNameTh { get; set; }

        public string DistrictNameEn { get; set; }


        public List<SubDistrictDetectionModel> subDistricts { get; set; }

    }

    public class SubDistrictDetectionModel
    {
        public int Id { get; set; }

        public string? NameTh { get; set; }

        public string? NameEn { get; set; }

    }
}

