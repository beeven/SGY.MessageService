using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using GZCustoms.Application.SGY.MessageService.SingleWindow.Interface.Entities;

namespace GZCustoms.Application.SGY.MessageService.SingleWindow.Interface
{
	[ServiceContract(Name = "MessageServiceWCF", Namespace = "http://sgy.gzcustoms.gov.cn/Contracts")]
	public interface IMessageService
	{
		/// <summary>
		/// 获取关检关联号
		/// </summary>
		/// <param name="ieFlag">进出口标识，1为进口，0为出口</param>
		/// <param name="locationCode">现场代码（4位）</param>
		/// <returns></returns>
		[OperationContract(Name = "GetCusCiqNo")]
		string GetCusCiqNo(string ieFlag, string locationCode);


		/// <summary>
		/// 上传报文到QP
		/// </summary>
		/// <param name="cusCisNo">关捡关联号</param>
		/// <param name="msgXml">报文</param>
		/// <returns></returns>
		[OperationContract(Name ="PostMessage")]
		MsgReceipt PostMessage(string cusCiqNo, string msgXml);



	}
}
