using System;
using NUnit.Framework;

namespace IPGMMS.Tests.Controllers
{
    [TestFixture]
    public class MemberControllerTests : TestRoutes
    {
        [SetUp]
        public void SetUp()
        {

        }

        // Member/Index Tests

        [Test]
        public void Member_ShouldMapTo_Member_Index()
        {
            TestRouteMatch("~/Member", "Member", "Index");
        }

        [Test]
        public void MemberWithIndex_ShouldMapTo_Member_Index()
        {
            TestRouteMatch("~/Member/Index", "Member", "Index");
        }

        [Test]
        public void MemberWith_InvalidAction_ShouldFail()
        {
            TestRouteFail("~/Member/Cat");
        }

        // Haven't figured out how to test query strings

        [Test]
        public void MemberWith_Page2_ShouldMapTo_Member_Index_Page2()
        {
            TestRouteMatch("~/Member?page=2", "Member", "Index", new { page = 2 });
        }

        [Test]
        public void MemberWith_SlashPage2_ShouldMapTo_Member_Index_Page2()
        {
            TestRouteMatch("~/Member/?page=2", "Member", "Index", new { page = 2 });
        }

        [Test]
        public void MemberWithIndex_Page2_ShouldMapTo_Member_Index_Page2()
        {
            TestRouteMatch("~/Member/Index?page=2", "Member", "Index", new { page = 2 });
        }

        [Test]
        public void MemberWithIndex_SlashPage2_ShouldMapTo_Member_Index_Page2()
        {
            TestRouteMatch("~/Member/Index/?page=2", "Member", "Index", new { page = 2 });
        }

        [Test]
        public void MemberWith_Page0_ShouldMapTo_Member_Index()
        {
            TestRouteMatch("~/Member?page=0", "Member", "Index");
        }

        [Test]
        public void MemberWith_Page6_ShouldMapTo_Member_Index()
        {
            TestRouteMatch("~/Member?page=6", "Member", "Index");
        }

        // Member/Details Tests

        [Test]
        public void MemberDetails_WithoutID_ShouldMapTo_Member_Index()
        {
            TestRouteMatch("~/Member/Details", "Member", "Index");
        }

        [Test]
        public void MemberDetails_WithValidID_ShouldMapTo_MemberDetailsID()
        {
            TestRouteMatch("~/Member/Details/2", "Member", "Details", new { id = "2" });
        }

        [Test]
        public void MemberDetails_WithInvalidID_ShouldMapTo_Member_Index()
        {
            TestRouteMatch("~/Member/Details/99", "Member", "Index");
        }
    }
}
