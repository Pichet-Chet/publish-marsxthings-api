using System;
namespace MarsXApps.Models.Customs.Request.SysCustomer
{
	public class SysCustomerDeleteRequest
	{
		public SysCustomerDeleteRequest()
		{
		}

        public string Id { get; set; }

        public string? DeletedReason { get; set; }
    }
}

