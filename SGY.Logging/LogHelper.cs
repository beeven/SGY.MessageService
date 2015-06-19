// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Logging
// FileName : LogHelper.cs
// Remark   : 日志处理工具类
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GZCustoms.Application.SGY.Entity;
using GZCustoms.Application.SGY.Data;
using GZCustoms.Application.SGY.Data.Interface;

namespace GZCustoms.Application.SGY.Logging
{
    public class LogHelper
    {
        private static LogHelper Instance { get; set; }        

        private void LogInfo(string errMessage, int eventId, string title, string category, string source, string msg)
        {
            IMessageDataHelper logDataHelper = DataHelperFactory.GetMessageDataHelper();
            var logInfo = new LogInfo() { LogContent = errMessage, 
                LogTime = DateTime.Now,
                EventId = eventId, 
                Title = title,
                LogType = category, 
                Source = source,
                Message = msg    
            };
            logDataHelper.LoggingInfo(logInfo);
        }        

        private void LogInfo(string errMessage, int eventId, string title, string category, string source)
        {
            LogInfo(errMessage, eventId, title, category, source, null);
        }

        private void LogInfo(string errMessage, int eventId, string title, string category)
        {
            LogInfo(errMessage, eventId, title, category, string.Empty);
        }

        /// <summary>
        /// 日志操作类静态实例
        /// </summary>
        /// <returns></returns>
        public static LogHelper GetInstance()
        {
            if (Instance == null)
                Instance = new LogHelper();
            return Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errMessage">错误信息</param>
        /// <param name="eventId">错误方法ID</param>
        /// <param name="title">错误标题（暂定为方法名称）</param>
        /// <param name="source">错误来源</param>
        /// <param name="msg">错误消息内容</param>
        public void LogErrInfo(string errMessage, int eventId, string title, string source, string msg)
        {
            LogInfo(errMessage, eventId, title, Context.LogCategoryErr, source, msg);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="errMessage">错误信息</param>
        /// <param name="eventId">错误方法ID</param>
        /// <param name="title">错误标题（暂定为方法名称）</param>
        /// <param name="source">错误来源</param>
        public void LogErrInfo(string errMessage, int eventId, string title, string source)
        {
            LogInfo(errMessage, eventId, title, Context.LogCategoryErr, source);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="errMessage">错误信息</param>
        /// <param name="eventId">错误方法ID</param>
        /// <param name="title">错误标题（暂定为方法名称）</param>
        public void LogErrInfo(string errMessage, int eventId, string title)
        {
            LogInfo(errMessage, eventId, title, Context.LogCategoryErr);
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="opMessage">操作信息</param>
        /// <param name="eventId">操作方法ID</param>
        /// <param name="title">操作标题（暂定方法名称）</param>
        /// <param name="source">操作来源</param>
        public void LogOperation(string opMessage, int eventId, string title, string source)
        {
            LogInfo(opMessage, eventId, title, Context.LogCategoryOperation, source);
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="opMessage">操作信息</param>
        /// <param name="eventId">操作方法ID</param>
        /// <param name="title">操作标题（暂定方法名称）</param>
        public void LogOperation(string opMessage, int eventId, string title)
        {
            LogInfo(opMessage, eventId, title, Context.LogCategoryOperation);
        }       
    }
}
