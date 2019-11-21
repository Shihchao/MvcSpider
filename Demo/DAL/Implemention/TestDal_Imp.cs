using DAL.Base;
using DAL.Interface;
using Model;
using Model.Test;
using MvcSpider.ExpressionExtend;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Implemention
{
    class TestDal_Imp : BaseDal, ITestDal
    {
        public IEnumerable<TEST_MODEL> Query(IEnumerable<QueryParam> queryParams)
        {
            return this.Query<TEST_MODEL>(queryParams);
        }

        public int AddTestModel(TEST_MODEL model)
        {
            this.Add(model);
            return this.SaveChanges();
        }

        public int DelTestModel(string pkid)
        {
            this.Del<TEST_MODEL>(pkid);
            return this.SaveChanges();
        }
    }
}
