// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Entity
// FileName : CusDataMsg.cs
// Remark   : 报关数据信息实体类
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace GZCustoms.Application.SGY.Entity
{
    /// <summary>
    /// 报关数据信息实体类
    /// </summary>
    public class CusDataMsg
    {
        /// <summary>
        /// 激活码
        /// </summary>
        [NotNullValidator]
        [StringLengthValidator(16)]
        [RegexValidator(@"^\d{16}$")]
        public string KeyValue { get; set; }

        /// <summary>
        /// 机器代码
        /// </summary>
        [NotNullValidator]
        public string MachineCode { get; set; }

        /// <summary>
        /// 报文Xml
        /// </summary>
        [NotNullValidator]
        public string MessageXml { get; set; }

        /// <summary>
        /// TCS报文Id
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 当前流水号Id
        /// </summary>
        public long CurrentId { get; set; }

        /// <summary>
        /// TCS报文TaskId
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// TCS报文TcsDocumentNo
        /// </summary>
        public string TcsDocumentNo { get; set; }

        /// <summary>
        /// TCS报文内容
        /// </summary>
        public string TcsMessageXml { get; set; }

        /// <summary>
        /// 上载时间
        /// </summary>
        public DateTime DeclTime { get; set; }

        /// <summary>
        /// 报文发送时间
        /// </summary>
        public DateTime Sendtime { get; set; }

        /// <summary>
        /// 报文Guid
        /// </summary>
        public string MsgGuid { get; set; }

        /// <summary>
        /// 关检关联号信息
        /// </summary>
        public CusCiqNoInfo CusCiqNo { get; set; }



    }
}
