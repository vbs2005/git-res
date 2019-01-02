using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SessionRedis
{
    /// <summary>
    /// Session管理
    /// </summary>
    public class SessionManager
    {
        /// <summary>
        /// 用户列表数量
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return new RedisClient<object>().KSearchKeys(Session.SessionName + "*").Count;
        }

        /// <summary>
        /// 注销某个用户
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool GetRemove<T>(string sessionId) where T : class,new()
        {
            return new RedisClient<T>().KRemove(sessionId);
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public T GetModel<T>(string sessionId) where T : class,new()
        {
            return new RedisClient<T>().KGet(sessionId);
        }

        /// <summary>
        /// 在线用户对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> GetValueAll<T>() where T : class,new()
        {
            return new RedisClient<T>().KSearchValues(Session.SessionName+"*");
        }

        /// <summary>
        /// 在线用户SessionId列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<string> GetKeyAll<T>() where T : class,new()
        {
            return new RedisClient<T>().KSearchKeys(Session.SessionName + "*");
        }

    }
}
