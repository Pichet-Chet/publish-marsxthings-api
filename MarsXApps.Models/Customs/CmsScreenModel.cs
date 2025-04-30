using System;
namespace MarsXApps.Models.Customs
{
	public class CmsScreenModel
	{
		public CmsScreenModel()
		{
		}

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public List<CmsContentModel> cmsContentModels { get; set; }

    }
}

