using System;
using System.ComponentModel.DataAnnotations;

namespace MarsXApps.Models.ThirdParty.SMSMKT.OtpValidate
{
    public class OtpValidateRequest
    {
        public OtpValidateRequest()
        {
            phone = string.Empty;
            token = string.Empty;
            otp_code = string.Empty;
            ref_code = string.Empty;
        }

        [Required(ErrorMessage = "Phone is required")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Token is required")]
        public string token { get; set; }

        [Required(ErrorMessage = "OTP code is required")]
        public string otp_code { get; set; }

        [Required(ErrorMessage = "Reference code is required")]
        public string ref_code { get; set; }

    }


    public class OtpValidateReturn
    {
        public OtpValidateReturn()
        {
            code = string.Empty;
            detail = string.Empty;
            result = new Result();
        }

        public string code { get; set; }
        public string detail { get; set; }

        public Result result { get; set; }
    }


    public class Result
    {
        public Result()
        {
            status = false;
            ref_code = string.Empty;
            token = string.Empty;
        }

        public bool status { get; set; }
        public string ref_code { get; set; }
        public string token { get; set; }
    }
}

