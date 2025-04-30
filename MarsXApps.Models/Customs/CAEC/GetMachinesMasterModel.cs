using System;
using System.ComponentModel.DataAnnotations;

namespace MarsXApps.Models.Customs.CAEC
{
    public class GetMachinesMasterModel
    {
        public GetMachinesMasterModel()
        {
            model = string.Empty;

            //serialNo = string.Empty;

            //chassis = string.Empty;
        }


        [Required]
        public string model { get; set; }

        //public string serialNo { get; set; }
        
        //public string chassis { get; set; } = null!;

        public string searchMachineIdentity { get; set; }

        public string textSearch { get; set; }

    }
}

