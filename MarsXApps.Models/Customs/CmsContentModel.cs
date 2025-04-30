using System;
namespace MarsXApps.Models.Customs
{
	public class CmsContentModel
	{
		public CmsContentModel()
		{
		}

        public int Id { get; set; }

        public string? Name { get; set; }

        public int? CmsScreenId { get; set; }

        public int? DisplaySeq { get; set; }

        public string? DisplayTitle { get; set; }

        public string? DisplaySubTitle { get; set; }

        public string? DisplayReadMore { get; set; }

        public string? DisplayReadMoreRoute { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public List<CmsWidgetModel> cmsWidgets { get; set; }
    }
}

