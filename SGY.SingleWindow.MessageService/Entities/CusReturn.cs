using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZCustoms.Application.SGY.SingleWindow.MessageService.Entities
{
	/// <summary>
	/// 回执信息
	/// </summary>
	[Serializable]
	public class CusReturn
	{
		/// <summary>
		/// TaskId任务编号
		/// </summary>
		public string TaskId { get; set; }
		/// <summary>
		/// 返回类型 TCS,QP
		/// </summary>
		public string ReturnType { get; set; }
		/// <summary>
		/// 返回代码
		/// </summary>
		public string ReturnCode { get; set; }
		/// <summary>
		/// 返回信息
		/// </summary>
		public string ReturnInfo { get; set; }
		/// <summary>
		/// 关检关联号
		/// </summary>
		public string CusCiqNo { get; set; }
		/// <summary>
		/// 状态信息
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// 报关单号
		/// </summary>
		public string EntryNo { get; set; }

		/// <summary>
		/// 平台编号，没有预录入号的时候用
		/// </summary>
		public string EportNo { get; set; }


	}
}
