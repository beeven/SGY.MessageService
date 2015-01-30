// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.MessageService
// FileName : Context.cs
// Remark   : 常量
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GZCustoms.Application.SGY.MessageService.Config
{
    internal static class Context
    {
        #region 常量
        internal const string DefaultSlidingExpiration = "10";
        internal const string xsi = "http://www.w3.org/2001/XMLSchema-instance";
        

        #endregion

        #region SQL 语句
        #endregion

        #region 错误信息
        internal const string ErrIeFlagOrLocalCodeInValid = "进出口标识或现场代码非法";
        internal const int ErrIeFlagOrLocalCodeInValidId = 1;
        internal const string ErrCusDataMsgInValid = "上载报关报文数据、激活码或机器码非法";
        internal const int ErrCusDataMsgInValidId = 2;
        internal const string ErrKeyValueInvalid = "激活码激活错误或过期";
        internal const int ErrKeyValueInvalidId = 3;
        internal const string ErrCusCiqNoInvalid = "关检关联号非法";
        internal const int ErrCusCiqNoInvalidId = 4;
        internal const string ErrRPwdInvalid = "动态密码非法";
        internal const int ErrRPwdInvalidId = 5;

        internal const string ErrSendMessage = "上载报文错误";
        internal const string ErrGetCusCiqNo = "获取关检关联号错误";
        internal const string ErrDownloadCustomsData = "下载报关数据错误";
        internal const string ErrDownloadCiqData = "下载报检数据错误";
        internal const string ErrUploadAllData = "上传数据错误";
        internal const string ErrGetSaveTime = "获取服务器上单据的保存时间";
        internal const string ErrLogin = "用户登陆错误";
        internal const string ErrUpdatePassword = "用户更改密码错误";
        internal const string ErrActiveKey = "激活错误";
        internal const string ErrReceiveMsgRep = "下载报关回执错误";

        #endregion

        #region 操作Id
        internal const int SendMessageEventId = 101;
        internal const int GetCusCiqNoEventId = 102;
        internal const int DownloadCustomsDataEventId = 103;
        internal const int DownloadCiqDataEventId = 104;
        internal const int UploadAllDataEventId = 105;
        internal const int GetSaveTimeEventId = 106;
        internal const int LoginEventId = 107;
        internal const int UpdatePasswordEventId = 108;
        internal const int ActiveKeyEventId = 109;
        internal const int ReceiveMsgRepEventId = 110;

        #endregion


    }
}
