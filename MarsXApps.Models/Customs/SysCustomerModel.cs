using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarsXApps.Models.Customs
{
	public class SysCustomerModel
	{
		public SysCustomerModel()
		{
		}

        public Guid Id { get; set; }

        public string? Email { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public int? MasterPrefixNameId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Gender { get; set; }

        public int? Age { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string MobilePhone { get; set; } = null!;

        public string? LineId { get; set; }

        public string? CitiyzenCode { get; set; }

        public string? TaxCode { get; set; }

        public string? Image { get; set; }

        public int? MasterCustomerGroupId { get; set; }

        public int? MasterCustomerTypeId { get; set; }

        public int? MasterThaiProvincesId { get; set; }

        public int? MasterThaiDistrictsId { get; set; }

        public int? MasterThaiSubdistrictsId { get; set; }

        public DateTime? RegisterDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool IsActive { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public string? MobilePhoneCode { get; set; }

        public string? AccessToken { get; set; }

        public DateTime? AccessTokenExpire { get; set; }

        [NotMapped]
        public string? ImageBase64 { get; set; }

    }
}

