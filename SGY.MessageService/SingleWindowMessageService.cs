using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using GZCustoms.Application.SGY.Data;
using GZCustoms.Application.SGY.Data.Interface;
using GZCustoms.Application.SGY.Entity;
using GZCustoms.Application.SGY.Logging;
using GZCustoms.Application.SGY.MessageService.Common;
using GZCustoms.Application.SGY.MessageService.Config;
using GZCustoms.Application.SGY.MessageService.SingleWindow.Interface;
using GZCustoms.Application.SGY.MessageService.SingleWindow.Interface.Entities;

namespace GZCustoms.Application.SGY.MessageService
{
    public class SingleWindowMessageService : IMessageService
    {
        public string GetCusCiqNo(string ieFlag, string locationCode)
        {
            throw new NotImplementedException();
        }

        public MsgReceipt PostMessage(string cusCiqNo, string msgXml)
        {
            var receipt = new MsgReceipt();
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                CusCiqNoInfo cusCiqNoInfo = new CusCiqNoInfo { CusCiqNo = cusCiqNo };

                var cusDataMsg = new CusDataMsg { MessageXml = msgXml, CusCiqNo = cusCiqNoInfo };
                IMessageDataHelper dataHelper = DataHelperFactory.GetMessageDataHelper();
                //检查激活码
                //if (!Utility.CheckKey(dataHelper.GetKeyInfo(keyValue, machineCode)))
                //{
                //    receipt.Status = "003";
                //    receipt.Message = GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId);
                //    return receipt;
                //}

                cusDataMsg = Utility.FormatCusDataMsg(cusDataMsg, dataHelper.GetMaxTcsCurrentId(), ConfigInfo.TscIdHead, ConfigInfo.DocumentNo);
                receipt.DateReceived = DateTime.Now.ToString("yyyyMMddHHmmss");
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
                logHelper.LogOperation(string.Format("PostMessage 上载报文（到QP）,CusCiqNo:{0}",
                    cusCiqNo), Context.SendMessageEventId, "PostMessage", "SingleWindow");
                return receipt;
            }
            catch (Exception ex)
            {
                receipt.Status = "001";
                receipt.Message = GetErrInfo(Context.ErrSendMessage, Context.SendMessageEventId);
                logHelper.LogErrInfo(ex.Message, Context.SendMessageEventId, "PostMessage", "SingleWindow", msgXml);
                return receipt;
            }
        }



        #region 回执处理





        /// <summary>
        /// 下载报关回执
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="taskId">任务ID</param>
        /// <returns>回执内容</returns>
        public IEnumerable<CusReturn> GetMsgRep(string keyValue, string machineCode, string taskId)
        {
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                IMessageDataHelper dataHelper = DataHelperFactory.GetMessageDataHelper();
                ////检查激活码是否有效
                //if (!Utility.CheckKey(dataHelper.GetKeyInfo(keyValue, machineCode)))
                //{
                //    return new List<CusReturn>() { new CusReturn() { ReturnInfo = GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId) } };
                //}
                return dataHelper.GetReceiveMsgRep(taskId).Select(x => new CusReturn()
                {
                    CusCiqNo = x.CusCiqNo,
                    EntryNo = x.EntryNo,
                    EportNo = x.EportNo,
                    ReturnCode = x.ReturnCode,
                    ReturnInfo = x.ReturnInfo,
                    ReturnType = x.ReturnType,
                    Status = x.Status,
                    TaskId = x.TaskId
                });
            }
            catch (Exception ex)
            {
                logHelper.LogErrInfo(ex.Message, Context.GetSaveTimeEventId, "GetSaveTime", keyValue);
                return new List<CusReturn>() { new CusReturn() { ReturnInfo = GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId) } };
            }
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
