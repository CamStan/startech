using NUnit.Framework;



namespace IPGMMS.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests : TestRoutes
    {

        [SetUp]
        public void SetUp()
        {
    
        }

        [Test]
        public void DefaultURL_ShouldMapTo_Home_Index()
        {
            TestRouteMatch("~/", "Home", "Index");
        }

        [Test]
        public void DefaultURLWithHome_ShouldMapTo_Home_Index()
        {
            TestRouteMatch("~/Home", "Home", "Index");
        }

        [Test, Description("About this application page")]
        public void About_WithHome_ShouldMapTo_Home_About()
        {
            TestRouteMatch("~/Home/About", "Home", "About");
        }

        [Test, Description("About this application page")]
        public void About_ShouldMapTo_Home_About()
        {
            TestRouteMatch("~/About", "Home", "About");
        }

        [Test]
        public void Contact_WithHome_ShouldMapTo_Home_Contact()
        {
            TestRouteMatch("~/Home/Contact", "Home", "Contact");
        }

        [Test]
        public void Contact_ShouldMapTo_Home_Contact()
        {
            TestRouteMatch("~/Contact", "Home", "Contact");
        }

        [Test]
        public void FAQ_WithHome_ShouldMapTo_Home_FAQ()
        {
            TestRouteMatch("~/Home/FAQ", "Home", "FAQ"); 
        }

        [Test]
        public void FAQ_ShouldMapTo_Home_FAQ()
        {
            TestRouteMatch("~/FAQ", "Home", "FAQ");
        }

        [TearDown]
        public void TearDown()
        {
            // clean up so future tests have a clean environment to run in
        }
    }
}
