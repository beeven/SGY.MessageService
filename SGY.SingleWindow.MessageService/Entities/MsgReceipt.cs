using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GZCustoms.Application.SGY.MessageService.Interface;

namespace GZCustoms.Application.SGY.SingleWindow.MessageService.Entities
{
	[DataContract]
	public class MsgReceipt
	{
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        [DataMember]
        public string TaskId { get; set; }

        /// <summary>
        /// 接收日期
        /// </summary>
        [DataMember]
        public string DateReceived { get; set; }

        /// <summary>
        /// 说明信息
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        public static explicit operator MsgReceipt(MesReceipt rec)
        {
            MsgReceipt result =  new MsgReceipt()
            {
                Status = rec.Status,
                TaskId = rec.MessagID,
                Message = rec.Message
            };
            DateTime d;
            if(DateTime.TryParseExact(rec.RDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture,DateTimeStyles.AssumeLocal,out d))
            {
                result.DateReceived = d.ToString("o");
            }
            else
            {
                result.DateReceived = DateTime.Now.ToString("o");
            }
            return result;

        }
    }
}
