// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.MessageService
// FileName : Utility.cs
// Remark   : 工具类
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Security.Cryptography;
using GZCustoms.Application.SGY.Entity;
using GZCustoms.Application.SGY.MessageService.Config;

namespace GZCustoms.Application.SGY.MessageService.Common
{
    internal class Utility
    {
        /// <summary>
        /// 验证激活码是否有效
        /// </summary>
        /// <param name="keyInfo"></param>
        /// <returns></returns>
        internal static bool CheckKey(KeyInfo keyInfo)
        {
            if (!new EntityValidator<KeyInfo>().Validate(keyInfo))
                return false;
            if (System.DateTime.Now > keyInfo.StartDate && System.DateTime.Now < keyInfo.EndDate)
                return true;
            return false;
        }

        internal static CusDataMsg FormatCusDataMsg(CusDataMsg msg, long currentId, string idHead, string documentNo)
        {
            //生成Tcs报文Id
            if (currentId == 0)
                currentId = Convert.ToInt64(ConfigInfo.CurrentId, 16) + 1;
            else
                currentId = ++currentId;
            msg.MessageId = idHead + DateTime.Now.ToString("yyyyMMdd") + Convert.ToString(currentId, 16);
            msg.CurrentId = currentId;
            msg.TaskId = msg.MessageId;
            msg.TcsDocumentNo = idHead + "001" + DateTime.Now.ToString("yyyyMMdd") + documentNo;            
            return msg;
        }

        internal static string GetRandomNumString(int len)
        {
            char[] chars = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            string code = string.Empty;
            for (int i = 0; i < len; i++)
            {
                //这里是关键，传入一个seed参数即可保证生成的随机数不同           
                //Random rnd = new Random(unchecked((int)DateTime.Now.Ticks));
                byte[] bytes = new byte[4];
                RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
                rng.GetBytes(bytes);
                Random rnd = new Random(BitConverter.ToInt32(bytes, 0));           
                code += chars[rnd.Next(0, 10)].ToString();
            }
            return code;              
        }

        

  

    }
}
