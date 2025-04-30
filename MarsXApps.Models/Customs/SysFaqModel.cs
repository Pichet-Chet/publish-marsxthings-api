using System;
using MarsXApps.Models.Extensions;

namespace MarsXApps.Models.Customs
{
	public class SysFaqModel
	{
		public SysFaqModel()
		{
            Id = 0;
            TitleTh = string.Empty;
            TitleEn = string.Empty;
            ContentTh = string.Empty;
            ContentEn = string.Empty;
            IsActive = true;
            CreatedDate = DateTime.Now.MarsX();
            UpdatedDate = DateTime.Now.MarsX();
        }

        public int Id { get; set; }

        public string? TitleTh { get; set; }

        public string? TitleEn { get; set; }

        public string? ContentTh { get; set; }

        public string? ContentEn { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}

