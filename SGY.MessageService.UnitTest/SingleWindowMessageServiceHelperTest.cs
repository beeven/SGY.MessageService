using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GZCustoms.Application.SGY.MessageService;
using FluentAssertions;

namespace GZCustoms.Application.SGY.MessageService.UnitTest
{
    /// <summary>
    /// Summary description for SingleWindowMessageServiceHelperTest
    /// </summary>
    [TestClass]
    public class SingleWindowMessageServiceHelperTest
    {

        readonly SingleWindowMessageServiceHelper target;
        public SingleWindowMessageServiceHelperTest()
        {
            //
            // TODO: Add constructor logic here
            //
            target = new SingleWindowMessageServiceHelper();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetCusCiqNum_ShouldReturnAValidSequence()
        {
            //
            // TODO: Add test logic here
            //
            var actual = target.GetCusCiqNo("1", "5106");

            actual.Should().NotBeNullOrEmpty();
            actual.Should().HaveLength(16);
            actual.Should().StartWith("1");
            
        }

        [TestMethod]
        [DeploymentItem(@"SGY.MessageService.UnitTest\msg.xml")]
        [DeploymentItem("Xml\\","Xml")]
        public void PostMessage_ShouldReturnAReceipt()
        {
            string msg = "";
            using (var reader = new System.IO.StreamReader("msg.xml"))
            {
                msg = reader.ReadToEnd();
            }
                
            string cusCiqNo = "0150617370400001";

            var actual = target.PostMessage(cusCiqNo, msg);
            actual.Status.Should().Be("000");
        }
    }
}
