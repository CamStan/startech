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

        //[Test]
        //public void MemberWith_InvalidAction_ShouldFail()
        //{
        //    TestRouteFail("~/Member/Cat");
        //}

        // Tests fail because TestRouteMatch does not work for this type of query.
        //[Test]
        //public void MemberWith_Page0_ShouldMapTo_Member_Index()
        //{
        //    TestRouteMatch("~/Member?page=0", "Member", "Index");
        //}

        //[Test]
        //public void MemberWith_Page6_ShouldMapTo_Member_Index()
        //{
        //    TestRouteMatch("~/Member?page=6", "Member", "Index");
        //}

        // Member/Details Tests

        [Test]
        public void MemberDetails_WithoutID_ShouldMapTo_Details_BeforeReroute()
        {
            TestRouteMatch("~/Member/Details", "Member", "Details");
        }

        [Test]
        public void MemberDetails_WithValidID_ShouldMapTo_MemberDetailsID()
        {
            TestRouteMatch("~/Member/Details/2", "Member", "Details", new { id = "2" });
        }

        [Test]
        public void MemberDetails_WithInvalidID_ShouldMapTo_Details_BeforeReroute()
        {
            TestRouteMatch("~/Member/Details/99", "Member", "Details",  new { id = "99" });
        }
    }
}
