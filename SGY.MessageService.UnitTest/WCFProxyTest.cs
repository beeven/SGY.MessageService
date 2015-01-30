using System;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GZCustoms.Application.SGY.MessageService.Interface;
using GZCustoms.Application.SGY.MessageService.WCFProxy;

namespace GZCustoms.Application.SGY.MessageService.UnitTest
{
    [TestClass]
    public class WCFProxyTest
    {
        [TestMethod]
        public void ProxyTest()
        {

            //string url = "http://localhost/SGY.MessageService.Web/MessageServiceWCF.svc";
            //string url = "http://localhost:42197/MessageServiceWCF.svc";
            //string url = "http://10.53.33.98/SGY.MessageService.Web/MessageServiceWCF.svc";
            string url = "http://183.63.251.70/SGY.MessageService.Web2/MessageServiceWCF.svc";
            WCFServiceProxy proxy = new WCFServiceProxy(url);
            //WCFServiceProxy proxy = new WCFServiceProxy(url, "paddy", "1qaz@WSX");

            //string cusCiqNo = proxy.GetCusCiqNo("1", "5100");
            //XDocument declDoc = XDocument.Load(@"F:\Temp\DeclMsg\DECL_FILE2.xml");
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(@"F:\Temp\DeclMsg\DECL_FILE.xml");
            //MesReceipt res2 = proxy.SendMessage("130409667935", "00-21-70-67-E8-27",
            //    cusCiqNo, xmlDoc.InnerXml);
            //上载报文
            //MesReceipt res = proxy.SendMessage("130409667935", "00-21-70-67-E8-27",
            //   cusCiqNo, declDoc.ToString());
            //MesReceipt res = proxy.SendMessage("130409667935", "00-21-70-67-E8-27",
            //    cusCiqNo, declDoc.ToString());
            //Assert.AreEqual(string.Empty, res.Message);
            //Assert.AreEqual<Boolean>(false, string.IsNullOrEmpty(res.MessagID));
            //上传报文（全）
            //SaveModel model = proxy.UploadAllData("130409667935", "00-21-70-67-E8-27", "1", "5100",
            //    cusCiqNo, 0, declDoc.ToString(), declDoc.ToString());
            //Assert.AreEqual<Boolean>(true, model.IsSuccess);
            ////上传报关报文
            //model = proxy.UploadCustomsData("130409667935", "00-21-70-67-E8-27", "1", "5100",
            //    cusCiqNo, 3, declDoc.ToString());
            //Assert.AreEqual<Boolean>(true, model.IsSuccess);
            //下载报关数据
            //string cusMsg = proxy.DownloadCustomsData("888888", "178BFBFF00200F31", "1130702516600003", "365039");
            //Assert.IsNotNull(cusMsg);
            //XDocument cusDoc = XDocument.Parse(cusMsg);
            //Assert.AreEqual<string>("662025362", cusDoc.Root.Element("EnvelopBody").Element("DECL_HEAD").Element("PRE_ENTRY_ID").Value);
            ////下载报检数据
            //string ciqMsg = proxy.DownloadCiqData("130409667935", "00-21-70-67-E8-27", "0130422510000024", "950496");
            //Assert.IsNotNull(ciqMsg);
            //XDocument ciqDoc = XDocument.Parse(ciqMsg);
            //Assert.AreEqual<string>("662025362", ciqDoc.Root.Element("EnvelopBody").Element("DECL_HEAD").Element("PRE_ENTRY_ID").Value);
            ////获得更新时间
            //Assert.AreEqual(DateTime.Parse("2013-04-22 10:33:28.000"), proxy.GetSaveTime("0130422510000024"));
            ////登陆
            UserInfo user = proxy.Login("gzctest", "123456");
            //Assert.AreEqual("3c94fe4f-677d-4ffc-9922-9479bb784283", user.Guid);
            //修改密码           
            //Assert.AreEqual<int>(1, proxy.UpdatePassword("jctest", "jctest", "jctest"));
            //UserInfo user = proxy.Login("jctest", "jctest");
            //Assert.AreEqual("3c94fe4f-677d-4ffc-9922-9479bb784283", user.Guid);
            //激活
            //Assert.AreEqual<int>(1, proxy.ActiveKeyByLoginName("gzctest", "123456", "141224926731", "ABCDEFGHIJKL"));
            //Assert.AreEqual<int>(2, proxy.ActiveKeyByLoginName("hgtest", "hgtest", "130521146400", "BFEBFBFF0001067A"));
            //下载回执
            var returnInfo = proxy.ReceiveMsgRep2("141224926731", "ABCDEFGHIJKL", "T1907843510020141223f4ff60bb5");
            foreach (var cusReturn in returnInfo)
            {
                Assert.AreEqual<Boolean>(false, string.IsNullOrEmpty(cusReturn.ReturnInfo));

            }
            //下载报关数据
            Assert.AreEqual<string>("01304225100000015", proxy.GetDeclCusData("T1907843510020130422f4ff60b9f").CusCiqNo);


        }
    }
}
