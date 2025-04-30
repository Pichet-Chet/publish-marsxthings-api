using System;
namespace MarsXApps.Models.Customs
{
    public class SasOrderAppointmentModel
    {
        public SasOrderAppointmentModel()
        {
        }

        public int Id { get; set; }

        public int SasOrderId { get; set; }

        public DateOnly Date { get; set; }

        public string Time { get; set; } = null!;

        public string? ReserveName { get; set; }

        public string? ReserveTel { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

        public virtual SasOrderModel? SasOrderModel { get; set; }
    }
}

