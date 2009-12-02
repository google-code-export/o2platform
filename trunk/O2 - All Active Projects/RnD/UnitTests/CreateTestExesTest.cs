using System;
using System.IO;

using NUnit.Framework;
using O2.External.O2Mono.MonoCecil;


namespace O2.RnD.AspectTests.UnitTests
{
    /// <summary>
    ///This is a test class for CreateTestExesTest and is intended
    ///to contain all CreateTestExesTest Unit Tests
    ///</summary>
    [TestFixture]
    public class CreateTestExesTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>        

        #region Additional test attributes

        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion

        /// <summary>
        ///A test for createBasicHelloWorldExe
        ///</summary>
        [Test]
        public void createBasicHelloWorldExeTest()
        {
            var sResponse = new CreateTestExe().createBasicHelloWorldExe().save();
            Assert.IsTrue(File.Exists(sResponse), "File Does not exist: {0}", sResponse);
        }
    }
}