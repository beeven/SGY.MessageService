using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GZCustoms.Application.SGY.Data;
using GZCustoms.Application.SGY.Data.Interface;
using GZCustoms.Application.SGY.Entity;
using GZCustoms.Application.SGY.Logging;
using GZCustoms.Application.SGY.MessageService.Common;
using GZCustoms.Application.SGY.MessageService.Config;
using GZCustoms.Application.SGY.MessageService.Interface;

namespace GZCustoms.Application.SGY.MessageService
{
    public class SingleWindowMessageServiceHelper
    {
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

        public MesReceipt PostMessage(string cusCiqNo, string msgXml)
        {
            var receipt = new MesReceipt();
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                CusCiqNoInfo cusCiqNoInfo = new CusCiqNoInfo { CusCiqNo = cusCiqNo };

                var cusDataMsg = new CusDataMsg { KeyValue = "SingleWindow", MachineCode = "SingleWindow", MessageXml = msgXml, CusCiqNo = cusCiqNoInfo };
                IMessageDataHelper dataHelper = DataHelperFactory.GetMessageDataHelper();
                //检查激活码
                //if (!Utility.CheckKey(dataHelper.GetKeyInfo(keyValue, machineCode)))
                //{
                //    receipt.Status = "003";
                //    receipt.Message = GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId);
                //    return receipt;
                //}
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
                    "SingleWindow", "SingleWindow", cusCiqNo), Context.SendMessageEventId, "SendMessage", "SingleWindow");
                return receipt;
            }
            catch (Exception ex)
            {
                receipt.Status = "001";
                receipt.Message = GetErrInfo(Context.ErrSendMessage, Context.SendMessageEventId) +"\nMessage:"+ ex.Message + "\nStackTrace:" + ex.StackTrace;
                logHelper.LogErrInfo(ex.Message, Context.SendMessageEventId, "SendMessage", "SingleWindow", msgXml);
                return receipt;
            }
        }


        public IEnumerable<CusReturnInfo2> ReceiveMsgRep(string taskId)
        {
            LogHelper logHelper = LogHelper.GetInstance();
            try
            {
                IMessageDataHelper dataHelper = DataHelperFactory.GetMessageDataHelper();
                var ret = dataHelper.GetReceiveMsgRep(taskId);
                return ret;
            }
            catch (Exception ex)
            {
                logHelper.LogErrInfo(ex.Message, Context.GetSaveTimeEventId, "GetSaveTime", "SingleWindow");
                return new List<CusReturnInfo2>() { new CusReturnInfo2() { ReturnInfo = GetErrInfo(Context.ErrKeyValueInvalid, Context.ErrKeyValueInvalidId) } };
            }
        }


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
