// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Data
// FileName : IMessageDataHelper.cs
// Remark   : 报文相关数据操作接口类
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GZCustoms.Application.SGY.Entity;
using GZCustoms.Application.SGY.MessageService.Interface;

namespace GZCustoms.Application.SGY.Data.Interface
{
    public interface IMessageDataHelper
    {
        /// <summary>
        /// 获得激活码信息
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器编码</param>
        /// <returns>激活码信息</returns>
        KeyInfo GetKeyInfo(string keyValue, string machineCode);

        /// <summary>
        /// 获取TCS报文当前Id
        /// </summary>        
        /// <returns>返回Id</returns>
        long GetMaxTcsCurrentId();

        /// <summary>
        /// 保存报文相关信息
        /// </summary>
        /// <param name="msg">报文信息</param>
        void SaveMessageInfo(CusDataMsg msg);

        /// <summary>
        /// 获取保存报文信息
        /// </summary>
        /// <param name="taskId">TaskId</param>
        /// <returns>报文信息</returns>
        CusDeclDataMsg GetMessageSavedById(string taskId);

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="loginName">用户名称</param>
        /// <param name="password">密码</param>
        /// <returns>用户信息</returns>        
        UserInfo GetLoginUserInfo(string loginName, string password);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="loginName">用户登陆名称</param>
        /// <param name="pwdOld">旧密码</param>
        /// <param name="pwdNew">新密码</param>
        /// <returns>返回修改结果 1 表示成功，0 表示失败</returns>
        int UpdateUserPassword(string loginName, string pwdOld, string pwdNew);

        /// <summary>
        /// 激活码激活
        /// </summary>
        /// <param name="userGuid">用户ID</param>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <returns>激活结果 0 不成功，1 成功</returns>  
        int ActiveKey(string userGuid, string keyValue, string machineCode);

          /// <summary>
        /// 下载报关回执
        /// </summary>     
        /// <param name="taskId">任务ID</param>
        /// <returns>回执内容</returns>
        IEnumerable<CusReturnInfo2> GetReceiveMsgRep(string taskId);

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log">日志信息</param>
        void LoggingInfo(LogInfo log);


      
      
    }
}
