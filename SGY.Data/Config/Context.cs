// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Data
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
using System.Configuration;

namespace GZCustoms.Application.SGY.Data.Config
{
    internal static class Context
    {
        #region 常量

        #region 数据库类型
        internal const string DefaultSqlConnName = "DefaultSqlDb";
        internal const string DefaultOracleConnName = "DefaultOracleDb";
        internal const string EPortSqlConnName = "EPortSqlDb";
        internal const string EportOracleConnName = "EportOracleDb";
        internal const string SqlServerType = "SqlServer";
        internal const string OracleType = "Oracle";
        internal static string DefaultDbType
        {
            get
            {
                return ConfigurationManager.AppSettings["DbType"];
            }
        }
        #endregion

        #endregion

        #region SQL 语句

        internal const string SqlGetDeclQueueIndex =
            "SELECT [INDEX] FROM DECL_QUEUE WHERE IE_FLAG=@IE_FLAG AND CODE=@CODE AND CREATE_DATE=@CREATE_DATE";
        internal const string SqlUpdateDeclQueueIndex =
            "UPDATE DECL_QUEUE SET [INDEX]=@INDEX WHERE IE_FLAG=@IE_FLAG AND CODE=@CODE AND CREATE_DATE=@CREATE_DATE";
        internal const string SqlInsertDeclQueueIndex =
            "INSERT INTO DECL_QUEUE (IE_FLAG,CODE,CREATE_DATE,[INDEX]) VALUES (@IE_FLAG, @CODE, @CREATE_DATE, @INDEX)";
        internal const string SqlGetKeyInfo =
            "SELECT GUID, KEY_NAME, KEY_VALUE, START_DATE, END_DATE, MACHINE_CODE, COM_NAME FROM KEY_INFO";
        internal const string SqlCheckKey =
            "SELECT COUNT(*) FROM KEY_INFO WHERE KEY_VALUE=@KEY_VALUE AND MACHINE_CODE=@MACHINE_CODE";
        internal const string SqlGetMaxTcsCurrId =
            "SELECT MAX(CURRENT_ID) FROM DECL_DATA WHERE DECL_DATE =@DECL_DATE";
        internal const string SqlSaveMsgInfo =
            "INSERT INTO DECL_DATA (KEY_VALUE,MACHINE_CODE,MESSAGE,DECL_TIME,DECL_DATE,TCS_MSGID,TCS_TASKID,TCS_DOCUMENT_NO,CURRENT_ID,TCS_MESSAGE,GUID,SEND_TIME,CUS_CIQ_NO) " +
            "VALUES (@KEY_VALUE,@MACHINE_CODE,@MESSAGE,@DECL_TIME,@DECL_DATE,@TCS_MSGID,@TCS_TASKID,@TCS_DOCUMENT_NO,@CURRENT_ID,@TCS_MESSAGE,@GUID,@SEND_TIME,@CUS_CIQ_NO)";
        internal const string SqlGetMsgInfo =
            "SELECT  MESSAGE,TCS_TASKID,CUS_CIQ_NO,DECL_TIME FROM DECL_DATA ";
        internal const string SqlGetCountByCusCiqNo = "SELECT COUNT(*) FROM PRE_MSG_DATA WHERE CUS_CIQ_NO = @CUS_CIQ_NO";
        internal const string SqlGetPasswordByCusCiqNo = "SELECT R_PASSWORD FROM PRE_MSG_DATA WHERE CUS_CIQ_NO = @CUS_CIQ_NO";
        internal const string SqlInsertPreCusData =
            "INSERT INTO PRE_MSG_DATA (CUS_CIQ_NO,IE_FLAG,CUS_MSG_XML,CIQ_MSG_XML,R_PASSWORD,UPDATE_TIME,MACHINE_CODE,KEY_VALUE,LOCAL_CODE) " +
            "VALUES (@CUS_CIQ_NO,@IE_FLAG,@CUS_MSG_XML,@CIQ_MSG_XML,@R_PASSWORD,@UPDATE_TIME,@MACHINE_CODE,@KEY_VALUE,@LOCAL_CODE)";
        internal const string SqlInsertDeclCusData =
           "INSERT INTO DECL_MSG_DATA (CUS_CIQ_NO,CUS_MSG_XML,CIQ_MSG_XML,SAVE_TIME,STATUS) " +
           "VALUES (@CUS_CIQ_NO,@CUS_MSG_XML,@CIQ_MSG_XML,@SAVE_TIME,@STATUS)";        
        internal const string SqlUpdatePreCusCiqData =
            "UPDATE PRE_MSG_DATA SET IE_FLAG=@IE_FLAG,CUS_MSG_XML=@CUS_MSG_XML,CIQ_MSG_XML=@CIQ_MSG_XML,UPDATE_TIME=@UPDATE_TIME,MACHINE_CODE=@MACHINE_CODE,KEY_VALUE=@KEY_VALUE,LOCAL_CODE=@LOCAL_CODE " +
            "WHERE CUS_CIQ_NO=@CUS_CIQ_NO";
        internal const string SqlUpdatePreCusData =
            "UPDATE PRE_MSG_DATA SET IE_FLAG=@IE_FLAG,CUS_MSG_XML=@CUS_MSG_XML,UPDATE_TIME=@UPDATE_TIME,MACHINE_CODE=@MACHINE_CODE,KEY_VALUE=@KEY_VALUE,LOCAL_CODE=@LOCAL_CODE " +
            "WHERE CUS_CIQ_NO=@CUS_CIQ_NO";
        internal const string SqlGetCusData =
            "SELECT CUS_MSG_XML FROM PRE_MSG_DATA WHERE CUS_CIQ_NO = @CUS_CIQ_NO AND R_PASSWORD = @R_PASSWORD";          
    
        internal const string SqlGetCiqData =
            "SELECT CIQ_MSG_XML FROM PRE_MSG_DATA WHERE CUS_CIQ_NO = @CUS_CIQ_NO AND R_PASSWORD = @R_PASSWORD";
        internal const string SqlGetPreCiqData =
            "SELECT CIQ_MSG_XML FROM PRE_MSG_DATA WHERE CUS_CIQ_NO = @CUS_CIQ_NO AND R_PASSWORD = @R_PASSWORD";
        internal const string SqlGetPreCusDataUpdateTime =
            "SELECT UPDATE_TIME FROM PRE_MSG_DATA WHERE CUS_CIQ_NO = @CUS_CIQ_NO ";
        internal const string SqlGetLoginUserInfo = "SELECT * FROM COM_ENT_INFO ";
        internal const string SqlUpdateLoginUserPwd =
            "UPDATE COM_ENT_INFO SET LOGIN_PASSWORD = @LOGIN_PASSWORD_NEW WHERE LOGIN_NAME = @LOGIN_NAME AND LOGIN_PASSWORD = @LOGIN_PASSWORD";
        internal const string SqlGetUserKeyCount = "SELECT COUNT(*) FROM V_USER_KEY WHERE USER_GUID = @USER_GUID AND KEY_VALUE = @KEY_VALUE";
        internal const string SqlGetActivatedUserKeyCount = "SELECT COUNT(*) FROM V_USER_KEY WHERE USER_GUID = @USER_GUID AND KEY_VALUE = @KEY_VALUE AND MACHINE_CODE=@MACHINE_CODE";
        internal const string SqlActivate = "UPDATE KEY_INFO SET MACHINE_CODE = @MACHINE_CODE WHERE KEY_VALUE = @KEY_VALUE AND MACHINE_CODE IS NULL";
        internal const string SqlGetCusReturn =
            "SELECT TASK_ID,RETURN_TYPE,RETURN_CODE,RETURN_INFO,CUSTOMS_CIQ_NO,STATUS,ENTRY_NO,EPORT_NO,CREATE_DATE,OID FROM DECL_RETURN";
        internal const string SqlInsertLogInfo =
            "INSERT INTO DECL_LOG (LOG_TYPE,TITLE,LOG_INFO,LOG_DATE,EVENT_ID,SOURCE,MESSAGE) " +
            "VALUES (@LOG_TYPE,@TITLE,@LOG_INFO,@LOG_DATE,@EVENT_ID,@SOURCE,@MESSAGE)";
        #endregion

        #region 异常信息
        internal const string ErrDbTypeNull = "没有配置数据库类型或连接串";
        internal const string ErrUnSupportedDbType = "不支持的数据库类型";
        #endregion


    }
}
