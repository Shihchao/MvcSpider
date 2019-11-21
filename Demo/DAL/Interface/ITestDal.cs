using DAL.Implemention;
using Model.Test;
using MvcSpider.ExpressionExtend;
using MvcSpider.IOC;
using System.Collections.Generic;

namespace DAL.Interface
{
    [Implement(typeof(TestDal_Imp))]
    public interface ITestDal
    {
        /// <summary>
        /// 通用查询方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ps"></param>
        /// <returns></returns>
        IEnumerable<TEST_MODEL> Query(IEnumerable<QueryParam> queryParams);

        int AddTestModel(TEST_MODEL model);

        int DelTestModel(string pkid);
    }
}
