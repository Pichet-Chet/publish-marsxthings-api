using System;
namespace MarsXApps.Models.Customs
{
	public class PostingTypeModel
	{
		public PostingTypeModel()
		{
		}

        public int? Id { get; set; }

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? imgIcon { get; set; }
    }
}

