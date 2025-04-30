using System;
namespace MarsXApps.Models.Customs
{
    public class SysTermsAndConditionModel
    {
        public SysTermsAndConditionModel()
        {
        }

        public int Id { get; set; }

        public string? ContentTh { get; set; }

        public string? ContentEn { get; set; }

        public string Version { get; set; } = null!;

        public DateTime EffectiveDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }
    }
}

