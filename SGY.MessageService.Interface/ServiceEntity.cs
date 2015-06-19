// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.MessageService.Interface
// FileName : ServiceEntity.cs
// Remark   : WCF服务实体类文件
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;


namespace GZCustoms.Application.SGY.MessageService.Interface
{
    /// <summary>
    ///  报文回执实体对象
    /// </summary>
    [Serializable]
    public class MesReceipt
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public string MessagID { get; set; }

        /// <summary>
        /// 接收日期
        /// </summary>
        public string RDate { get; set; }

        /// <summary>
        /// 说明信息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    ///  数据入库返回实体
    /// </summary>
    [Serializable]
    public class SaveModel
    {
        /// <summary>
        ///  是否保存成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        ///  关检关联号
        /// </summary>
        public string CusCiqNo { get; set; }

        /// <summary>
        ///  单据密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///  附加信息 （保存失败时产生的相关信息）
        /// </summary>
        public string Message { get; set; }
    }

    ///// <summary>
    /////  版本更新信息
    ///// </summary>
    //public class ClientVersion
    //{
    //    /// <summary>
    //    ///  版本号
    //    /// </summary>
    //    public string Code { get; set; }

    //    /// <summary>
    //    ///  更新日期
    //    /// </summary>
    //    public string Date { get; set; }

    //    /// <summary>
    //    ///  更新脚本
    //    /// </summary>
    //    public string[] Sql { get; set; }

    //    /// <summary>
    //    ///  更新模块
    //    /// </summary>
    //    public string[] ModuleName { get; set; }

    //    /// <summary>
    //    ///  dll存放的url
    //    /// </summary>
    //    public string Url { get; set; }

    //    /// <summary>
    //    ///  更新描述
    //    /// </summary>
    //    public string Text { get; set; }
    //}

    /// <summary>
    ///  用户信息
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        /// <summary>
        ///  用户表的GUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        ///  组织机构代码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        ///  海关编码
        /// </summary>
        public string CusCode { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登陆名称
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string EntName { get; set; }

    }

    /// <summary>
    /// 回执信息
    /// </summary>
    [Serializable]
    public class CusReturnInfo
    {
        /// <summary>
        /// TaskId任务编号
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 返回类型 TCS,QP
        /// </summary>
        public string ReturnType { get; set; }
        /// <summary>
        /// 返回代码
        /// </summary>
        public string ReturnCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string ReturnInfo { get; set; }
        /// <summary>
        /// 关检关联号
        /// </summary>
        public string CusCiqNo { get; set; }
        /// <summary>
        /// 状态信息
        /// </summary>
        public string Status { get; set; }

    }

    /// <summary>
    /// 回执信息
    /// </summary>
    [Serializable]
    public class CusReturnInfo2
    {
        /// <summary>
        /// TaskId任务编号
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 返回类型 TCS,QP
        /// </summary>
        public string ReturnType { get; set; }
        /// <summary>
        /// 返回代码
        /// </summary>
        public string ReturnCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string ReturnInfo { get; set; }
        /// <summary>
        /// 关检关联号
        /// </summary>
        public string CusCiqNo { get; set; }
        /// <summary>
        /// 状态信息
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 报关单号
        /// </summary>
        public string EntryNo { get; set; }

        /// <summary>
        /// 平台编号，没有预录入号的时候用
        /// </summary>
        public string EportNo { get; set; }

        public DateTime DateCreated { get; set; }

        public string MessageId { get; set; }

        public static explicit operator  CusReturnInfo(CusReturnInfo2 info)
        {
            return new CusReturnInfo(){
                TaskId = info.TaskId,
                ReturnType = info.ReturnType,
                ReturnCode = info.ReturnCode,
                ReturnInfo = info.ReturnInfo,
                CusCiqNo = info.CusCiqNo,
                Status = info.Status
            };
        }
    }

    /// <summary>
    /// 成功报关数据
    /// </summary>
    [Serializable]
    public class CusDeclDataMsg
    {
        /// <summary>
        /// TaskId任务编号
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 关检关联号
        /// </summary>
        public string CusCiqNo { get; set; }
        /// <summary>
        /// 上载QP时间
        /// </summary>
        public DateTime DeclTime { get; set; }
        /// <summary>
        /// 报文信息
        /// </summary>
        public string MessageXml { get; set; }

    }

}
