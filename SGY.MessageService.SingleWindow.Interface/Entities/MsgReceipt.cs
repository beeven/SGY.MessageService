using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZCustoms.Application.SGY.MessageService.SingleWindow.Interface.Entities
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
    }
}
