// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Data
// FileName : IPreserveDataHelper.cs
// Remark   : 暂存相关数据操作接口类
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GZCustoms.Application.SGY.Entity;

namespace GZCustoms.Application.SGY.Data.Interface
{
    public interface IPreserveDataHelper 
    {
        /// <summary>
        /// 获得现场记录编号
        /// </summary>
        /// <param name="ieFlag">进出口标识</param>
        /// <param name="locationCode">现场代码</param>
        /// <returns>编号</returns>
        int GetCusCiqIndex(string ieFlag, string locationCode);

        /// <summary>
        /// 暂存报关数据
        /// </summary>
        /// <param name="cusData">报关数据</param>
        /// <returns>执行结果</returns>
        int UploadPreCusData(PreCusData cusData);

         /// <summary>
        /// 保存申报数据
        /// </summary>
        /// <param name="declData">申报数据信息</param>
        /// <returns>返回结果</returns>
        int UploadDeclData(DeclCusData declData);

        /// <summary>
        /// 根据关检关联号获得密码
        /// </summary>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <returns>密码</returns>
        string GetPasswordByCusCiqNo(string cusCiqNo);

        /// <summary>
        /// 下载报关数据
        /// </summary>
        /// <param name="cusCqiNo">关检关联号</param>
        /// <param name="password">密码</param>
        /// <returns>报关数据</returns>
        string DownloadCusData(string cusCiqNo, string password);

        /// <summary>
        /// 下载报检数据
        /// </summary>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="password">密码</param>
        /// <returns>报检数据</returns>
        string DownloadCiqData(string cusCiqNo, string password);

        /// <summary>
        /// 获得暂存时间
        /// </summary>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <returns>暂存时间</returns>
        DateTime GetSaveTime(string cusCiqNo);
    }
}
