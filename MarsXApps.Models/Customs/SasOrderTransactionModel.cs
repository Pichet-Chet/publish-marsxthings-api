using System;
namespace MarsXApps.Models.Customs
{
    public class SasOrderTransactionModel
    {
        public SasOrderTransactionModel()
        {
            SasStatusModel = new SasStatusModel();
        }

        public int Id { get; set; }

        public int SasOrderId { get; set; }

        public int SasStatusId { get; set; }

        public string CaecUserName { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

        public string Remark { get; set; } = null!;

        public string SasStatusValue { get; set; } = null!;


        public string? ImageUrl{ get; set; }

        public SasStatusModel SasStatusModel { get; set; }
    }
}

