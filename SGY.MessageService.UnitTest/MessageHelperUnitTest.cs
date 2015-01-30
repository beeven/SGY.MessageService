using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GZCustoms.Application.SGY.MessageService;
using GZCustoms.Application.SGY.MessageService.Interface;
using GZCustoms.Application.SGY.Entity;



namespace GZCustoms.Application.SGY.MessageService.UnitTest
{
    [TestClass]
    public class MessageHelperUnitTest
    {
        [TestMethod]
        public void SendMessageTest()
        {
            MessageServiceHelper helper = new MessageServiceHelper();           
            string cusCiqNo = helper.GetCusCiqNo("0", "5100");
            XDocument declDoc = XDocument.Parse(Constants.ClientMessage);
            MesReceipt res = helper.SendMessage(Constants.ClientKey, Constants.ClientMachineCode, cusCiqNo, declDoc.ToString());
            Assert.AreEqual(string.Empty, res.Message);  
            Assert.AreEqual<Boolean>(false, string.IsNullOrEmpty(res.MessagID));
        }

        [TestMethod]
        public void GetCusCiqNoTest()
        {
            MessageServiceHelper helper = new MessageServiceHelper();
            string cusCiqNo = helper.GetCusCiqNo("0", "5100");
            Assert.IsNotNull(cusCiqNo);
            Assert.AreEqual<int>(16, cusCiqNo.Length);            
        }

        [TestMethod]
        public void UploadAllDataTest()
        {
            MessageServiceHelper helper = new MessageServiceHelper();
            string cusCiqNo = helper.GetCusCiqNo("0", "5100");
            XDocument declDoc = XDocument.Load(@"F:\Temp\DeclMsg\DECL_FILE.xml");
            SaveModel model = helper.UploadAllData("130409667935", "00-21-70-67-E8-27", "1", "5100", cusCiqNo, 2, declDoc.ToString(), declDoc.ToString());
            Assert.AreEqual<Boolean>(true, model.IsSuccess);
        }

        [TestMethod]
        public void UploadCusTomsDatatest()
        {
            MessageServiceHelper helper = new MessageServiceHelper();
            string cusCiqNo = helper.GetCusCiqNo("0", "5100");
            XDocument declDoc = XDocument.Load(@"F:\Temp\DeclMsg\DECL_FILE.xml");
            SaveModel model = helper.UploadCusTomsData("130409667935", "00-21-70-67-E8-27", "1", "5100", cusCiqNo, 2, declDoc.ToString());
            Assert.AreEqual<Boolean>(true, model.IsSuccess);
        }

        [TestMethod]
        public void DownloadCustomsDataTest()
        {
            MessageServiceHelper helper = new MessageServiceHelper();
            string msg = helper.DownloadCustomsData("130409667935", "00-21-70-67-E8-27", "0130422510000024", "950496");
            Assert.IsNotNull(msg);
            XDocument doc = XDocument.Parse(msg);
            Assert.AreEqual<string>("662025362", doc.Root.Element("EnvelopBody").Element("DECL_HEAD").Element("PRE_ENTRY_ID").Value);
           
        }

        [TestMethod]
        public void DownloadCiqDataTest()
        {
            MessageServiceHelper helper = new MessageServiceHelper();
            string msg = helper.DownloadCiqData("130409667935", "00-21-70-67-E8-27", "0130422510000024", "950496");
            Assert.IsNotNull(msg);
            XDocument doc = XDocument.Parse(msg);
            Assert.AreEqual<string>("662025362", doc.Root.Element("EnvelopBody").Element("DECL_HEAD").Element("PRE_ENTRY_ID").Value);
        }

        [TestMethod]
        public void GetSaveTimeTest()
        {
            MessageServiceHelper helper = new MessageServiceHelper();
            Assert.AreEqual(DateTime.Parse("2013-04-22 10:33:28.000"), 
                helper.GetSaveTime("0130422510000024"));

        }

        [TestMethod]
        public void LoginTest()
        {
            MessageServiceHelper helper = new MessageServiceHelper();
            UserInfo user = helper.Login("jctest", "2157FC3FAD461F6A45834F07F8B0BA03");
            Assert.AreEqual("3c94fe4f-677d-4ffc-9922-9479bb784283", user.Guid);
        }

        [TestMethod]
        public void UpdatePasswordTest()
        {
            MessageServiceHelper helper = new MessageServiceHelper();
            Assert.AreEqual<int>(1, helper.UpdatePassword("bbb", "B59C67BF196A4758191E42F76670CEBA", "2157FC3FAD461F6A45834F07F8B0BA03"));
        }

        [TestMethod]
        public void ActiveKey()
        {
            MessageServiceHelper helper = new MessageServiceHelper();
            //Assert.AreEqual<int>(1, helper.ActiveKey("9c190178-fc55-4973-bc21-e6048c128bcf", "130403162281", "BFEBFBFF0001067A"));
            Assert.AreEqual<int>(1, helper.ActiveKey("e", "E1671797C52E15F763380B45E841EC32", "130403162281", "BFEBFBFF0001067A"));
        }

        [TestMethod]
        public void ReceiveMsgRepTest()
        {
            MessageServiceHelper helper = new MessageServiceHelper();
            CusReturnInfo returnInfo = helper.ReceiveMsgRep("130409667935", "00-21-70-67-E8-27", "T1907843510020130223f4ff60b96").GetEnumerator().Current;
            Assert.AreEqual<Boolean>(false, string.IsNullOrEmpty(returnInfo.ReturnInfo));
        }
         
    }
}
