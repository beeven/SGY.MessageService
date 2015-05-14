using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using GZCustoms.Application.SGY.SingleWindow.MessageService.Entities;

namespace GZCustoms.Application.SGY.SingleWindow.MessageService
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
        [WebGet(UriTemplate = "{ieFlag}/{locationCode}", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
		string GetCusCiqNo(string ieFlag, string locationCode);


		/// <summary>
		/// 上传报文到QP
		/// </summary>
		/// <param name="cusCisNo">关捡关联号</param>
		/// <param name="msgXml">报文</param>
		/// <returns></returns>
		[OperationContract(Name ="PostMessage")]
        [WebInvoke(UriTemplate ="/", RequestFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
		MsgReceipt PostMessage(string cusCiqNo, string msgXml);


        [OperationContract(Name = "Hello")]
        [WebGet(UriTemplate ="/", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        string Hello();

	}
}
