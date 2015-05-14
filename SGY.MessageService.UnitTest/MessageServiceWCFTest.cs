using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.ServiceModel;
using GZCustoms.Application.SGY.MessageService;
using GZCustoms.Application.SGY.MessageService.Interface;
using GZCustoms.Application.SGY.Entity;

namespace GZCustoms.Application.SGY.MessageService.UnitTest
{
    [TestClass]
    public class MessageServiceWCFTest
    {
        [TestMethod]
        public void WCFTest()
        {
            //ServiceReference1.MessageServiceWCFClient client = new ServiceReference1.MessageServiceWCFClient();
            //string cusCiqNo = client.GetCusCiqNo("0", "5100");

            WSHttpBinding httpBinding = new WSHttpBinding();
            httpBinding.Security.Mode = SecurityMode.None;
            //string url = "http://localhost:42197/MessageServiceWCF.svc";
            //string url = "http://10.53.33.98/SGY.MessageService.Web/MessageServiceWCF.svc";
            string url = "http://211.155.17.217/WCF/st/SGY.MessageService.Web/MessageServiceWCF.svc";
            EndpointAddress httpEndpointAddress = new EndpointAddress(url);
            ChannelFactory<IMessageServiceWCF> wsHttpFactory = new ChannelFactory<IMessageServiceWCF>(httpBinding, httpEndpointAddress);
            IMessageServiceWCF wsHttpChannel = wsHttpFactory.CreateChannel();

            string cusCiqNo = wsHttpChannel.GetCusCiqNo("0", "5100");
            XDocument declDoc = XDocument.Load(@"F:\Temp\DeclMsg\DECL_FILE.xml");


            //上载报文
            //MesReceipt res = wsHttpChannel.SendMessage("130409667935", "00-21-70-67-E8-27",
            //    cusCiqNo, declDoc.ToString());
             MesReceipt res = wsHttpChannel.SendMessage("130815300366", "00-21-70-67-E8-27",
                cusCiqNo, declDoc.ToString());
            Assert.AreEqual(string.Empty, res.Message);
            Assert.AreEqual<Boolean>(false, string.IsNullOrEmpty(res.MessagID));
            //上传报文（全）
            SaveModel model = wsHttpChannel.UploadAllData("130409667935", "00-21-70-67-E8-27", "1", "5100",
                cusCiqNo, 0, declDoc.ToString(), declDoc.ToString());
            SaveModel model2 = wsHttpChannel.UploadAllData("130409667935", "00-21-70-67-E8-27", "1", "5100",
                cusCiqNo, 0, declDoc.ToString(), declDoc.ToString());
            Assert.AreEqual<string>(model.Password, model2.Password);
            
            Assert.AreEqual<Boolean>(true, model.IsSuccess);
            //上传报关报文
            model = wsHttpChannel.UploadCustomsData("130409667935", "00-21-70-67-E8-27", "1", "5100",
                cusCiqNo, 0, declDoc.ToString());
            Assert.AreEqual<Boolean>(true, model.IsSuccess);
            //下载报关数据
            string cusMsg = wsHttpChannel.DownloadCustomsData("130409667935", "00-21-70-67-E8-27", "0130422510000024", "950496");
            Assert.IsNotNull(cusMsg);
            XDocument cusDoc = XDocument.Parse(cusMsg);
            Assert.AreEqual<string>("662025362", cusDoc.Root.Element("EnvelopBody").Element("DECL_HEAD").Element("PRE_ENTRY_ID").Value);
            //下载报检数据
            string ciqMsg = wsHttpChannel.DownloadCiqData("130409667935", "00-21-70-67-E8-27", "0130422510000024", "950496");
            Assert.IsNotNull(ciqMsg);
            XDocument ciqDoc = XDocument.Parse(ciqMsg);
            Assert.AreEqual<string>("662025362", ciqDoc.Root.Element("EnvelopBody").Element("DECL_HEAD").Element("PRE_ENTRY_ID").Value);
            //获得更新时间
            Assert.AreEqual(DateTime.Parse("2013-04-22 10:33:00"), wsHttpChannel.GetSaveTime("0130422510000024"));
            //登陆
            UserInfo user = wsHttpChannel.Login("jctest", "2157FC3FAD461F6A45834F07F8B0BA03");
            Assert.AreEqual("3c94fe4f-677d-4ffc-9922-9479bb784283", user.Guid);
            //修改密码           
            Assert.AreEqual<int>(1, wsHttpChannel.UpdatePassword("jctest", "2157FC3FAD461F6A45834F07F8B0BA03", "2157FC3FAD461F6A45834F07F8B0BA03"));
            //激活
            //Assert.AreEqual<int>(1, wsHttpChannel.ActiveKeyByLoginName("e", "E1671797C52E15F763380B45E841EC32", "130403162281", "BFEBFBFF0001067A"));
            //下载回执
            foreach (var cusRep in wsHttpChannel.ReceiveMsgRep("130409667935", "00-21-70-67-E8-27", "T1907843510020130223f4ff60b96"))
            {
                Assert.AreEqual<Boolean>(false, string.IsNullOrEmpty(cusRep.ReturnInfo));
            }
          

        
        
        }
    }
}
