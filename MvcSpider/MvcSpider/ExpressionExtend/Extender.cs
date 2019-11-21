using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MvcSpider.ExpressionExtend
{
    public static class Extender
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, IEnumerable<QueryParam> queryParams)
        {
            if (queryParams == null || queryParams.Count() == 0)
            {
                return query;
            }

            ParameterExpression modelParamEs = Expression.Parameter(typeof(T), "modelParamEs");
            Expression expressionAnd = Expression.Constant(true, typeof(bool));

            var groupList = queryParams.GroupBy(x => x.group);
            foreach (var g in groupList)
            {
                Expression expressionOr = Expression.Constant(false, typeof(bool));
                foreach (var p in g.ToArray())
                {
                    var psex = BuildExpression(p, modelParamEs);
                    if (psex != null)
                    {
                        expressionOr = Expression.OrElse(expressionOr, psex);
                    }
                }
                expressionAnd = Expression.AndAlso(expressionAnd, expressionOr);
            }

            return query.Where(Expression.Lambda<Func<T, bool>>(expressionAnd, modelParamEs));
        }

        /// <summary>
        /// 根据过滤参数获取表达式
        /// </summary>
        /// <param name="isand">是否是And运算</param>
        /// <param name="queryParam">过滤参数集合</param>
        /// <returns>表达式</returns>
        static Expression BuildExpression(QueryParam queryParam, ParameterExpression modelParamsEs)
        {
            Expression psex = Expression.Constant(true, typeof(bool));

            var pro = modelParamsEs.Type.GetProperty(queryParam.name);
            var left = Expression.Property(modelParamsEs, pro);
            var right = Expression.Constant(Convert.ChangeType(GetCorrectValue(queryParam.value), pro.PropertyType), pro.PropertyType);


            switch (queryParam.mappingType)
            {
                case MappingType.Contains:
                    return Expression.AndAlso(psex, Expression.Call(left, pro.PropertyType.GetMethod("Contains", new Type[] { typeof(string) }), right));
                case MappingType.BeContains:
                    return Expression.AndAlso(psex, Expression.Call(right, pro.PropertyType.GetMethod("Contains", new Type[] { typeof(string) }), left));
                case MappingType.Equal:
                    return Expression.AndAlso(psex, Expression.Equal(left, right));
                case MappingType.NotEqual:
                    return Expression.AndAlso(psex, Expression.NotEqual(left, right));
                case MappingType.GreaterThan:
                    return Expression.AndAlso(psex, Expression.GreaterThan(left, right));
                case MappingType.GreaterThanOrEqual:
                    return Expression.AndAlso(psex, Expression.GreaterThanOrEqual(left, right));
                case MappingType.LessThan:
                    return Expression.AndAlso(psex, Expression.LessThan(left, right));
                case MappingType.LessThanOrEqual:
                    return Expression.AndAlso(psex, Expression.LessThanOrEqual(left, right));
            }

            return null;
        }

        static object GetCorrectValue(object value)
        {
            string[] v = value as string[];
            if (v.Length > 0)
            {
                return v[0] as object;
            }

            return value;
        }
    }
}
