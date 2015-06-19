using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GZCustoms.Application.SGY.MessageService.Interface;

namespace GZCustoms.Application.SGY.SingleWindow.MessageService.Entities
{
	/// <summary>
	/// 回执信息
	/// </summary>
	[DataContract]
	public class CusReturn
	{
		/// <summary>
		/// TaskId任务编号
		/// </summary>
        [DataMember]
		public string TaskId { get; set; }
        
        /// <summary>
        /// 返回类型 TCS,QP
        /// </summary>
        [DataMember]
        public string ReturnType { get; set; }
        /// <summary>
        /// 返回代码
        /// </summary>
        [DataMember]
        public string ReturnCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        [DataMember]
        public string ReturnInfo { get; set; }


        /// <summary>
        /// 报关单号
        /// </summary>
        [DataMember]
        public string EntryNo { get; set; }

        /// <summary>
        /// 平台编号，没有预录入号的时候用
        /// </summary>
        [DataMember]
        public string EportNo { get; set; }

        /// <summary>
        /// 消息编号
        /// </summary>
        [DataMember]
        public string MessageId { get; set; }

        /// <summary>
        /// 消息生成时间
        /// </summary>
        [DataMember]
        public string DateCreated { get; set; }


        public static explicit operator CusReturn(CusReturnInfo2 info)
        {
            return new CusReturn()
            {
                TaskId = info.TaskId,
                ReturnType = info.ReturnType,
                ReturnCode = info.ReturnCode,
                ReturnInfo = info.ReturnInfo,
                EntryNo = info.EntryNo,
                EportNo = info.EportNo,
                MessageId = info.MessageId,
                DateCreated = info.DateCreated.ToUniversalTime().ToString("o")
            };
            
        }

	}
}
