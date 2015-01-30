// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.MessageService
// FileName : UpgradeHelper.cs
// Remark   : 系统升级工具类
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using GZCustoms.Application.SGY.Entity;

namespace GZCustoms.Application.SGY.MessageService.Common
{
    internal class UpgradeHelper
    {
        /// <summary>
        /// 从配置文件获得版本升级信息
        /// </summary>
        /// <param name="docEntity">升级配置文件信息</param>
        /// <returns>升级信息实体</returns>
        internal UpdateInfo GetVersionIdFromConfig(XDocEntity docEntity)
        {
            XDocument configDoc = docEntity.XDoc;
            UpdateInfo info = new UpdateInfo()
            {
                AppName = configDoc.Root.Element("ProgramName").Value,
                SysName = configDoc.Root.Element("SoftName").Value,
                PackageName = configDoc.Root.Element("PacketName").Value,
                PackageUrl = configDoc.Root.Element("PacketUrl").Value,
                Version = configDoc.Root.Element("Version").Value
            };
            return info;
        }
    }
}
