using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SessionRedis;

namespace Test.Controllers
{
    public class test
    {
        public string name { get; set; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           var t= this.SessionExt().Get<test>();

            return View(t);
        }

        public ActionResult Login(string name)
        {
            test t=new test (){ name=name};

            //登录
            this.SessionExt().Login<test>(t);

            return this.RedirectToAction("Index");
        }

    }
}
