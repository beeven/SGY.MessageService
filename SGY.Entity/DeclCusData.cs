// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Entity
// FileName : DeclCusData.cs
// Remark   : 报关数据信息实体类
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130528        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZCustoms.Application.SGY.Entity
{
    public class DeclCusData
    {
        public enum SaveType { TempSave, CiqDecl, UploadQp, CusDecl }

        /// <summary>
        /// 关检关联号相关信息
        /// </summary>
        public CusCiqNoInfo CusCiqNoInfo { get; set; }

        /// <summary>
        /// 报关Xml
        /// </summary>     
        public string CusMsgXml { get; set; }

        /// <summary>
        /// 报检报文Xml
        /// </summary>
        public string CiqMsgXml { get; set; }

        /// <summary>
        /// 数据状态（0，暂存；1，报检；2，上载QP；3，申报）
        /// </summary>
        public SaveType Status { get; set; }     
    }
}
