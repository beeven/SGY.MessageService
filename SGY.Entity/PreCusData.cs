// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Entity
// FileName : PreCusData.cs
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
    public class PreCusData
    {       
        /// <summary>
        /// 关检关联号相关信息
        /// </summary>
        public CusCiqNoInfo CusCiqNoInfo { get; set; }

        /// <summary>
        /// 报关Xml
        /// </summary>
        [NotNullValidator]
        public string CusMsgXml { get; set; }

        /// <summary>
        /// 报检报文Xml
        /// </summary>
        public string CiqMsgXml { get; set; }

        /// <summary>
        /// 动态密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 暂存更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }             

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
    }
}
