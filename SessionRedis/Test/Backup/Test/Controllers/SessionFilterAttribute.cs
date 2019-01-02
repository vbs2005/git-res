using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using SessionRedis;

namespace Test
{
    public class SessionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 每次请求都续期
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            new Session(filterContext.HttpContext).Postpone();
        }
    }
}

