using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GZCustoms.Application.SGY.MessageService.Interface;

namespace GZCustoms.Application.SGY.SingleWindow.MessageService.Entities
{
	[Serializable]
	public class MsgReceipt
	{
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public string MessagID { get; set; }

        /// <summary>
        /// 接收日期
        /// </summary>
        public string DateReceived { get; set; }

        /// <summary>
        /// 说明信息
        /// </summary>
        public string Message { get; set; }

        public static explicit operator MsgReceipt(MesReceipt rec)
        {
            MsgReceipt result =  new MsgReceipt()
            {
                Status = rec.Status,
                MessagID = rec.MessagID,
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
