// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Data
// FileName : OracleMessageDataHelper.cs
// Remark   : 报文数据操作类（Oracle）
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using GZCustoms.Application.SGY.Data.Interface;

namespace GZCustoms.Application.SGY.Data.DataHelper
{
    public class OracleMessageDataHelper
    {
        private Database Db { get; set; }

        public OracleMessageDataHelper()
        {
            Db = DatabaseFactory.GetDefaultDatabase(DatabaseType.Oracle);
        }
    }
}
