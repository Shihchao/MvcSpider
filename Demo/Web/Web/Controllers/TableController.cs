using BLL;
using BLL.Interface;
using Model.Test;
using MvcSpider.ExpressionExtend;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Base;

namespace Web.Controllers
{
    public class TableController : BaseController
    {
        // GET: Table
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult Query(List<QueryParam> queryParams)
        {
            return Content(ToTableJson(BllContanier.Get<ITestBll>().Query(queryParams)));
        }

        public JsonResult Add(TEST_MODEL model)
        {
            return Json(BllContanier.Get<ITestBll>().AddTestModel(model));
        }

        public JsonResult Del(string pkid)
        {
            return Json(BllContanier.Get<ITestBll>().DelTestModel(pkid));
        }
    }
}