using System;
namespace MarsXApps.Models.Customs
{
	public class CmsWidgetModel
	{
		public CmsWidgetModel()
		{
		}

        public int Id { get; set; }

        public string? Name { get; set; }

        public int? CmsContentId { get; set; }

        public int? DisplaySeq { get; set; }

        public string? DisplayType { get; set; }

        public string? DisplayPath { get; set; }

        public string? DisplayTitle { get; set; }

        public string? DisplaySubTitle { get; set; }

        public string? DisplayRoute { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}

