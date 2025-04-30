using System;
using MarsXApps.Models.Extensions;

namespace MarsXApps.Models.Customs
{
    public class SysPrivacyModel
    {
        public SysPrivacyModel()
        {
            Id = 0;
            @Version = "1";
            ContentEn = string.Empty;
            ContentTh = string.Empty;
            IsActive = true;
            CreatedBy = "system";
            UpdatedBy = "system";

            CreatedDate = DateTime.Now.MarsX();
            UpdatedDate = DateTime.Now.MarsX();
        }

        public int Id { get; set; }

        public string ContentTh { get; set; }

        public string ContentEn { get; set; }

        public string Version { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}

