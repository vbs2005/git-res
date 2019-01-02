using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SessionRedis;
using System.Web.Mvc;

public static class ExtSessions
{
    public static Session SessionExt(this HttpContextBase context)
    {
        return new Session(context);
    }

   public static Session SessionExt(this Controller controller)
    {
        return new Session(controller.HttpContext);
    }
}