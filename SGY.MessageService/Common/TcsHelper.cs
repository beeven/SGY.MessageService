// -------------------------------------------------
// Copyright (c) 2013,GZCustoms
// All rights reserved.
// -------------------------------------------------
// Assembly : SGY.MessageService
// FileName : TcsHelper.cs
// Remark   : TCS报文处理工具类
// -------------------------------------------------    
// VERSION    AUTHOR        DATE          CONTENT
// 1.0        彭煜        20130415        创建
// -------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using GZCustoms.Application.SGY.Entity;
using GZCustoms.Application.SGY.MessageService.Config;

namespace GZCustoms.Application.SGY.MessageService.Common
{
    internal class TcsHelper
    {
        internal XDocument GenerateTcsXDoc(XDocEntity entity, CusDataMsg cusMsg)
        {
            XDocument tcsDoc = XDocument.Parse(entity.XDoc.ToString());
            XDocument msgDoc = XDocument.Parse(cusMsg.MessageXml);
            XNamespace tcs = entity.NS;
            XElement tcsHead = tcsDoc.Root.Element("MessageHead");
            XElement tcsBody = tcsDoc.Root.Element("MessageBody");
            //MessageHead赋值
            tcsHead.Element("MessageId").Value = cusMsg.MessageId;
            tcsHead.Element("MessageTime").Value = string.Format("{0}{1}{2}{3}{4}{5}",
                DateTime.Now.Year.ToString(),
                DateTime.Now.Month.ToString().PadLeft(2, '0'),
                DateTime.Now.Day.ToString().PadLeft(2, '0'),
                DateTime.Now.Hour.ToString().PadLeft(2, '0'),
                DateTime.Now.Minute.ToString().PadLeft(2, '0'),
                DateTime.Now.Second.ToString().PadLeft(2, '0'));
            //TcsFlow赋值
            XElement flowEle = tcsBody.Element(tcs + "TcsFlow201").Element(tcs + "TcsFlow");
            flowEle.Element(tcs + "MessageId").Value = cusMsg.MessageId;
            flowEle.Element(tcs + "TaskId").Value = cusMsg.TaskId;
          
            XElement declDocEle = tcsBody.Element(tcs + "TcsFlow201").Element(tcs + "TcsData").Element(tcs + "DeclarationDocument");
            //TcsDocumentNo赋值
            declDocEle.Element(tcs + "TcsDocumentNo").Value = cusMsg.TcsDocumentNo;
            //报关相关数据赋值                  
            XElement entryInfoEle = declDocEle.Element(tcs + "EntryInformation");
            XElement declHeadEle = msgDoc.Root.Element("EnvelopBody").Element("DECL_HEAD");
            XElement declListsEle = msgDoc.Root.Element("EnvelopBody").Element("DECL_LISTS");
            XElement declContainersEle = msgDoc.Root.Element("EnvelopBody").Element("DECL_CONTAINERS");
            XElement declCertificates = msgDoc.Root.Element("EnvelopBody").Element("DECL_CERTIFICATES");

            TransformEntryHeadData(declHeadEle);

            //EntryIdentityInformationList
            ParseEntryIdentityInformationList(entryInfoEle.Element(tcs + "EntryIdentityInformationList"), declHeadEle, tcs);
            //LogisticsLocationInformationList
            ParseLogisticsLocationInformationList(entryInfoEle.Element(tcs + "LogisticsLocationInformationList"), declHeadEle, tcs);
            //EportLocationInformationList
            ParseEportLocationInformationList(entryInfoEle.Element(tcs + "EportLocationInformationList"), declHeadEle, tcs);
            //BaseInformation
            ParseBaseInformation(entryInfoEle.Element(tcs + "BaseInformation"), declHeadEle);
            //GoodsInformationList
            ParseGoodsInformationList(entryInfoEle.Element(tcs + "GoodsInformationList"), declListsEle, tcs);
            //EntryContainerInformationList

            if(declContainersEle != null)
            {
                ParseEntryContainerInformationList(entryInfoEle.Element(tcs + "EntryContainerInformationList"), declContainersEle, tcs);
            }
                                
            //DocumentAttachedInformationList
            ParseDocumentAttachedInformationList(entryInfoEle.Element(tcs + "DocumentAttachedInformationList"), declCertificates, tcs);

            ParseTransitInformation(declDocEle, tcs, declHeadEle);

            return tcsDoc;                       
            
        }

        /// <summary>
        /// 预处理报文EntryHead节点数据
        /// </summary>
        /// <param name="headEle"></param>
        private void TransformEntryHeadData(XElement headEle)
        {
            //翻译EntryType节点内容
            XElement entryTypeEle = headEle.Element("ENTRY_TYPE");
            switch (entryTypeEle.Value)
            {                 
                case "O":
                    //有纸报关
                    entryTypeEle.Value = "001";
                    break;
                case "W":
                    //无纸报关
                    entryTypeEle.Value = "002";
                    break;
                case "L":
                    //有纸带清单报关
                    entryTypeEle.Value = "003";
                    break;
                case "D":
                    //无纸带清单报关
                    entryTypeEle.Value = "004";
                    break;
                case "M":
                    //通关无纸化
                    entryTypeEle.Value = "005";
                    break;
                default:
                    break;
            }           
        }

        /// <summary>
        /// 处理EntryIdentityInformationList节点：
        /// </summary>
        /// <param name="entryIdInfoEle"></param>
        /// <param name="headEle"></param>
        /// <param name="tcs"></param>
        private void ParseEntryIdentityInformationList(XElement entryIdInfoEle, XElement headEle, XNamespace tcs)
        {
            if (entryIdInfoEle.HasAttributes)
                entryIdInfoEle.RemoveAttributes();
            foreach (XElement ele in entryIdInfoEle.Elements())
            {
                //代码
                XElement codeEle = ele.Element(tcs + "CorporationCustomsCode");
                if(HasMapAttribute(codeEle))
                    codeEle.Value = headEle.Element(codeEle.Attribute("map").Value).Value;
                //名称               
                XElement nameEle = ele.Element(tcs + "CorporationName");
                if(HasMapAttribute(nameEle))
                    nameEle.Value = headEle.Element(nameEle.Attribute("map").Value).Value;
                //删除属性
                codeEle.RemoveAttributes();
                nameEle.RemoveAttributes();
            }
        }

        /// <summary>
        /// 处理LogisticsLocationInformationList节点
        /// </summary>
        /// <param name="locationInfoEle"></param>
        /// <param name="headEle"></param>
        /// <param name="tcs"></param>
        private void ParseLogisticsLocationInformationList(XElement locationInfoEle, XElement headEle, XNamespace tcs)
        {
            if (locationInfoEle.HasAttributes)
                locationInfoEle.RemoveAttributes();
            foreach (XElement ele in locationInfoEle.Elements())
            {
                //代码
                XElement codeEle = ele.Element(tcs + "LogisticsLocationCode");
                if(HasMapAttribute(codeEle))
                    codeEle.Value = headEle.Element(codeEle.Attribute("map").Value).Value;
                codeEle.RemoveAttributes();

            }
        }

        /// <summary>
        /// 处理EportLocationInformationList节点
        /// </summary>
        /// <param name="locationInfoEle"></param>
        /// <param name="headEle"></param>
        /// <param name="tcs"></param>
        private void ParseEportLocationInformationList(XElement locationInfoEle, XElement headEle, XNamespace tcs)
        {
            if (locationInfoEle.HasAttributes)
                locationInfoEle.RemoveAttributes();
            foreach (XElement ele in locationInfoEle.Elements())
            {
                //代码
                XElement codeEle = ele.Element(tcs + "EportLocationCode");
                if(HasMapAttribute(codeEle))
                    codeEle.Value = headEle.Element(codeEle.Attribute("map").Value).Value;
                codeEle.RemoveAttributes();
            }
        }

        /// <summary>
        /// 处理BaseInformation节点，报关单表头基本信息
        /// </summary>
        /// <param name="baseInfoEle"></param>
        /// <param name="headEle"></param>
        /// <param name="tcs"></param>
        private void ParseBaseInformation(XElement baseInfoEle, XElement headEle)
        {
            if (baseInfoEle.HasAttributes)
                baseInfoEle.RemoveAttributes();
            foreach (XElement ele in baseInfoEle.Elements())
            {
                if (HasMapAttribute(ele))
                {
                    if (HasMapAttribute(ele))
                        ele.Value = headEle.Element(ele.Attribute("map").Value).Value;
                    else
                        //没有map属性不处理
                        continue;
                    if (string.IsNullOrEmpty(ele.Value) && IsNullable(ele))
                    {
                        ele.RemoveAttributes();
                        XNamespace xsi = Context.xsi;
                        ele.Add(new XAttribute(xsi + "nil", "true"));
                        continue;
                    }
                    ele.RemoveAttributes();
                }
            }
        }

        /// <summary>
        /// 处理GoodsInformationList，报关单表体基本信息
        /// </summary>
        /// <param name="goodsInfoListEle"></param>
        /// <param name="bodyEleList"></param>
        /// <param name="tcs"></param>
        private void ParseGoodsInformationList(XElement goodsInfoListEle, XElement declListsEle, XNamespace tcs)
        {
            if (goodsInfoListEle.HasAttributes)
                goodsInfoListEle.RemoveAttributes();
            XElement tmpEle = goodsInfoListEle.Element(tcs + "GoodsInformation");
            foreach (XElement bodyEle in declListsEle.Elements())
            {
                XElement newInfoEle = new XElement(tcs + "GoodsInformation");
                foreach (XElement ele in tmpEle.Elements())
                {
                    string value = string.Empty;
                    if (HasMapAttribute(ele))
                        value = bodyEle.Element(ele.Attribute("map").Value).Value;
                    else
                        //没有map属性不处理
                        continue;                    
                    if (string.IsNullOrEmpty(value) && IsNullable(ele))
                    {
                        XNamespace xsi = Context.xsi;
                        newInfoEle.Add(new XElement(ele.Name, new XAttribute(xsi + "nil", "true")));
                        continue;
                    }
                    newInfoEle.Add(new XElement(ele.Name, value));
                }
                goodsInfoListEle.Add(newInfoEle);
            }
            tmpEle.Remove();
        }

        /// <summary>
        /// 处理EntryContainerInformationList节点，集装箱信息
        /// </summary>
        /// <param name="containerInfoListEle"></param>
        /// <param name="containerEleList"></param>
        /// <param name="tcs"></param>
        private void ParseEntryContainerInformationList(XElement containerInfoListEle, XElement containersEle, XNamespace tcs)
        {
            if (containerInfoListEle.HasAttributes)
                containerInfoListEle.RemoveAttributes();
            XElement tmpEle = containerInfoListEle.Element(tcs + "EntryContainerInformation");
            foreach (XElement containerEle in containersEle.Elements())
            {
                XElement newInfoEle = new XElement(tcs + "EntryContainerInformation");
                foreach (XElement ele in tmpEle.Elements())
                {
                    string value = string.Empty;
                    if (HasMapAttribute(ele))
                        value =containerEle.Element(ele.Attribute("map").Value).Value;
                    else
                        //没有map属性不处理
                        continue;
                    if (string.IsNullOrEmpty(value) && IsNullable(ele))
                    {
                        XNamespace xsi = Context.xsi;
                        newInfoEle.Add(new XElement(ele.Name, new XAttribute(xsi + "nil", "true")));
                        continue;
                    }
                    newInfoEle.Add(new XElement(ele.Name, value));
                }
                containerInfoListEle.Add(newInfoEle);
            }
            tmpEle.Remove();
        }

        /// <summary>
        /// 处理DocumentAttachedInformationList，随附单证信息
        /// </summary>
        /// <param name="attachedInfoListEle"></param>
        /// <param name="certificateEleList"></param>
        /// <param name="tcs"></param>
        private void ParseDocumentAttachedInformationList(XElement attachedInfoListEle, XElement certificatesEle, XNamespace tcs)
        {
            if (attachedInfoListEle.HasAttributes)
                attachedInfoListEle.RemoveAttributes();
            XElement tmpEle = attachedInfoListEle.Element(tcs + "DocumentAttachedInformation");
            foreach (XElement certificateEle in certificatesEle.Elements())
            {
                XElement newInfoEle = new XElement(tcs + "DocumentAttachedInformation");
                foreach (XElement ele in tmpEle.Elements())
                {
                    string value = string.Empty;
                    if(HasMapAttribute(ele))
                        value = certificateEle.Element(ele.Attribute("map").Value).Value;
                    else
                        //没有map属性不处理
                        continue;
                    if (string.IsNullOrEmpty(value) && IsNullable(ele))
                    {
                        XNamespace xsi = Context.xsi;
                        newInfoEle.Add(new XElement(ele.Name, new XAttribute(xsi + "nil", "true")));
                        continue;
                    }
                    newInfoEle.Add(new XElement(ele.Name, value));
                }
                attachedInfoListEle.Add(newInfoEle);
            }
            tmpEle.Remove();
        }

        /// <summary>
        /// 处理TransitInformation，转关单信息。现在的处理是，申报口岸为5161的，全部改为转关单
        /// </summary>
        /// <param name="transitInformationElement">tcs:DeclarationDocument节点</param>
        /// <param name="tcs"></param>
        private void ParseTransitInformation(XElement declarationDocuemntElement, XNamespace tcs, XElement declHeadElem)
        {

            var entryTransitType = declHeadElem.Element("ENTRY_TRANSIT_TYPE");
            if(entryTransitType != null && entryTransitType.Value == "003")
            { 

                // 修改 EntryTransitType 为 003
                declarationDocuemntElement.Descendants(tcs + "EntryTransitType").Single().Value = "003";

                // 增加 TransitInformation 节点
                //var preEntryNo = declarationDocuemntElement.Descendants(tcs + "PreentryNo").Single().Value;
                //var declPort = declarationDocuemntElement.Descendants(tcs + "EportLocationInformation")
                //                                         .Where(x=>x.Element(tcs + "EportLocationTypeCode").Value == "001")
                //                                         .Single()
                //                                         .Element(tcs+"EportLocationCode").Value;
                //var importYear = declarationDocuemntElement.Descendants(tcs + "ImportExportDate").Single().Value.Substring(0, 4);
                //var importFlag = declarationDocuemntElement.Descendants(tcs + "ImportExportFlag").Single().Value == "I" ? "1" : "0";
                /*
                      <tcs:TransitInformation>
                        <tcs:TransitBaseInformation>
                          <tcs:TransitEportNo>519920141994173575</tcs:TransitEportNo>
                          <tcs:Note>test</tcs:Note>
                        </tcs:TransitBaseInformation>
                      </tcs:TransitInformation>
                */
                //var declPort = declarationDocuemntElement.Descendants(tcs + "EportLocationInformation")
                //                                        .Single(x => x.Element(tcs + "EportLocationTypeCode").Value == "001")
                //                                        .Element(tcs + "EportLocationCode").Value;
                XNamespace xsi = Context.xsi;
                var transitInformationElement = new XElement(tcs + "TransitInformation",
                                                    new XElement(tcs + "TransitBaseInformation",
                                                        new XElement(tcs + "TransitEportNo",new XAttribute(xsi + "nil", true)),
                                                        new XElement(tcs + "Note", "sgy_test")
                                                    )
                                                );
                declarationDocuemntElement.Element(tcs + "SignInformation").AddBeforeSelf(transitInformationElement);
            }
            
        }

        private bool IsNullable(XElement ele)
        {
            if (ele.HasAttributes
                && !string.IsNullOrEmpty(ele.Attribute("format").Value)
                && ele.Attribute("format").Value.Contains('['))
                return true;
            else
                return false;
        }

        private bool HasMapAttribute(XElement ele)
        {
            if (ele.HasAttributes && ele.FirstAttribute.Name == "map"
                    && !String.IsNullOrEmpty(ele.Attribute("map").Value))
                return true;
            return false;
        }

        private bool HasNullableAttribute(XElement ele, XNamespace xsi)
        {
            if (ele.HasAttributes && ele.FirstAttribute.Name == xsi + "nil"
                && (String.Compare("true", ele.Attribute(xsi + "nil").Value, true) == 0))
                return true;
            return false;
        }




    }
}
