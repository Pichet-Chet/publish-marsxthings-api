using System;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using MarsXApps.Models.Customs;

namespace MarsXApps.Models.Filter.MasterData
{
    public class MasterDistrictFilter : Pagination
    {
        public MasterDistrictFilter()
        {
            InitializeStringProperties();
        }

        [Required]
        public int MasterProvincesId { get; set; }

        public string? NameTh { get; set; }

        public string? NameEn { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }


        public static List<MasterDistrictModel> ApplySorting(List<MasterDistrictModel> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(MasterDistrictModel).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(MasterDistrictModel), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda<Func<MasterDistrictModel, object>>(
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
    }
}

