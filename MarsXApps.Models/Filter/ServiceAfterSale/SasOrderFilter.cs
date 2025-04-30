using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using MarsXApps.Models.Customs;

namespace MarsXApps.Models.Filter.ServiceAfterSale
{
	public class SasOrderFilter : Pagination
	{
		public SasOrderFilter()
		{
            InitializeStringProperties();
        }

        public string? TextSearch { get; set; }

        public Guid? SysCustomerId { get; set; }

        public List<int>? StatusId { get; set; }

        public List<string>? StatusValue { get; set; }

        public string? BranchCode { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Telephone { get; set; }

        public string? MachineDetail { get; set; }

        public string ServiceType { get; set; }

        public string? Description { get; set; }

        public int MasterProvincesId { get; set; }

        public int MasterDistrictsId { get; set; }

        public int MasterSubdistrictsId { get; set; }


        public bool? IsActive { get; set; }

        public static List<SasOrderModel> ApplySorting(List<SasOrderModel> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(SasOrderModel).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(SasOrderModel), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda<Func<SasOrderModel, object>>(
                        Expression.Convert(property, typeof(object)), parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        if (sortType.ToLower() == "asc")
                        {
                            return queryable.OrderBy(lambda.Compile()).ToList();
                        }
                        else if (sortType.ToLower() == "desc")
                        {
                            return queryable.OrderByDescending(lambda.Compile()).ToList();
                        }
                    }
                }
            }

            return queryable;
        }

        private void InitializeStringProperties()
        {
            foreach (var prop in GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string) && prop.CanWrite && prop.CanRead)
                {
                    prop.SetValue(this, string.Empty);
                }
            }
        }

        // Method to trim all string properties

        public void LowerAllProperties()
        {
            foreach (var prop in GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string) && prop.CanWrite && prop.CanRead)
                {
                    var currentValue = prop.GetValue(this) as string;
                    if (currentValue != null)
                    {
                        prop.SetValue(this, currentValue.ToLower());
                    }
                }
            }
        }

        public void TrimAllProperties()
        {
            foreach (var prop in GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string) && prop.CanWrite && prop.CanRead)
                {
                    var currentValue = prop.GetValue(this) as string;
                    if (currentValue != null)
                    {
                        prop.SetValue(this, currentValue.Trim());
                    }
                }
            }
        }

        public void ConvertStatusValuesToLower()
        {
            if (StatusValue != null)
            {
                StatusValue = StatusValue.Select(s => s.ToLower()).ToList();
            }
        }
    }
}

