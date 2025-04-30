using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsXApps.Models.Customs
{
    public class NotificationMessageModel
    {
        public NotificationMessageModel()
        {
        }
        public int Id { get; set; }

        public string? TypeCode { get; set; }

        public string? Title { get; set; }

        public string? MessagePreview { get; set; }

        public string? Message { get; set; }

        public DateOnly? EstimatesDate { get; set; }

        public bool? IsRead { get; set; }

        public bool? IsActive { get; set; }

        public bool? ActionRequired { get; set; }

        public string? SysCustomersId { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
