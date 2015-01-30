// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Entity
// FileName : KeyInfo.cs
// Remark   : 激活码信息实体类
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
    /// 激活码信息
    /// </summary>
    public class KeyInfo
    {
        /// <summary>
        /// Guid
        /// </summary>
        [NotNullValidator]
        public string Guid { get; set; }
        /// <summary>
        /// 激活码名称
        /// </summary>        
        public string KeyName { get; set; }
        /// <summary>
        /// 激活码
        /// </summary>
        [NotNullValidator]
        public string KeyValue { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>       
        [NotNullValidator]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        [NotNullValidator]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 机器编码
        /// </summary>
        [NotNullValidator]
        public string MachineCode { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string EntName { get; set; }
    }
}
