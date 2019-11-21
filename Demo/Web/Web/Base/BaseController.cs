using MvcSpider.ExpressionExtend;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Base
{
    public class BaseController : Controller
    {
        protected dynamic ToTableJson<T>(IEnumerable<T> data)
        {
            return JsonConvert.SerializeObject(new
            {
                iTotalRecords = data.Count(),
                aaData = data
            }, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });

        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}