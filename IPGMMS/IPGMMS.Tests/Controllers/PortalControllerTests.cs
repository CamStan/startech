using NUnit.Framework;

namespace IPGMMS.Tests.Controllers
{
    [TestFixture]
    class PortalControllerTests : TestRoutes
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Portal_DefaultURL_ShouldMapTo_Portal()
        {
            TestRouteMatch("~/Portal/Index", "Portal", "Index");
        }

        [Test]
        public void Portal_DefaultURL_ShouldMapTo_Portal_noIndex()
        {
            TestRouteMatch("~/Portal", "Portal", "Index");
        }

        [Test]
        public void PortalMember_Page0_ShouldMapTo_Portal_ListMembers()
        {
            TestRouteMatch("~/Portal/ListMembers", "Portal", "ListMembers");
        }

        // Not sure if we can actually test the next 2 with the default tests.
        [Test]
        public void Portal_Member_Page1_ShouldMapTo_Portal_ListMembers_Page1()
        {
            TestRouteMatch("~/Portal/ListMembers?page=1", "Portal", "ListMembers");
        }
        // Probably need to write another test.
        [Test]
        public void Portal_Member_Page2_ShouldMapTo_Portal_ListMembers_Page2()
        {
            TestRouteMatch("~/Portal/ListMembers?page=2", "Portal", "ListMember");
        }

        [Test]
        public void Portal_DetailMember_That_We_Dont_Use()
        {
            TestRouteMatch("~/Portal/DetailMember", "Portal", "DetailMember");
        }

        [Test]
        public void Portal_AddMember_ShouldMapTo_Portal_AddMember()
        {
            TestRouteMatch("~/Portal/AddMember", "Portal", "AddMember");
        }

        [Test]
        public void Portal_AddMemberPost_SholdMapTo_Portal_AddMember()
        {
            TestRouteMatch("~/Portal/AddMember", "Portal", "AddMember", null, "Post");
        }

        [Test]
        public void Portal_UpdateMember_ShouldMapTo_Portal_UpdateMember()
        {
            TestRouteMatch("~/Portal/UpdateMember", "Portal", "UpdateMember");
        }

        [Test]
        public void Portal_UpdateMemberInfo_ShouldMapTo_Portal_UpdateMemberInfo()
        {
            TestRouteMatch("~/Portal/UpdateMemberInfo", "Portal", "UpdateMemberInfo");
        }
        // This test might be valid. On successful update it should redirect to UpdateMember
        // If update fail, it returns to UpdateMemberInfo
        [Test]
        public void Portal_UpdatememberInfo_ShouldMapTo_Portal_UpdateMember()
        {
            TestRouteMatch("~/Portal/UpdateMemberInfo", "Portal", "UpdateMemberInfo", null, "Post");
        }

        [Test]
        public void Portal_UpdateMemberMailing_ShouldMapTo_Portal_UpdateMemberMailing()
        {
            TestRouteMatch("~/Portal/UpdateMemberMailing", "Portal", "UpdateMemberMailing");
        }

        [Test]
        public void Portal_UpdateMemberMailing_Post_ShouldMapTo_Portal_UpdateMemberMailing()
        {
            TestRouteMatch("~/Portal/UpdateMemberMailing", "Portal", "UpdateMemberMailing", null, "Post");
        }

        [Test]
        public void Portal_UpdateMemberListing_ShouldMapTo_Portal_UpdateMemberListing()
        {
            TestRouteMatch("~/Portal/UpdateMemberListing", "Portal", "UpdateMemberListing");
        }

        [Test]
        public void Portal_UpdateMemberListing_Post_ShouldMapTo_Portal_UpdateMemberListing()
        {
            TestRouteMatch("~/Portal/UpdateMemberListing", "Portal", "UpdateMemberListing", null, "Post");
        }

        [Test]
        public void Portal_ListTests_ShouldMapTo_Portal_Listtests()
        {
            TestRouteMatch("~/Portal/ListTests", "Portal", "ListTests");
        }

        [Test]
        public void Portal_AddTest_ShouldMapTo_Portal_AddTest()
        {
            TestRouteMatch("~/Portal/AddTest", "Portal", "AddTest");
        }

        [Test]
        public void Portal_ListCertifications_ShouldMapTo_Portal_ListCertifications()
        {
            TestRouteMatch("~/Portal/ListCertifications", "Portal", "ListCertifications");
        }

        [Test]
        public void Portal_DetailCertification_ShouldMapTo_Portal_DetailCertification()
        {
            TestRouteMatch("~/Portal/DetailCertification", "Portal", "DetailCertification");
        }

        [Test]
        public void Portal_AddCertification_ShouldMapTo_Portal_AddCertification()
        {
            TestRouteMatch("~/Portal/AddCertification", "Portal", "AddCertification");
        }

        [Test]
        public void Portal_UpdateCertification_ShouldMapTo_Portal_UpdateCertification()
        {
            TestRouteMatch("~/Portal/UpdateCertification", "Portal", "UpdateCertification");
        }

        [TearDown]
        public void Teardown()
        {
            // Clean up so futuretests have a clean environment to run in
        }

    }
}
