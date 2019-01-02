using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SessionRedis;

namespace SessionRedis
{
    /// <summary>
    /// 用户状态管理
    /// </summary>
    public class Session
    {
        private HttpContextBase context;

        public const string SessionName = "__sessionName__";

        /// <summary>
        /// 生成一个新ID
        /// </summary>
        /// <returns></returns>
        private string NewGuid()
        {
            return SessionName+Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 当前SessionId
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_context"></param>
        public Session(HttpContextBase _context)
        {
            var context = _context;
            var cookie = context.Request.Cookies.Get(SessionName);
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                SessionId = NewGuid();
                context.Response.Cookies.Add(new HttpCookie(SessionName, SessionId));
                context.Request.Cookies.Add(new HttpCookie(SessionName, SessionId));
            }
            else
            {
                SessionId = cookie.Value;
            }
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public object Get<T>() where T:class,new()
        {
            return new RedisClient<T>().KGet(SessionId);
        }

        /// <summary>
        /// 用户是否在线
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            return new RedisClient<object>().KIsExist(SessionId);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Login<T>(T obj) where T : class,new()
        {
            new RedisClient<T>().KSet(SessionId, obj, new TimeSpan(0, Managers.TimeOut, 0));
        }

        /// <summary>
        /// 退出
        /// </summary>
        public void Quit()
        {
            new RedisClient<object>().KRemove(SessionId);
        }

        /// <summary>
        /// 续期
        /// </summary>
        public void Postpone()
        {
            new RedisClient<object>().KSetEntryIn(SessionId, new TimeSpan(0, Managers.TimeOut, 0));
        }
    }
}

