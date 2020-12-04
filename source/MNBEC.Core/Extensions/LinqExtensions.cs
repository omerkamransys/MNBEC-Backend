using System;
using System.Collections.Generic;
using System.Linq;

namespace MNBEC.Core.Extensions
{
    /// <summary>
    /// LinqExtensions class provides implementation for extension methods for Linq addons.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Sort 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        public static List<T> Sort<T>(this List<T> data, string sortColumn, bool sortAscending)
        {
            // No sorting needed
            if (string.IsNullOrWhiteSpace(sortColumn))
            {
                return data;
            }

            IOrderedEnumerable<T> orderedQuery = null;

            Func<T, object> expression = item => item.GetType()
                            .GetProperty(sortColumn)
                            .GetValue(item, null);

            if (sortAscending)
            {
                orderedQuery = data.OrderBy(expression);
            }
            else
            {
                orderedQuery = data.OrderByDescending(expression);
            }

            return orderedQuery.ToList();
        }

        //TODO: Code to be tested
        //public static IEnumerable<T> Sort<T>(this IEnumerable<T> data, string sortColumn, string sortDirection)
        //{
        //    // No sorting needed
        //    if (string.IsNullOrWhiteSpace(sortColumn))
        //    {
        //        return data;
        //    }

        //    // Let us sort it
        //    IEnumerable<T> query = from item in data select item;
        //    IOrderedEnumerable<T> orderedQuery = null;

        //    Func<T, object> expression = item => item.GetType()
        //                    .GetProperty(sortColumn)
        //                    .GetValue(item, null);

        //    if (sortDirection.Equals("asc", StringComparison.CurrentCultureIgnoreCase))
        //    {
        //        orderedQuery = query.OrderBy(expression);
        //    }
        //    else
        //    {
        //        orderedQuery = query.OrderByDescending(expression);
        //    }

        //    query = orderedQuery;

        //    return query;
        //}
        //public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool Ascending)
        //{
        //    var param = Expression.Parameter(typeof(T), "p");
        //    var prop = Expression.Property(param, SortField);
        //    var exp = Expression.Lambda(prop, param);
        //    string method = Ascending ? "OrderBy" : "OrderByDescending";
        //    Type[] types = new Type[] { q.ElementType, exp.Body.Type };
        //    var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
        //    return q.Provider.CreateQuery<T>(mce);
        //}
    }
}