using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using GZCustoms.Application.SGY.Entity;

namespace GZCustoms.Application.SGY.MessageService.Common
{
    internal class DeclHelper
    {
        internal DeclEnvelopHead GetEnvelopHeader(string msgXml)
        {
            XDocument doc = XDocument.Parse(msgXml);
            XElement headerEle = doc.Root.Element("EnvelopHead");
            DeclEnvelopHead head = new DeclEnvelopHead
            {
                Name = headerEle.Element("Name").Value,
                Version = headerEle.Element("Version").Value,
                From = headerEle.Element("From").Value,
                To = headerEle.Element("To").Value,
                Operation = headerEle.Element("Operation").Value,              
                MsgGuid = headerEle.Element("Guid").Value
            };
            DateTime time;
            if (DateTime.TryParse(headerEle.Element("SendTime").Value, out time))
            {
                head.SendTime = time;
            }
            return head;
        }
    }
}
