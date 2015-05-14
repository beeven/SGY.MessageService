using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GZCustoms.Application.SGY.MessageService;
using GZCustoms.Application.SGY.SingleWindow.MessageService.Entities;

namespace GZCustoms.Application.SGY.SingleWindow.MessageService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SingleWindowMessageService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SingleWindowMessageService.svc or SingleWindowMessageService.svc.cs at the Solution Explorer and start debugging.
    public class SingleWindowMessageService : IMessageService
    {
        SingleWindowMessageServiceHelper helper = new SingleWindowMessageServiceHelper();

        /// <summary>
        /// 获取关检关联号
        /// </summary>
        /// <param name="ieFlag">进出口标识，1为进口，0为出口</param>
        /// <param name="locationCode">现场代码（4位）</param>
        /// <returns>关检关联号（16位）</returns>
        public string GetCusCiqNo(String ieFlag, String locationCode)
        {
            return helper.GetCusCiqNo(ieFlag, locationCode);
        }

        /// <summary>
        /// 上载报文（到QP）
        /// </summary>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="msgXml">报文内容</param>
        /// <returns>上传回执</returns>
        public MsgReceipt PostMessage(string cusCiqNo, string msgXml)
        {
            return (MsgReceipt)helper.PostMessage(cusCiqNo, msgXml);
        }

        public string Hello()
        {
            return "World";
        }
    }
}
