// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.Entity
// FileName : DeclEnvelopHead.cs
// Remark   : 报文头实体类
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZCustoms.Application.SGY.Entity
{
    public class DeclEnvelopHead
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Operation { get; set; }
        public DateTime SendTime { get; set; }
        public string MsgGuid { get; set; }
    }
}
