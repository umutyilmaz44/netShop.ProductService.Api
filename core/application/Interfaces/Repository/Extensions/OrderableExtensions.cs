using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using NetShop.ProductService.Domain.Common;
using NetShop.ProductService.Application.Exceptions;

namespace NetShop.ProductService.Application.Interfaces.Repository.Extensions
{
    public static class OrderableExtensions
    {
        public static Tuple<Expression, Type> GetSelector<T>(IEnumerable<string> propertyNames)
        {
            var parameter = Expression.Parameter(typeof(T));
            Expression body = parameter;

            foreach (var property in propertyNames)
            {
                body = Expression.Property(body,
                    body.Type.GetProperty(property));
            }

            return Tuple.Create(Expression.Lambda(body, parameter) as Expression
                , body.Type);
        }

        public static Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderByFunc<T>(this Tuple<IEnumerable<string>, string> sortCriteria)
        {
            var selector = GetSelector<T>(sortCriteria.Item1);
            Type[] argumentTypes = new[] { typeof(T), selector.Item2 };

            var orderByMethod = typeof(Queryable).GetMethods()
                .First(method => method.Name == "OrderBy"
                    && method.GetParameters().Count() == 2)
                    .MakeGenericMethod(argumentTypes);
            var orderByDescMethod = typeof(Queryable).GetMethods()
                .First(method => method.Name == "OrderByDescending"
                    && method.GetParameters().Count() == 2)
                    .MakeGenericMethod(argumentTypes);

            if (sortCriteria.Item2.Trim().ToLower() == "desc")
                return query => (IOrderedQueryable<T>)
                    orderByDescMethod.Invoke(null, new object[] { query, selector.Item1 });
            else
                return query => (IOrderedQueryable<T>)
                    orderByMethod.Invoke(null, new object[] { query, selector.Item1 });
        }

        public static Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderBy<T>(string sort) where T : BaseEntity
        {
            Expression resultExp = null;
            Type typeQueryable = typeof(IQueryable<T>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
            LambdaExpression outerExpression = Expression.Lambda(argQueryable, argQueryable);

            MatchCollection mc = null;
            if (!string.IsNullOrEmpty(sort))
            {
                var regex = new Regex(@"^(\s*(?<fieldName>\w+)(?<sortType>\s+\w+)?,?)*$");
                mc = regex.Matches(sort.Trim());
            }
            if (mc != null)
            {
                PropertyInfo pi;
                Type entityType = typeof(T);
                String methodName = string.Empty;

                ParameterExpression arg = Expression.Parameter(entityType, "x");
                Expression expr = arg;

                foreach (Match match in mc)
                {
                    pi = entityType.GetProperty(match.Groups["fieldName"].Value.Trim(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (pi != null)
                    {
                        Expression propertyExpr = Expression.Property(expr, pi);
                        Type propertyType = pi.PropertyType;
                        LambdaExpression lambdaExp = Expression.Lambda(propertyExpr, arg);
                        if (resultExp != null)
                        {
                            methodName = (string.IsNullOrEmpty(match.Groups["sortType"].Value) || match.Groups["sortType"].Value.Trim().ToLower() == "asc")
                                            ? "ThenBy"
                                            : "ThenByDescending";

                            /// No generic method 'ThenBy' on type 'System.Linq.Queryable' is compatible with the supplied type arguments and arguments.
                            /// No type arguments should be provided if the method is non - generic.
                            Expression exp = Expression.Call(typeof(Queryable), methodName, new Type[] { entityType, propertyType }, outerExpression.Body, Expression.Quote(lambdaExp));

                            MethodInfo minfo = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public).First(m => m.Name == methodName);

                            /// Method System.Linq.IOrderedQueryable`1
                            /// [TSource] OrderBy[TSource,TKey](System.Linq.IQueryable`1
                            /// [TSource],System.Linq.Expressions.Expression`1
                            /// [System.Func`2[TSource,TKey]]) is a generic method definition
                            /// Parameter name: method
                            resultExp = Expression.Call(minfo, exp, resultExp);
                        }
                        else
                        {
                            methodName = (string.IsNullOrEmpty(match.Groups["sortType"].Value) || match.Groups["sortType"].Value.Trim().ToLower() == "asc")
                                            ? "OrderBy"
                                            : "OrderByDescending";
                            resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { entityType, propertyType }, outerExpression.Body, Expression.Quote(lambdaExp));
                        }
                    }
                    else {
                        throw new BadRequestException($"{match.Groups["fieldName"].Value.Trim()} field not found");
                    }
                }
            }
            if (resultExp != null)
            {
                LambdaExpression orderedLambda = Expression.Lambda(resultExp, argQueryable);
                return (Func<IQueryable<T>, IOrderedQueryable<T>>)orderedLambda.Compile();
            }
            else
                return null;
        }
    }
}