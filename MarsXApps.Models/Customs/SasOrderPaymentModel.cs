using System;

namespace MarsXApps.Models.Customs
{
    public class SasOrderPaymentModel
    {

        public int Id { get; set; }

        public int SasOrderId { get; set; }

        public string? SlipImage { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string? ImageBase64 { get; set; }
        public string? ImageUrl { get; set; }


    }
}

