using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GZCustoms.Application.SGY.Data;
using GZCustoms.Application.SGY.Data.Interface;
using GZCustoms.Application.SGY.Entity;

namespace GZCustoms.Application.SGY.MessageService.UnitTest
{
    [TestClass]
    public class DataUnitTest
    {
        [TestMethod]
        public void DataHelperTest()
        {
            IMessageDataHelper dataHelper = DataHelperFactory.GetMessageDataHelper();
            KeyInfo keyInfo = dataHelper.GetKeyInfo("130409667935", "00-21-70-67-E8-27");
            Assert.AreEqual("九城测试Key", keyInfo.KeyName);

            Assert.IsNotNull(dataHelper.GetMaxTcsCurrentId());
        }
    }
}
