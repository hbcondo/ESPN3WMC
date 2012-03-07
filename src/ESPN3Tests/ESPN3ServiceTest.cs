using ESPN3Library;
using ESPN3Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ESPN3Tests.wsESPN3;

namespace ESPN3Tests
{
    /// <summary>
    ///This is a test class for ESPN3ServiceTest and is intended
    ///to contain all ESPN3ServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ESPN3ServiceTest
    {


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
        ///A test for GetListings
        ///</summary>
        public void GetListingsTest()
        {
            ESPN3Library.ESPN3Service target = new ESPN3Library.ESPN3Service(); // TODO: Initialize to an appropriate value
            //ESPN3Library.Models.Listings expected = null; // TODO: Initialize to an appropriate value
            List<ESPN3Library.Models.Match> actual = target.GetListings(ESPN3Library.Models.SportConstants.TENNIS);
            Assert.AreNotEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetSports
        ///</summary>
        [TestMethod()]
        public void GetSportsTest()
        {
            ESPN3Library.ESPN3Service target = new ESPN3Library.ESPN3Service();
            System.Collections.Generic.List<ESPN3Library.Models.Sport> actual;
            actual = target.GetSports();
            Assert.AreNotEqual(0, actual.Count);
        }

        [TestMethod]
        public void PageTest()
        {
            ESPN3Library.ESPN3Service blah = new ESPN3Service();
            string returner = blah.GetVideoPageHTML();
        }

        [TestMethod]
        public void WebServiceTest()
        {
            wsESPN3.Service service = new wsESPN3.Service();

            // set authentication key for service
            UserCredentials uc = new UserCredentials();
            uc.AuthenticationKey = ESPN3Library.Properties.Settings.Default.WMCAuthorizationKey;
            service.UserCredentialsValue = uc;
            service.PreAuthenticate = true;

            ESPN3Tests.wsESPN3.ListingsResponse listings = service.GetListingsForWMCV1();

            Assert.IsNotNull(listings);
        }

        [TestMethod]
        public void GetEventsTest()
        {
            ESPN3Library.ESPN3Service target = new ESPN3Library.ESPN3Service();
            List<ESPN3Library.Models.Sport> sports = target.GetEvents();
        }

        /// <summary>
        ///A test for GetESPN3Events
        ///</summary>
        [TestMethod()]
        public void GetESPN3EventsTest()
        {
            ESPN3Library.ESPN3Service target = new ESPN3Library.ESPN3Service(); // TODO: Initialize to an appropriate value
            List<ESPN3Library.Models.Match> actual = target.GetESPN3Events();
            Assert.AreNotEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetRandomNumber
        ///</summary>
        [TestMethod()]
        public void GetRandomNumberTest()
        {
            ESPN3Library.ESPN3Service target = new ESPN3Library.ESPN3Service(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetRandomNumber();
            Assert.AreEqual(11, actual.Length);
        }

		[TestMethod()]
		public void GetRawXMLTest()
		{
			ESPN3Library.ESPN3Service target = new ESPN3Service();
			System.Xml.XmlDocument x = target.GetRawEventData("espn.go.com/watchespn/feeds/startup?action=live&channel=espn3&rand=32123060478");

			Assert.IsNotNull(x);
		}
    }
}
