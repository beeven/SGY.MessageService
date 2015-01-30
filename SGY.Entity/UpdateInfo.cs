// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Entity
// FileName : UpdateInfo.cs
// Remark   : 系统升级信息实体类
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZCustoms.Application.SGY.Entity
{
    public class UpdateInfo
    {
        /// <summary>
        /// 程序名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SysName { get; set; }
        /// <summary>
        /// 升级包名称
        /// </summary>
        public string PackageName { get; set; }
        /// <summary>
        /// 升级包目录
        /// </summary>
        public string PackageUrl { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
    }
}
