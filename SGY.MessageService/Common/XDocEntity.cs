// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.MessageService
// FileName : XDocEntity.cs
// Remark   : XML文档对象实体类
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------

using System.Xml.Linq;

namespace GZCustoms.Application.SGY.MessageService.Common
{
    /// <summary>
    /// XDocument信息实体
    /// </summary>
    public class XDocEntity
    {
        /// <summary>
        /// XDocument
        /// </summary>
        public XDocument XDoc { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public XNamespace NS { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xDoc">XDocument</param>
        /// <param name="ns">命名空间</param>
        public XDocEntity(XDocument xDoc, XNamespace ns)
        {
            XDoc = xDoc;
            NS = ns;
        }

        public XDocEntity() { }
    }
}

