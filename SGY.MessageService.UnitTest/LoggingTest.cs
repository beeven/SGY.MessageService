using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GZCustoms.Application.SGY.Logging;


namespace GZCustoms.Application.SGY.MessageService.UnitTest
{
    [TestClass]
    public class LoggingTest
    {
        [TestMethod]
        public void LogTest()
        {
            LogHelper helper = LogHelper.GetInstance();
            helper.LogErrInfo("TestErr", 120, "TestTile");

        }
    }
}
