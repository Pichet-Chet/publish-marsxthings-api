using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsXApps.Models.Customs
{
    public partial class PromotionModel
    {
        public long Id { get; set; }

        public string CmsWidgetName { get; set; } = null!;

        public string CmsWidgetDisplayPath { get; set; } = null!;

        public string? Name { get; set; }

        public string Description { get; set; } = null!;

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public bool IsActive { get; set; }

        public string? PageUrl { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public string UpdateBy { get; set; } = null!;

        public DateTime UpdateDate { get; set; }

        public CmsWidgetModel? CmsWidgetModel { get; set; }
    }
}
