using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using netShop.Domain.Common;

namespace netShop.Application.Interfaces.Repository.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
            Func<IIncludable<T>, IIncludable> includes)
            where T : class
        {
            if (includes == null)
                return query;

            var includable = (Includable<T>)includes(new Includable<T>(query));
            return includable.Input;
        }
    }
}