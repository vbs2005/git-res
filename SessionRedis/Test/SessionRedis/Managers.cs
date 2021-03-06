﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;
using System.Threading;
using System.Xml.Linq;
using System.IO;
using Newtonsoft;
using ServiceStack.Common.Web;
using System.Configuration;

namespace SessionRedis
{
    /// <summary>
    /// 配置管理和初始化
    /// </summary>
    internal class Managers
    {
        /// <summary>
        /// 最大读写数量
        /// </summary>
        const int writeReadCount = 128;

        /// <summary>
        /// 过期时间
        /// </summary>
        internal static int TimeOut = 30; 


        private static PooledRedisClientManager ClientManagers { get; set; }

        internal PooledRedisClientManager GetClientManagers()
        {
            return ClientManagers;
        }

        static Managers()
        {
            string sessionRedis= ConfigurationManager.AppSettings["SessionRedis"];
            string timeOut = ConfigurationManager.AppSettings["SessionRedisTimeOut"];

            if (string.IsNullOrEmpty(sessionRedis))
            {
                throw new Exception("web.config 缺少配置SessionRedis,每台Redis之间用,分割.第一个必须为主机");
            }

            if (string.IsNullOrEmpty(timeOut)==false)
            {
                TimeOut = Convert.ToInt32(timeOut);
            }

            var host = sessionRedis.Split(char.Parse(","));
            var writeHost = new string[] { host[0] };
            var readHosts = host.Skip(1).ToArray();

            ClientManagers = new PooledRedisClientManager(writeHost, readHosts, new RedisClientManagerConfig
            {
                MaxWritePoolSize = writeReadCount,//“写”链接池链接数
                MaxReadPoolSize = writeReadCount,//“读”链接池链接数
                AutoStart = true
            });
        }

    }
}
