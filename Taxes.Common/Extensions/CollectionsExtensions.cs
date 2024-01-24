using System.Linq.Expressions;
using System.Reflection;
using Taxes.Common.Enums.Paging;
using Taxes.Common.Models.Paging;

namespace Taxes.Common.Extensions
{
    public static class CollectionsExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any<T>();
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static IQueryable<TModel> SortAndFilter<TModel>(this IQueryable<TModel> query, PageSortInfo sortInfo)
        {
            return Sort<TModel>(query, sortInfo).Skip(sortInfo.PageIndex * sortInfo.PageSize).Take(sortInfo.PageSize);
        }

        public static IQueryable<TModel> Sort<TModel>(this IQueryable<TModel> query, PageSortInfo pageSort)
        {
            string orderDist = pageSort.SortOrder != null && pageSort.SortOrder.Value == SortOrderEnum.Asc ? nameof(OrderByProperty) : nameof(OrderByPropertyDescending);

            Type entityType = typeof(TModel);
            PropertyInfo sortProperty = entityType.GetProperty(pageSort.SortField);

            if (sortProperty == null)
                throw new ArgumentException($"Wrong field for sorting: {pageSort.SortField}");

            MethodInfo m = typeof(CollectionsExtensions).GetMethod(orderDist).MakeGenericMethod(entityType, sortProperty.PropertyType);
            return (IQueryable<TModel>)m.Invoke(null, new object[] { query, sortProperty });
        }

        public static IQueryable<TModel> OrderByPropertyDescending<TModel, TRet>(IQueryable<TModel> q, PropertyInfo p)
        {
            ParameterExpression pe = Expression.Parameter(typeof(TModel));
            Expression se = Expression.Convert(Expression.Property(pe, p), typeof(TRet));
            return q.OrderByDescending(Expression.Lambda<Func<TModel, TRet>>(se, pe));
        }

        public static IQueryable<TModel> OrderByProperty<TModel, TRet>(IQueryable<TModel> q, PropertyInfo p)
        {
            ParameterExpression pe = Expression.Parameter(typeof(TModel));
            Expression se = Expression.Convert(Expression.Property(pe, p), typeof(TRet));
            return q.OrderBy(Expression.Lambda<Func<TModel, TRet>>(se, pe));
        }
    }
}
