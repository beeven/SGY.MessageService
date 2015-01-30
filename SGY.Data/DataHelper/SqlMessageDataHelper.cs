// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Data
// FileName : SqlMessageDataHelper.cs
// Remark   : 报文数据操作类（SQLServer）
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using GZCustoms.Application.SGY.Data.Interface;
using GZCustoms.Application.SGY.Data.Config;
using GZCustoms.Application.SGY.Entity;
using GZCustoms.Application.SGY.MessageService.Interface;


namespace GZCustoms.Application.SGY.Data.DataHelper
{
    public class SqlMessageDataHelper : IMessageDataHelper
    {
        private Database Db { get; set; }

        internal SqlMessageDataHelper()
        {
            Db = DatabaseFactory.GetDefaultDatabase(DatabaseType.SqlServer);
        }

        /// <summary>
        /// 验证激活码
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器编码</param>
        /// <returns>验证结果</returns>
        public KeyInfo GetKeyInfo(string keyValue, string machineCode)
        {
            IRowMapper<KeyInfo> mapper = MapBuilder<KeyInfo>.MapAllProperties()
              .Map(g => g.Guid).ToColumn("GUID")
              .Map(g => g.KeyName).ToColumn("KEY_NAME")
              .Map(g => g.KeyValue).ToColumn("KEY_VALUE")
              .Map(g => g.StartDate).ToColumn("START_DATE")
              .Map(g => g.EndDate).ToColumn("END_DATE")
              .Map(g => g.MachineCode).ToColumn("MACHINE_CODE")
              .Map(g => g.EntName).ToColumn("COM_NAME").Build();

            return Db.ExecuteSqlStringAccessor<KeyInfo>(Context.SqlGetKeyInfo, mapper)
                .Where(e => e.KeyValue == keyValue && e.MachineCode == machineCode).FirstOrDefault();
        }

        /// <summary>
        /// 获取TCS报文当前Id
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>返回Id</returns>
        public long GetMaxTcsCurrentId()
        {
            var cmd = Db.GetSqlStringCommand(Context.SqlGetMaxTcsCurrId);
            Db.AddInParameter(cmd, "DECL_DATE", DbType.Date, DateTime.Now.Date);
            var result = Db.ExecuteScalar(cmd);
            return result != DBNull.Value ? Convert.ToInt64(result) : 0;
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="loginName">用户名称</param>
        /// <param name="password">密码</param>
        /// <returns>用户信息</returns>        
        public UserInfo GetLoginUserInfo(string loginName, string password)
        {
            IRowMapper<UserInfo> mapper = MapBuilder<UserInfo>.MapAllProperties()
               .Map(g => g.Guid).ToColumn("GUID")
               .Map(g => g.OrgCode).ToColumn("ORG_CODE")
               .Map(g => g.CusCode).ToColumn("TRADE_CODE")
               .Map(g => g.UserName).ToColumn("USER_NAME")
               .Map(g => g.LoginName).ToColumn("LOGIN_NAME")
               .Map(g => g.Password).ToColumn("LOGIN_PASSWORD")
               .Map(g => g.EntName).ToColumn("COM_NAME").Build();

            return Db.ExecuteSqlStringAccessor<UserInfo>(Context.SqlGetLoginUserInfo, mapper)
                .Where(e => e.LoginName == loginName && e.Password == password).FirstOrDefault();
        }


        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="loginName">用户登陆名称</param>
        /// <param name="pwdOld">旧密码</param>
        /// <param name="pwdNew">新密码</param>
        /// <returns>返回修改结果 1 表示成功，0 表示失败</returns>
        public int UpdateUserPassword(string loginName, string pwdOld, string pwdNew)
        {
            var cmd = Db.GetSqlStringCommand(Context.SqlUpdateLoginUserPwd);
            Db.AddInParameter(cmd, "LOGIN_PASSWORD_NEW", DbType.String, pwdNew);
            Db.AddInParameter(cmd, "LOGIN_NAME", DbType.String, loginName);
            Db.AddInParameter(cmd, "LOGIN_PASSWORD", DbType.String, pwdOld);
            return Db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 激活码激活
        /// </summary>
        /// <param name="userGuid">用户ID</param>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <returns>激活结果 0 不成功，1 成功，2已经激活过</returns>  
        public int ActiveKey(string userGuid, string keyValue, string machineCode)
        {
            var getCountCmd = Db.GetSqlStringCommand(Context.SqlGetUserKeyCount);
            Db.AddInParameter(getCountCmd, "USER_GUID", DbType.String, userGuid);
            Db.AddInParameter(getCountCmd, "KEY_VALUE", DbType.String, keyValue);
            var result = Db.ExecuteScalar(getCountCmd);
            int count = result != DBNull.Value ? Convert.ToInt32(result) : 0;
            //找不到用户对应的key
            if (count == 0)
                return 0;

            var activatedCmd = Db.GetSqlStringCommand(Context.SqlGetActivatedUserKeyCount);
            Db.AddInParameter(activatedCmd, "USER_GUID", DbType.String, userGuid);
            Db.AddInParameter(activatedCmd, "KEY_VALUE", DbType.String, keyValue);
            Db.AddInParameter(activatedCmd, "MACHINE_CODE", DbType.String, machineCode);
            var result2 = Db.ExecuteScalar(activatedCmd);
            int count2 = result2 != DBNull.Value ? Convert.ToInt32(result2) : 0;
            //是否已经激活
            if (count2 > 0)
                return 2;
            //激活
            var updateCmd = Db.GetSqlStringCommand(Context.SqlActivate);
            Db.AddInParameter(updateCmd, "MACHINE_CODE", DbType.String, machineCode);
            Db.AddInParameter(updateCmd, "KEY_VALUE", DbType.String, keyValue);
            return Db.ExecuteNonQuery(updateCmd);
        }


        /// <summary>
        /// 保存报文相关信息
        /// </summary>
        /// <param name="msg">报文信息</param>
        public void SaveMessageInfo(CusDataMsg msg)
        {
            var cmd = Db.GetSqlStringCommand(Context.SqlSaveMsgInfo);
            Db.AddInParameter(cmd, "GUID", DbType.String, msg.MsgGuid);
            Db.AddInParameter(cmd, "KEY_VALUE", DbType.String, msg.KeyValue);
            Db.AddInParameter(cmd, "MACHINE_CODE", DbType.String, msg.MachineCode);
            Db.AddInParameter(cmd, "MESSAGE", DbType.Xml, msg.MessageXml);
            Db.AddInParameter(cmd, "DECL_TIME", DbType.DateTime, msg.DeclTime);
            Db.AddInParameter(cmd, "DECL_DATE", DbType.Date, msg.DeclTime.Date);
            Db.AddInParameter(cmd, "TCS_MSGID", DbType.String, msg.MessageId);
            Db.AddInParameter(cmd, "TCS_TASKID", DbType.String, msg.TaskId);
            Db.AddInParameter(cmd, "TCS_DOCUMENT_NO", DbType.String, msg.TcsDocumentNo);
            Db.AddInParameter(cmd, "CURRENT_ID", DbType.String, msg.CurrentId);
            Db.AddInParameter(cmd, "TCS_MESSAGE", DbType.Xml, msg.TcsMessageXml);
            Db.AddInParameter(cmd, "SEND_TIME", DbType.DateTime, msg.Sendtime);
            Db.AddInParameter(cmd, "CUS_CIQ_NO", DbType.String, msg.CusCiqNo.CusCiqNo);
            Db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 获取保存报文信息
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <returns>报文信息</returns>
        public CusDeclDataMsg GetMessageSavedById(string taskId)
        {
            IRowMapper<CusDeclDataMsg> mapper = MapBuilder<CusDeclDataMsg>.MapAllProperties()
               .Map(g => g.TaskId).ToColumn("TCS_TASKID")
               .Map(g => g.MessageXml).ToColumn("MESSAGE")
               .Map(g => g.CusCiqNo).ToColumn("CUS_CIQ_NO")
               .Map(g => g.DeclTime).ToColumn("DECL_TIME")
               .Build();
            return Db.ExecuteSqlStringAccessor<CusDeclDataMsg>(Context.SqlGetMsgInfo, mapper)
                .Where(e => e.TaskId == taskId).FirstOrDefault();

        }

        /// <summary>
        /// 下载报关回执
        /// </summary>        
        /// <param name="taskId">任务ID</param>
        /// <returns>回执内容</returns>
        public IEnumerable<CusReturnInfo2> GetReceiveMsgRep(string taskId)
        {
            IRowMapper<CusReturnInfo2> mapper = MapBuilder<CusReturnInfo2>.MapAllProperties()
               .Map(g => g.TaskId).ToColumn("TASK_ID")
               .Map(g => g.ReturnType).ToColumn("RETURN_TYPE")
               .Map(g => g.ReturnCode).ToColumn("RETURN_CODE")
               .Map(g => g.ReturnInfo).ToColumn("RETURN_INFO")
               .Map(g => g.CusCiqNo).ToColumn("CUSTOMS_CIQ_NO")
               .Map(g => g.Status).ToColumn("STATUS")
               .Map(g => g.EntryNo).ToColumn("ENTRY_NO")
               .Map(g => g.EportNo).ToColumn("EPORT_NO")
               .Build();
            return Db.ExecuteSqlStringAccessor<CusReturnInfo2>(Context.SqlGetCusReturn, mapper)
                .Where(e => e.TaskId == taskId);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log">日志信息</param>
        public void LoggingInfo(LogInfo log)
        {
            var cmd = Db.GetSqlStringCommand(Context.SqlInsertLogInfo);
            Db.AddInParameter(cmd, "LOG_TYPE", DbType.String, log.LogType);
            Db.AddInParameter(cmd, "TITLE", DbType.String, log.Title);
            Db.AddInParameter(cmd, "LOG_INFO", DbType.String, log.LogContent);
            Db.AddInParameter(cmd, "LOG_DATE", DbType.String, log.LogTime);
            Db.AddInParameter(cmd, "EVENT_ID", DbType.Int32, log.EventId);
            Db.AddInParameter(cmd, "SOURCE", DbType.String, log.Source);
            Db.AddInParameter(cmd, "MESSAGE", DbType.Xml, log.Message);
            Db.ExecuteNonQuery(cmd);
        }
    }
}
