// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Entity
// FileName : CusCiqNo.cs
// Remark   : 关检关联号实体类
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
    /// 关检关联号信息实体类
    /// </summary>
    public class CusCiqNoInfo
    {
        /// <summary>
        /// 进出口标识 0 进口，1 出口
        /// </summary>
        [NotNullValidator]      
        [StringLengthValidator(1)]
        [RegexValidator(@"[0-1]")]
        public string IeFlag { get; set; }

        /// <summary>
        /// 现场代码
        /// </summary>
        [NotNullValidator]
        [StringLengthValidator(4)]
        [RegexValidator(@"^\d{4}$")]
        public string LocationCode { get; set; }

        /// <summary>
        /// 关检关联号
        /// </summary>
        public string CusCiqNo{get;set;}
    }
}
