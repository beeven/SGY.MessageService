// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Data
// FileName : DatabaseFactory.cs
// Remark   : 数据库对象工厂类
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
using GZCustoms.Application.SGY.Data.Config;

namespace GZCustoms.Application.SGY.Data
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DatabaseType
    {
        SqlServer,
        Oracle
    }

    ///<summary>
    /// 数据库对象工厂
    ///</summary>
    public static class DatabaseFactory
    {
        /// <summary>
        /// 获得默认数据库对象
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>数据库对象</returns>
        public static Database GetDefaultDatabase(DatabaseType dbType)
        {
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    return EnterpriseLibraryContainer.Current.GetInstance<Database>(Context.DefaultSqlConnName);
                case DatabaseType.Oracle:
                    return EnterpriseLibraryContainer.Current.GetInstance<Database>(Context.DefaultOracleConnName);
                default:
                    throw new Exception(Context.ErrDbTypeNull);
             }                   
        }

        /// <summary>
        /// 获得暂存数据库对象
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>暂存数据库对象</returns>
        public static Database GetEPortDatabase(DatabaseType dbType)
        {
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    return EnterpriseLibraryContainer.Current.GetInstance<Database>(Context.EPortSqlConnName);
                case DatabaseType.Oracle:
                    return EnterpriseLibraryContainer.Current.GetInstance<Database>(Context.EportOracleConnName);
                default:
                    throw new Exception(Context.ErrDbTypeNull);
            }        
        }
            
    }
}
