// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Data
// FileName : DataHelperFactory.cs
// Remark   : 常量
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GZCustoms.Application.SGY.Data.Interface;
using GZCustoms.Application.SGY.Data.Config;
using GZCustoms.Application.SGY.Data.DataHelper;

namespace GZCustoms.Application.SGY.Data
{
    /// <summary>
    /// 数据操作工厂类
    /// </summary>    
    public static class DataHelperFactory
    {
        public static IMessageDataHelper GetMessageDataHelper()
        {
            switch (Context.DefaultDbType)
            {
                case Context.SqlServerType:
                    return new SqlMessageDataHelper() as IMessageDataHelper;
                //case Context.OracleType:
                //    return new OracleMessageDataHelper() as IMessageDataHelper;
                default:
                    throw new Exception(Context.ErrUnSupportedDbType);
            }
        }

        public static IPreserveDataHelper GetPreserveDataHelper()
        {
            switch (Context.DefaultDbType)
            {
                case Context.SqlServerType:
                    return new SqlPreserveDataHelper() as IPreserveDataHelper;
                //case Context.OracleType:
                //    return new OraclePreserveDataHelper() as IPreserveDataHelper;
                default:
                    throw new Exception(Context.ErrUnSupportedDbType);
            }
        }

    }
}
