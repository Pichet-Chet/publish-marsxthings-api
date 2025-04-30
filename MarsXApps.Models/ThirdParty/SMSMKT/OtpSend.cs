using System;
using System.Numerics;
using System.Reflection.Emit;

namespace MarsXApps.Models.ThirdParty.SMSMKT.OtpSend
{
    public class OtpSendRequest
    {
        public OtpSendRequest()
        {
            project_key = string.Empty;
            phone = string.Empty;
            ref_code = string.Empty;
        }

        public string project_key { get; set; }
        public string phone { get; set; }
        public string ref_code { get; set; }

    }


    public class OtpSendReturn
    {
        public OtpSendReturn()
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
            phone = string.Empty;
            token = string.Empty;
            otp_code = string.Empty;
            ref_code = string.Empty;
        }

        public string phone { get; set; }
        public string token { get; set; }
        public string otp_code { get; set; }
        public string ref_code { get; set; }
    }
}

