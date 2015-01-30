using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Configuration;
using System.Xml.Linq;
using GZCustoms.Application.SGY.MessageService.Common;


namespace GZCustoms.Application.SGY.MessageService.Config
{
    internal static class ConfigInfo
    {
        private static MemoryCache Cache { get; set; }

        private static MemoryCache GetCache()
        {
            return Cache ?? (Cache = MemoryCache.Default);
        }

        internal static string CurrentId
        {
            get
            {
                return ConfigurationManager.AppSettings["CurrentId"];
            }
        }

        internal static string TscIdHead
        {
            get
            {
                return ConfigurationManager.AppSettings["TscIdHead"];
            }
        }

        internal static string DocumentNo
        {
            get
            {
                return ConfigurationManager.AppSettings["DocumentNo"];
            }
        }

        internal static string Tns
        {
            get
            {
                return ConfigurationManager.AppSettings["Tns"];
            }
        }

        internal static string Path
        {
            get
            {
                return ConfigurationManager.AppSettings["Path"];
            }
        }

        internal static int DeclType
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["DeclType"]);
            }
        }



        /// <summary>
        /// 获得模板信息实体
        /// </summary>
        /// <returns></returns>
        internal static XDocEntity GetTemplateDocEntity()
        {
            ObjectCache cache = GetCache();
            string key = "TCSMsgTemplate";
            string xmlTmpStr = cache[key] as string;
            if (String.IsNullOrEmpty(xmlTmpStr))
            {
                string baseUrl = AppDomain.CurrentDomain.BaseDirectory;
                if (!baseUrl.EndsWith("\\"))
                    baseUrl += "\\";
                baseUrl += @"Xml\map.xml";
                if (!System.IO.File.Exists(baseUrl))
                    throw new Exception("默认的模板Xml文件不存在");
                XDocument xDoc = XDocument.Load(baseUrl);
                var policy = new CacheItemPolicy { SlidingExpiration = GetSlidingExpiration() };
                cache.Set(key, xDoc.ToString(), policy);
                return new XDocEntity(xDoc, Tns);
            }
            return new XDocEntity(XDocument.Parse(xmlTmpStr), Tns);
        }

        /// <summary>
        /// 获取升级配置文件信息实体
        /// </summary>
        /// <returns></returns>
        internal static XDocEntity GetUpgradeConfigDocEntity()
        {
            ObjectCache cache = GetCache();
            string key = "UpgradeConfig";
            string xmlUpgradeConfig = cache[key] as string;
            if (String.IsNullOrEmpty(xmlUpgradeConfig))
            {
                string baseUrl = AppDomain.CurrentDomain.BaseDirectory;
                if (!baseUrl.EndsWith("\\"))
                    baseUrl += "\\";
                baseUrl += @"Xml\Upgrade.xml";
                if (!System.IO.File.Exists(baseUrl))
                    throw new Exception("默认的模板Xml文件不存在");
                XDocument xDoc = XDocument.Load(baseUrl);
                var policy = new CacheItemPolicy { SlidingExpiration = GetSlidingExpiration() };
                cache.Set(key, xDoc.ToString(), policy);
                return new XDocEntity() { XDoc = xDoc};
            }
            return new XDocEntity() { XDoc = XDocument.Parse(xmlUpgradeConfig) };
        }


        static TimeSpan GetSlidingExpiration()
        {
            string slidingExpiration = ConfigurationManager.AppSettings["SlidingExpiration"];
            if (string.IsNullOrEmpty(slidingExpiration))
                slidingExpiration = Context.DefaultSlidingExpiration;
            return TimeSpan.FromMinutes(Convert.ToInt32(slidingExpiration));
        }

    }
}
