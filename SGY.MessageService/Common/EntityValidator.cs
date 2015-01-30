// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.MessageService
// FileName : EntityValidator.cs
// Remark   : 验证实体数据属性是否合法
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace GZCustoms.Application.SGY.MessageService.Common
{
    internal class EntityValidator <T>
    {
        /// <summary>
        /// 验证实体类数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>是否合法</returns>
        internal bool Validate(T entity)
        {
             Validator<T> validator = ValidationFactory.CreateValidator<T>();
             ValidationResults results = validator.Validate(entity);
             return results.IsValid;
        }
    }
}
