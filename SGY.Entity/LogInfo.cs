// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Entity
// FileName : LogInfo.cs
// Remark   : 日志信息实体类
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
    /// <summary>
    /// 日志信息实体类
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 日志类型：Error，Operation
        /// </summary>
        public string LogType { get; set; }
        /// <summary>
        /// 日志标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime LogTime { get; set; }
        /// <summary>
        /// 日志事件ID
        /// </summary>
        public int EventId { get; set; }
        /// <summary>
        /// 日志来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 错误消息来源
        /// </summary>
        public string Message { get; set; }

    }
}
