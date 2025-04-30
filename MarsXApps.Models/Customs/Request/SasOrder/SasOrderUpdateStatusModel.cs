using System;
using System.ComponentModel.DataAnnotations;

namespace MarsXApps.Models.Customs.Request.SasOrder
{
    public class SasOrderUpdateStatusModel
    {
        public SasOrderUpdateStatusModel()
        {

        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string SasStatusValue { get; set; } = null!;

        [Required]
        public string UpdateBy { get; set; } = null!;
        public string? Remark { get; set; }
    }
}

