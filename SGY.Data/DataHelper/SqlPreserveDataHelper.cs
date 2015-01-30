// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Data
// FileName : SqlPreserveDataHelper.cs
// Remark   : 暂存数据操作类（SQLServer）
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using GZCustoms.Application.SGY.Data.Interface;
using GZCustoms.Application.SGY.Data.Config;
using GZCustoms.Application.SGY.Entity;

namespace GZCustoms.Application.SGY.Data.DataHelper
{
    public class SqlPreserveDataHelper : IPreserveDataHelper
    {
        private Database Db { get; set; }

        internal SqlPreserveDataHelper()
        {
            Db = DatabaseFactory.GetEPortDatabase(DatabaseType.SqlServer);
        }

        /// <summary>
        /// 获得现场记录编号
        /// </summary>
        /// <param name="ieFlag">进出口标识</param>
        /// <param name="locationCode">现场代码</param>
        /// <returns>编号</returns>
        public int GetCusCiqIndex(string ieFlag, string locationCode)
        {
            var cmd = Db.GetSqlStringCommand(Context.SqlGetDeclQueueIndex);
            Db.AddInParameter(cmd, "IE_FLAG", DbType.String, ieFlag);
            Db.AddInParameter(cmd, "CODE", DbType.String, locationCode);
            Db.AddInParameter(cmd, "CREATE_DATE", DbType.Date, DateTime.Now.Date);
            var result = Db.ExecuteScalar(cmd);
            //获取当前记录号
            int index = result != DBNull.Value ? Convert.ToInt32(result) : 0;
            index = index + 1;
            if (index == 1)
            {
                //插入记录号
                var cmdInsert = Db.GetSqlStringCommand(Context.SqlInsertDeclQueueIndex);
                Db.AddInParameter(cmdInsert, "IE_FLAG", DbType.String, ieFlag);
                Db.AddInParameter(cmdInsert, "CODE", DbType.String, locationCode);
                Db.AddInParameter(cmdInsert, "CREATE_DATE", DbType.Date, DateTime.Now.Date);
                Db.AddInParameter(cmdInsert, "INDEX", DbType.Int32, index);
                Db.ExecuteNonQuery(cmdInsert);
            }
            else
            {
                //更新记录号
                var cmdUpdate = Db.GetSqlStringCommand(Context.SqlUpdateDeclQueueIndex);
                Db.AddInParameter(cmdUpdate, "IE_FLAG", DbType.String, ieFlag);
                Db.AddInParameter(cmdUpdate, "CODE", DbType.String, locationCode);
                Db.AddInParameter(cmdUpdate, "CREATE_DATE", DbType.Date, DateTime.Now.Date);
                Db.AddInParameter(cmdUpdate, "INDEX", DbType.Int32, index);
                Db.ExecuteNonQuery(cmdUpdate);
            }
            return index;
        }

        /// <summary>
        /// 暂存报关数据
        /// </summary>
        /// <param name="cusData">报关数据</param>
        /// <returns>执行结果</returns>
        public int UploadPreCusData(PreCusData cusData)
        {
            var cmd = Db.GetSqlStringCommand(Context.SqlGetCountByCusCiqNo);
            Db.AddInParameter(cmd, "CUS_CIQ_NO", DbType.String, cusData.CusCiqNoInfo.CusCiqNo);
            var result = Db.ExecuteScalar(cmd);
            //获取当前记录号
            int count = result != DBNull.Value ? Convert.ToInt32(result) : 0;

            #region 不存在记录则插入新记录
            if (count == 0)
            {
                var insertCmd = Db.GetSqlStringCommand(Context.SqlInsertPreCusData);
                Db.AddInParameter(insertCmd, "CUS_CIQ_NO", DbType.String, cusData.CusCiqNoInfo.CusCiqNo);
                Db.AddInParameter(insertCmd, "IE_FLAG", DbType.String, cusData.CusCiqNoInfo.IeFlag);
                Db.AddInParameter(insertCmd, "CUS_MSG_XML", DbType.Xml, cusData.CusMsgXml);
                Db.AddInParameter(insertCmd, "CIQ_MSG_XML", DbType.Xml, cusData.CiqMsgXml);
                Db.AddInParameter(insertCmd, "R_PASSWORD", DbType.String, cusData.Password);
                Db.AddInParameter(insertCmd, "UPDATE_TIME", DbType.String, cusData.UpdateTime);
                Db.AddInParameter(insertCmd, "MACHINE_CODE", DbType.String, cusData.MachineCode);
                Db.AddInParameter(insertCmd, "KEY_VALUE", DbType.String, cusData.KeyValue);
                Db.AddInParameter(insertCmd, "LOCAL_CODE", DbType.String, cusData.CusCiqNoInfo.LocationCode);
                return Db.ExecuteNonQuery(insertCmd);
            }
            #endregion

            #region 存在则更新记录
            if (count == 1)
            {
                if (string.IsNullOrEmpty(cusData.CiqMsgXml))
                {
                    //报检数据为空，只更新报关数据
                    var updateCmd = Db.GetSqlStringCommand(Context.SqlUpdatePreCusData);
                    Db.AddInParameter(updateCmd, "CUS_CIQ_NO", DbType.String, cusData.CusCiqNoInfo.CusCiqNo);
                    Db.AddInParameter(updateCmd, "IE_FLAG", DbType.String, cusData.CusCiqNoInfo.IeFlag);
                    Db.AddInParameter(updateCmd, "CUS_MSG_XML", DbType.Xml, cusData.CusMsgXml);
                    //Db.AddInParameter(updateCmd, "CIQ_MSG_XML", DbType.Xml, cusData.CiqMsgXml);
                    //Db.AddInParameter(updateCmd, "R_PASSWORD", DbType.String, cusData.Password);
                    Db.AddInParameter(updateCmd, "UPDATE_TIME", DbType.String, cusData.UpdateTime);
                    Db.AddInParameter(updateCmd, "MACHINE_CODE", DbType.String, cusData.MachineCode);
                    Db.AddInParameter(updateCmd, "KEY_VALUE", DbType.String, cusData.KeyValue);
                    Db.AddInParameter(updateCmd, "LOCAL_CODE", DbType.String, cusData.CusCiqNoInfo.LocationCode);
                    return Db.ExecuteNonQuery(updateCmd);
                }
                else
                {
                    //同时更新报检和报关数据
                    var updateCmd = Db.GetSqlStringCommand(Context.SqlUpdatePreCusCiqData);
                    Db.AddInParameter(updateCmd, "CUS_CIQ_NO", DbType.String, cusData.CusCiqNoInfo.CusCiqNo);
                    Db.AddInParameter(updateCmd, "IE_FLAG", DbType.String, cusData.CusCiqNoInfo.IeFlag);
                    Db.AddInParameter(updateCmd, "CUS_MSG_XML", DbType.Xml, cusData.CusMsgXml);
                    Db.AddInParameter(updateCmd, "CIQ_MSG_XML", DbType.Xml, cusData.CiqMsgXml);
                    //Db.AddInParameter(updateCmd, "R_PASSWORD", DbType.String, cusData.Password);
                    Db.AddInParameter(updateCmd, "UPDATE_TIME", DbType.String, cusData.UpdateTime);
                    Db.AddInParameter(updateCmd, "MACHINE_CODE", DbType.String, cusData.MachineCode);
                    Db.AddInParameter(updateCmd, "KEY_VALUE", DbType.String, cusData.KeyValue);
                    Db.AddInParameter(updateCmd, "LOCAL_CODE", DbType.String, cusData.CusCiqNoInfo.LocationCode);
                    return Db.ExecuteNonQuery(updateCmd);
                }
            }
            #endregion

            return 0;
        }

        /// <summary>
        /// 保存申报数据
        /// </summary>
        /// <param name="declData">申报数据信息</param>
        /// <returns>返回结果</returns>
        public int UploadDeclData(DeclCusData declData)
        {
            #region 记录状态数据
            var insertCmd = Db.GetSqlStringCommand(Context.SqlInsertDeclCusData);
            Db.AddInParameter(insertCmd, "CUS_CIQ_NO", DbType.String, declData.CusCiqNoInfo.CusCiqNo);
            Db.AddInParameter(insertCmd, "CUS_MSG_XML", DbType.Xml, declData.CusMsgXml);
            Db.AddInParameter(insertCmd, "CIQ_MSG_XML", DbType.String, declData.CiqMsgXml);         
            Db.AddInParameter(insertCmd, "SAVE_TIME", DbType.DateTime, DateTime.Now);
            Db.AddInParameter(insertCmd, "STATUS", DbType.Int32, (int)declData.Status);
            return Db.ExecuteNonQuery(insertCmd);
            #endregion
        }

        /// <summary>
        /// 根据关检关联号获得密码
        /// </summary>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <returns>密码</returns>
        public string GetPasswordByCusCiqNo(string cusCiqNo)
        {
            var cmd = Db.GetSqlStringCommand(Context.SqlGetPasswordByCusCiqNo);
            Db.AddInParameter(cmd, "CUS_CIQ_NO", DbType.String, cusCiqNo);
            return Db.ExecuteScalar(cmd) as string;
        }

        /// <summary>
        /// 下载报关数据
        /// </summary>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="password">密码</param>
        /// <returns>报关数据</returns>
        public string DownloadCusData(string cusCiqNo, string password)
        {
            var cmd = Db.GetSqlStringCommand(Context.SqlGetCusData);
            Db.AddInParameter(cmd, "CUS_CIQ_NO", DbType.String, cusCiqNo);
            Db.AddInParameter(cmd, "R_PASSWORD", DbType.String, password);
            return Db.ExecuteScalar(cmd) as string;
        }

        /// <summary>
        /// 下载报检数据
        /// </summary>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="password">密码</param>
        /// <returns>报检数据</returns>
        public string DownloadCiqData(string cusCiqNo, string password)
        {
            var cmd = Db.GetSqlStringCommand(Context.SqlGetCiqData);
            Db.AddInParameter(cmd, "CUS_CIQ_NO", DbType.String, cusCiqNo);
            Db.AddInParameter(cmd, "R_PASSWORD", DbType.String, password);
            return Db.ExecuteScalar(cmd) as string;
        }



        /// <summary>
        /// 获得暂存时间
        /// </summary>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <returns>暂存时间</returns>
        public DateTime GetSaveTime(string cusCiqNo)
        {
            var cmd = Db.GetSqlStringCommand(Context.SqlGetPreCusDataUpdateTime);
            Db.AddInParameter(cmd, "CUS_CIQ_NO", DbType.String, cusCiqNo);
            var result = Db.ExecuteScalar(cmd);
            return result != DBNull.Value ? Convert.ToDateTime(result) : DateTime.MinValue;
        }



    }
}
