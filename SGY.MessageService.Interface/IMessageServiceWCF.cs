// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.MessageService.Interface
// FileName : IMessageServiceWCF.cs
// Remark   : WCF服务接口
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace GZCustoms.Application.SGY.MessageService.Interface
{

    /// <summary>
    /// WCF服务接口
    /// </summary>
    [ServiceContract(Name = "MessageServiceWCF", Namespace = "http://sgy.gzcustoms.gov.cn/Contracts")]
    public interface IMessageServiceWCF
    {

        #region 上载报关单数据

        /// <summary>
        /// 上载报文（到QP）
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="cusCiqNo">关检关联号（可选），如果没有填空字符</param>
        /// <param name="msgXml">消息</param>
        /// <returns></returns>
        [OperationContract(Name = "SendMessage")]
        MesReceipt SendMessage(string keyValue, string machineCode, string cusCiqNo, string msgXml);
        #endregion

        #region 暂存相关

        /// <summary>
        /// 获取关检关联号
        /// </summary>
        /// <param name="ieFlag">进出口标识，1为进口，0为出口</param>
        /// <param name="locationCode">现场代码（4位）</param>
        /// <returns></returns>
        [OperationContract(Name = "GetCusCiqNo")]
        string GetCusCiqNo(string ieFlag, string locationCode);

        /// <summary>
        /// 下载报关数据
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [OperationContract(Name = "DownloadCustomsData")]
        string DownloadCustomsData(string keyValue, string machineCode, string cusCiqNo, string password);

          /// <summary>
        /// 下载报检数据
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>     
        [OperationContract(Name = "DownloadCiqData")]
        string DownloadCiqData(string keyValue, string machineCode, string cusCiqNo, string password);

          /// <summary>
        /// 上传报关和报检数据（暂存数据）
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="ieFlag">进出口标识，1为进口，0为出口</param>
        /// <param name="locationCode">现场代码（4位）</param>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="status">数据状态（0，暂存；1，报检；2，上载QP；3，申报）</param>
        /// <param name="cusMsgXml">报关数据报文</param>      
        /// <param name="ciqMsgXml">报检数据报文</param>
        /// <returns>返回实体消息</returns>        
        [OperationContract(Name = "UploadAllData")]
        SaveModel UploadAllData(string keyValue, string machineCode, string ieFlag, string locationCode, string cusCiqNo, int status, string cusMsgXml, string ciqMsgXml);
        
        /// <summary>
        /// 上传报关数据（暂存数据）
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="ieFlag">进出口标识，1为进口，0为出口</param>
        /// <param name="locationCode">现场代码（4位）</param>        
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="status">数据状态（0，暂存；1，报检；2，上载QP；3，申报）</param>
        /// <param name="msgXml">报关数据报文</param>       
        /// <returns>返回实体消息</returns>
        [OperationContract(Name = "UploadCustomsData")]
        SaveModel UploadCustomsData(string keyValue, string machineCode, string ieFlag, string locationCode, string cusCiqNo, int status, string msgXml);

        /// <summary>
        /// 获取服务器上单据的保存时间
        /// </summary>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <returns>返回暂存时间</returns>
        [OperationContract(Name = "GetSaveTime")]
        DateTime GetSaveTime(string cusCiqNo);       

        #endregion

        #region 用户管理

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="password">用户密码</param>
        /// <returns>用户信息实体</returns>
        [OperationContract(Name = "Login")]
        UserInfo Login(string userName, string password);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="pwdOld">旧密码</param>
        /// <param name="pwdNew">新密码</param>
        /// <returns>修改结果 0 不成功，1 成功</returns>
        [OperationContract(Name = "UpdatePassword")]
        int UpdatePassword(string userName, string pwdOld, string pwdNew);

        /// <summary>
        /// 激活码激活
        /// </summary>
        /// <param name="userGuid">用户ID</param>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <returns>激活结果 0 不成功，1 成功</returns>
        [OperationContract(Name = "ActiveKey")]
        int ActiveKey(string userGuid, string keyValue, string machineCode);

          /// <summary>
        /// 激活码激活
        /// </summary>
        /// <param name="loginName">登陆用户名</param>
        /// <param name="password">密码</param>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <returns>激活结果 0 不成功，1 成功,2 该激活码已成功激活过</returns>
        [OperationContract(Name = "ActiveKeyByLoginName")]
        int ActiveKeyByLoginName(string loginName, string password, string keyValue, string machineCode);

        #endregion

        #region 回执处理

        /// <summary>
        /// 下载报关回执
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="taskId">任务ID</param>
        /// <returns>回执内容</returns>
        [OperationContract(Name = "ReceiveMsgRep")]
        IEnumerable<CusReturnInfo> ReceiveMsgRep(string keyValue, string machineCode, string taskId);

        /// <summary>
        /// 下载报关回执2
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="taskId">任务ID</param>
        /// <returns>回执内容</returns>
        [OperationContract(Name = "ReceiveMsgRep2")]
        IEnumerable<CusReturnInfo2> ReceiveMsgRep2(string keyValue, string machineCode, string taskId);
        #endregion

        #region 下载已申报报关数据
        /// <summary>
        /// 下载已申报报关数据
        /// </summary>
        /// <param name="id">任务编号</param>
        /// <returns>报关数据</returns>
        [OperationContract(Name = "GetDeclCusData")]
        CusDeclDataMsg GetDeclCusData(string taskId);
          /// <summary>
        /// 下载已申报报关数据
        /// </summary>
        /// <param name="idList">任务编号列表</param>
        /// <returns>报关数据列表</returns>
        [OperationContract(Name = "GetDeclCusDataList")]
        IEnumerable<CusDeclDataMsg> GetDeclCusDataList(IEnumerable<string> taskIdList);
        #endregion


    } 

    
}
