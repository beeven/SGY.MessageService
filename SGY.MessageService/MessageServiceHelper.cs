// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.MessageService
// FileName : MessageServiceHelper.cs
// Remark   : 消息服务操作类
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using GZCustoms.Application.SGY.Entity;
using GZCustoms.Application.SGY.MessageService.Config;
using GZCustoms.Application.SGY.Data;
using GZCustoms.Application.SGY.Data.Interface;
using GZCustoms.Application.SGY.MessageService.Interface;
using GZCustoms.Application.SGY.MessageService.Common;
using GZCustoms.Application.SGY.Logging;

namespace GZCustoms.Application.SGY.MessageService
{
    public class MessageServiceHelper
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
        public MesReceipt SendMessage(string keyValue, string machineCode, string cusCiqNo, string msgXml)
        {
            var receipt = new MesReceipt();
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                CusCiqNoInfo cusCiqNoInfo = new CusCiqNoInfo { CusCiqNo = cusCiqNo };
              
                var cusDataMsg = new CusDataMsg { KeyValue = keyValue, MachineCode = machineCode, MessageXml = msgXml, CusCiqNo = cusCiqNoInfo };
                IMessageDataHelper dataHelper = DataHelperFactory.GetMessageDataHelper();
                //检查激活码
                if (!Utility.CheckKey(dataHelper.GetKeyInfo(keyValue, machineCode)))
                {
                    receipt.Status = "003";
                    receipt.Message = GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId);                
                    return receipt;
                }
                cusDataMsg = Utility.FormatCusDataMsg(cusDataMsg, dataHelper.GetMaxTcsCurrentId(), ConfigInfo.TscIdHead, ConfigInfo.DocumentNo);
                receipt.RDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                DeclHelper declHelper = new DeclHelper();
                DeclEnvelopHead msgHeader = declHelper.GetEnvelopHeader(msgXml);
                #region 生成TCS报文
                XDocEntity tmpDoc = ConfigInfo.GetTemplateDocEntity();                
                TcsHelper tcsHelper = new TcsHelper();  
                //保存报文
                XDocument tcsDoc = tcsHelper.GenerateTcsXDoc(tmpDoc, cusDataMsg);               
                tcsDoc.Save(GetFileName(ConfigInfo.Path));              
                #endregion
                #region 报文落地数据库
                cusDataMsg.TcsMessageXml = tcsDoc.ToString();
                cusDataMsg.DeclTime = DateTime.Now;
                cusDataMsg.MsgGuid = msgHeader.MsgGuid;
                cusDataMsg.Sendtime = msgHeader.SendTime;
                dataHelper.SaveMessageInfo(cusDataMsg);
                #endregion
  
                receipt.Status = "000";
                receipt.Message = string.Empty;
                receipt.MessagID = cusDataMsg.TaskId;
              
                //记录操作日志
                logHelper.LogOperation(string.Format("SendMessage 上载报文（到QP）,KeyValue:{0},Machinecode:{1},CusCiqNo:{2}",
                    keyValue, machineCode, cusCiqNo), Context.SendMessageEventId, "SendMessage", keyValue);
                return receipt;
            }
            catch (Exception ex)
            {
                receipt.Status = "001";
                receipt.Message = GetErrInfo(Context.ErrSendMessage, Context.SendMessageEventId);
                logHelper.LogErrInfo(ex.Message, Context.SendMessageEventId, "SendMessage", keyValue, msgXml);
                return receipt;
            }            
        }

        #endregion

        #region 暂存相关

        /// <summary>
        /// 获取关检关联号（16位）
        /// </summary>
        /// <param name="ieFlag">进出口标识，1为进口，0为出口</param>
        /// <param name="locationCode">现场代码（4位）</param>
        /// <returns>关检关联号（16位）</returns>       
        public string GetCusCiqNo(string ieFlag, string locationCode)
        {
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                var cusCiqNo = new CusCiqNoInfo() { IeFlag = ieFlag, LocationCode = locationCode, CusCiqNo = string.Empty };
                if (!new EntityValidator<CusCiqNoInfo>().Validate(cusCiqNo))
                    throw new Exception(Context.ErrIeFlagOrLocalCodeInValid);
                IPreserveDataHelper dataHelper = DataHelperFactory.GetPreserveDataHelper();
                int index = dataHelper.GetCusCiqIndex(cusCiqNo.IeFlag, cusCiqNo.LocationCode);
                string indexStr = index.ToString();               
                cusCiqNo.CusCiqNo = string.Format("{0}{1}{2}{3}", ieFlag, DateTime.Now.ToString("yyMMdd"), locationCode, indexStr.PadLeft(5, '0'));
                //记录操作日志
                logHelper.LogOperation(string.Format("GetCusCiqNo 获取关检关联号,IeFlag:{0},LocationCode:{1},CusCiqNo:{2}",
                    ieFlag, locationCode, cusCiqNo.CusCiqNo), Context.GetCusCiqNoEventId, "GetCusCiqNo");
                if (ConfigInfo.DeclType == 0 && ieFlag == "1")
                    throw new Exception("目前只允许出口业务类型");
                if (ConfigInfo.DeclType == 1 && ieFlag == "0")
                    throw new Exception("目前只允许进口业务类型");                

                return cusCiqNo.CusCiqNo;
            }
            catch (Exception ex)
            {
                logHelper.LogErrInfo(ex.Message, Context.GetCusCiqNoEventId, "GetCusCiqNo");
                return GetErrInfo(Context.ErrGetCusCiqNo, Context.GetCusCiqNoEventId);
            }                       
        }

        /// <summary>
        /// 下载报关数据
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>        
        public string DownloadCustomsData(string keyValue, string machineCode, string cusCiqNo, string password)
        {
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                if (string.IsNullOrEmpty(cusCiqNo))
                    throw new Exception(Context.ErrCusCiqNoInvalid);
                if (string.IsNullOrEmpty(password))
                    throw new Exception(Context.ErrRPwdInvalid);
                IMessageDataHelper msgDataHelper = DataHelperFactory.GetMessageDataHelper();
                //检查激活码是否有效
                if (!Utility.CheckKey(msgDataHelper.GetKeyInfo(keyValue, machineCode)))
                    return GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId); ;
                IPreserveDataHelper preDataHelper = DataHelperFactory.GetPreserveDataHelper();
                return preDataHelper.DownloadCusData(cusCiqNo, password);
            }
            catch (Exception ex)
            {
                logHelper.LogErrInfo(ex.Message, Context.DownloadCustomsDataEventId, "DownloadCustomsData", keyValue);
                return GetErrInfo(Context.ErrDownloadCustomsData, Context.DownloadCustomsDataEventId);
            }
        }

        /// <summary>
        /// 下载报检数据
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>        
        public string DownloadCiqData(string keyValue, string machineCode, string cusCiqNo, string password)
        {
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                if (string.IsNullOrEmpty(cusCiqNo))
                    throw new Exception(Context.ErrCusCiqNoInvalid);
                if (string.IsNullOrEmpty(password))
                    throw new Exception(Context.ErrRPwdInvalid);
                IMessageDataHelper msgDataHelper = DataHelperFactory.GetMessageDataHelper();
                //检查激活码是否有效
                if (!Utility.CheckKey(msgDataHelper.GetKeyInfo(keyValue, machineCode)))
                    return GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId); ;
                IPreserveDataHelper preDataHelper = DataHelperFactory.GetPreserveDataHelper();
                return preDataHelper.DownloadCiqData(cusCiqNo, password);
            }
            catch (Exception ex)
            {
                logHelper.LogErrInfo(ex.Message, Context.DownloadCiqDataEventId, "DownloadCiqData", keyValue);
                return GetErrInfo(Context.ErrDownloadCiqData, Context.DownloadCiqDataEventId);
            }
        }

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
        public SaveModel UploadAllData(string keyValue, 
            string machineCode, string ieFlag, string locationCode, string cusCiqNo, int status, string cusMsgXml, string ciqMsgXml)
        {
            CusCiqNoInfo cusCiqNoInfo = new CusCiqNoInfo { CusCiqNo = cusCiqNo, IeFlag = ieFlag, LocationCode = locationCode };
            if (!new EntityValidator<CusCiqNoInfo>().Validate(cusCiqNoInfo))
                throw new Exception(Context.ErrIeFlagOrLocalCodeInValid);
            SaveModel saveModel = new SaveModel() { CusCiqNo = cusCiqNo, Password = string.Empty, IsSuccess = false };
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                IMessageDataHelper msgDataHelper = DataHelperFactory.GetMessageDataHelper();
                //检查激活码是否有效
                if (!Utility.CheckKey(msgDataHelper.GetKeyInfo(keyValue, machineCode)))
                {
                    saveModel.Message = GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId); ;
                    return saveModel;
                }

                PreCusData preData = new PreCusData
                {
                    CusCiqNoInfo = cusCiqNoInfo,
                    KeyValue = keyValue,
                    MachineCode = machineCode,
                    CusMsgXml = cusMsgXml,
                    CiqMsgXml = ciqMsgXml,
                    UpdateTime = DateTime.Now,
                    Password = Utility.GetRandomNumString(6)                    
                };
                IPreserveDataHelper preDataHelper = DataHelperFactory.GetPreserveDataHelper();
                //如果暂存成功
                if (preDataHelper.UploadPreCusData(preData) == 1)
                {
                    preData.Password = preDataHelper.GetPasswordByCusCiqNo(cusCiqNo);
                    saveModel.IsSuccess = true;
                    saveModel.Message = string.Empty;
                    saveModel.Password = preData.Password;
                }
                if (status > 0)
                {
                    DeclCusData declData = new DeclCusData
                    {
                        CusCiqNoInfo = preData.CusCiqNoInfo,
                        CiqMsgXml = preData.CiqMsgXml,
                        CusMsgXml = preData.CusMsgXml, 
                        Status = (DeclCusData.SaveType)status                     
                    };
                    preDataHelper.UploadDeclData(declData);
                }
            }
            catch (Exception ex)
            {
                saveModel.Message = GetErrInfo(Context.ErrUploadAllData, Context.UploadAllDataEventId);
                logHelper.LogErrInfo(ex.Message, Context.UploadAllDataEventId, "UploadAllData", keyValue);                
            }
            return saveModel;
        }

        /// <summary>
        /// 上传报关数据（暂存数据）
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="ieFlag">进出口标识，1为进口，0为出口</param>
        /// <param name="locationCode">现场代码（4位）</param>
        /// <param name="status">数据状态（0，暂存；1，报检；2，上载QP；3，申报）</param>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="cusMsgXml">报关数据报文</param>
        /// <returns></returns>
        public SaveModel UploadCusTomsData(string keyValue, 
            string machineCode, string ieFlag, string locationCode, string cusCiqNo, int status, string cusMsgXml)
        {
            return UploadAllData(keyValue, machineCode, ieFlag, locationCode, cusCiqNo, status, cusMsgXml, string.Empty);
        }

        /// <summary>
        /// 获取服务器上单据的保存时间
        /// </summary>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <returns>返回暂存时间</returns>     
        public DateTime GetSaveTime(string cusCiqNo)
        {
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                if (string.IsNullOrEmpty(cusCiqNo))
                    return DateTime.MinValue;
                IPreserveDataHelper preDataHelper = DataHelperFactory.GetPreserveDataHelper();
                return preDataHelper.GetSaveTime(cusCiqNo);
            }
            catch (Exception ex)
            {
                logHelper.LogErrInfo(ex.Message, Context.GetSaveTimeEventId, "GetSaveTime", cusCiqNo);
                return DateTime.MinValue;
            }
        }
        #endregion

        #region 用户管理
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="loginName">用户名称</param>
        /// <param name="password">用户密码</param>
        /// <returns>用户信息实体</returns>        
        public UserInfo Login(string loginName, string password)
        {
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                IMessageDataHelper dataHelper = DataHelperFactory.GetMessageDataHelper();
                return dataHelper.GetLoginUserInfo(loginName, password);
            }
            catch (Exception ex)
            {
                logHelper.LogErrInfo(ex.Message, Context.LoginEventId, "Login", loginName);
                return new UserInfo();
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginName">登陆名称</param>
        /// <param name="pwdOld">旧密码</param>
        /// <param name="pwdNew">新密码</param>
        /// <returns>修改结果 0 不成功，1 成功</returns>        
        public int UpdatePassword(string loginName, string pwdOld, string pwdNew)
        {
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                IMessageDataHelper dataHelper = DataHelperFactory.GetMessageDataHelper();
                return dataHelper.UpdateUserPassword(loginName, pwdOld, pwdNew);
            }
            catch (Exception ex)
            {
                logHelper.LogErrInfo(ex.Message, Context.UpdatePasswordEventId, "UpdatePassword", loginName);
                return 0;
            }
        }

        /// <summary>
        /// 激活码激活
        /// </summary>
        /// <param name="userGuid">用户ID</param>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <returns>激活结果 0 不成功，1 成功，2 该激活码已激活过</returns>  
        public int ActiveKey(string userGuid, string keyValue, string machineCode)
        {
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                IMessageDataHelper dataHelper = DataHelperFactory.GetMessageDataHelper();
                return dataHelper.ActiveKey(userGuid, keyValue, machineCode);
            }
            catch (Exception ex)
            {
                logHelper.LogErrInfo(ex.Message, Context.ActiveKeyEventId, "ActiveKey", keyValue);
                return 0;
            }
        }

        /// <summary>
        /// 激活码激活
        /// </summary>
        /// <param name="loginName">登陆用户名</param>
        /// <param name="password">用户密码</param>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <returns>激活结果 0 不成功，1 成功</returns>
        public int ActiveKey(string loginName, string password, string keyValue, string machineCode)
        {
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                IMessageDataHelper dataHelper = DataHelperFactory.GetMessageDataHelper();
                UserInfo userInfo = dataHelper.GetLoginUserInfo(loginName, password);
                if (string.IsNullOrEmpty(userInfo.Guid))
                    return 0;
                return dataHelper.ActiveKey(userInfo.Guid, keyValue, machineCode);
            }
            catch (Exception ex)
            {
                logHelper.LogErrInfo(ex.Message, Context.ActiveKeyEventId, "ActiveKey", keyValue);
                return 0;
            }
        }

        #endregion

        #region 回执处理

        /// <summary>
        /// 下载报关回执
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="taskId">任务ID</param>
        /// <returns>回执内容</returns>
        public IEnumerable<CusReturnInfo> ReceiveMsgRep(string keyValue, string machineCode, string taskId)
        {
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                IMessageDataHelper dataHelper = DataHelperFactory.GetMessageDataHelper();
                //检查激活码是否有效
                if (!Utility.CheckKey(dataHelper.GetKeyInfo(keyValue, machineCode)))
                {
                    return new List<CusReturnInfo>() {  new CusReturnInfo() { ReturnInfo = GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId) } };                                 
                }
                var ret = dataHelper.GetReceiveMsgRep(taskId);
                return ret.Select(x => { return (CusReturnInfo)x; }).AsEnumerable();
            }
            catch (Exception ex)
            {
                logHelper.LogErrInfo(ex.Message, Context.GetSaveTimeEventId, "GetSaveTime", keyValue);
                return new List<CusReturnInfo>() { new CusReturnInfo() { ReturnInfo = GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId) } };                                 
            }
        }



        /// <summary>
        /// 下载报关回执
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="taskId">任务ID</param>
        /// <returns>回执内容</returns>
        public IEnumerable<CusReturnInfo2> ReceiveMsgRep2(string keyValue, string machineCode, string taskId)
        {
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                IMessageDataHelper dataHelper = DataHelperFactory.GetMessageDataHelper();
                //检查激活码是否有效
                if (!Utility.CheckKey(dataHelper.GetKeyInfo(keyValue, machineCode)))
                {
                    return new List<CusReturnInfo2>() { new CusReturnInfo2() { ReturnInfo = GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId) } };
                }
                return dataHelper.GetReceiveMsgRep(taskId);
            }
            catch (Exception ex)
            {
                logHelper.LogErrInfo(ex.Message, Context.GetSaveTimeEventId, "GetSaveTime", keyValue);
                return new List<CusReturnInfo2>() { new CusReturnInfo2() { ReturnInfo = GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId) } };
            }
        }

        #endregion

        #region 下载已申报报关数据
        /// <summary>
        /// 下载已申报报关数据
        /// </summary>
        /// <param name="id">任务编号</param>
        /// <returns>报关数据</returns>
        public CusDeclDataMsg GetDeclCusData(string id)
        {
            IMessageDataHelper helper = DataHelperFactory.GetMessageDataHelper();
            return helper.GetMessageSavedById(id);
        }

        /// <summary>
        /// 下载已申报报关数据
        /// </summary>
        /// <param name="idList">任务编号列表</param>
        /// <returns>报关数据列表</returns>
        public IEnumerable<CusDeclDataMsg> GetDeclCusData(IEnumerable<string> idList)
        {
            IMessageDataHelper helper = DataHelperFactory.GetMessageDataHelper();
            List<CusDeclDataMsg> list = new List<CusDeclDataMsg>();
            foreach (var id in idList)
            {
                list.Add(helper.GetMessageSavedById(id));
            }
            return list;
        }

        #endregion

        private string GetFileName(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();
            return string.Format(@"{0}\{1}_{2}.xml", dir.FullName, DateTime.Now.ToString("yyyyMMddHHmmss"), Utility.GetRandomNumString(3));
        }     

        private string GetErrInfo(string msg, int eventId)
        {
            return string.Format("{0}, 错误代码： {1}", msg, eventId.ToString());
        }

    }
}
