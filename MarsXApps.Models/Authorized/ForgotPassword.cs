using System;
using System.ComponentModel.DataAnnotations;
using MarsXApps.Models.ThirdParty.SMSMKT.OtpValidate;

namespace MarsXApps.Models.Authorized
{
	public class ForgotPassword : OtpValidateRequest
    {
		public ForgotPassword()
		{
			Password = string.Empty;
			ConfirmPassword = string.Empty;
		}
		[Required]
		public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
	}
}

