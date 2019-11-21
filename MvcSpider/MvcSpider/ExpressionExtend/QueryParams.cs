using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSpider.ExpressionExtend
{
    public class QueryParam
    {
        public QueryParam() { }

        public QueryParam(object value, string name, MappingType wherefun = MappingType.Contains, int group = 0)
        {
            this.value = value;
            this.name = name;
            this.mappingType = wherefun;
            this.group = group;
        }

        /// <summary>
        /// 要匹配的值
        /// </summary>
        public object value { get; set; }

        /// <summary>
        /// 要匹配的字段名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 匹配方法
        /// </summary>
        public MappingType mappingType { get; set; }

        /// <summary>
        /// 组,同一组的进行或者运算,不同组的进行并且运算
        /// </summary>
        public int group { get; set; }
    }

    /// <summary>
    /// 匹配方法
    /// </summary>
    public enum MappingType
    {
        /// <summary>
        /// 包含
        /// </summary>
        Contains = 1,

        /// <summary>
        /// 相等
        /// </summary>
        Equal = 2,

        /// <summary>
        /// 不相等
        /// </summary>
        NotEqual = 3,

        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan = 4,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual = 5,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan = 6,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual = 7,

        /// <summary>
        /// 被包含于
        /// </summary>
        BeContains = 8
    }
}
