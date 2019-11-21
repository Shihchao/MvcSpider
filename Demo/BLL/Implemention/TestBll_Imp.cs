using BLL.Interface;
using DAL;
using DAL.Interface;
using Model.Test;
using MvcSpider.ExpressionExtend;
using System.Collections.Generic;

namespace BLL.Implemention
{
    class TestBll_Imp : ITestBll
    {
        public IEnumerable<TEST_MODEL> Query(IEnumerable<QueryParam> queryParams)
        {
            return DalContainer.Get<ITestDal>().Query(queryParams);
        }

        public int AddTestModel(TEST_MODEL model)
        {
            if (string.IsNullOrEmpty(model.Name) || model.Age == 0 || string.IsNullOrEmpty(model.Language))
            {
                return 0;
            }
            return DalContainer.Get<ITestDal>().AddTestModel(model);
        }

        public int DelTestModel(string pkid)
        {
            return DalContainer.Get<ITestDal>().DelTestModel(pkid);
        }
    }
}
