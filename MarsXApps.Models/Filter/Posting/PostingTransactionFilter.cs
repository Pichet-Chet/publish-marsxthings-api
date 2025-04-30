using System;
using MarsXApps.Models.Customs;
using System.Linq.Expressions;
using System.Reflection;

namespace MarsXApps.Models.Filter.Posting
{
    public class PostingTransactionFilter : Pagination
    {
        public PostingTransactionFilter()
        {
            InitializeStringProperties();
        }

        public string? TextSearch { get; set; }

        public Guid? SysCustomersId { get; set; }

        public int? PostingTypeId { get; set; }

        public string? Topic { get; set; } = null!;

        public string? ShortDescription { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public string? Feature { get; set; } = null!;

        public double? Price { get; set; }

        public string? Address { get; set; }

        public int ?MasterProvincesId { get; set; }

        public int? MasterDistrictsId { get; set; }

        public int? PostCode { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsFavorite { get; set; }

        public string? Language { get; set; }

        public static List<PostingTransactionModel> ApplySorting(List<PostingTransactionModel> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(PostingTransactionModel).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(PostingTransactionModel), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda<Func<PostingTransactionModel, object>>(
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

