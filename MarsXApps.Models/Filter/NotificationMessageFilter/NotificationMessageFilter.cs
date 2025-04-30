using System;
using System.Linq.Expressions;
using System.Reflection;
using MarsXApps.Models.Customs;

namespace MarsXApps.Models.Filter.NotificationMessageFilter
{
	public class NotificationMessageFilter : Pagination
	{
		public NotificationMessageFilter()
		{
        }
        public string? TextSearch { get; set; }
        public string? Title { get; set; }
        public string? SysCustomersId { get; set; }
        public string? TypeCode { get; set; }
        public int? Id { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsRead { get; set; }


    }
}

