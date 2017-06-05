using System;
using Moq;
using NUnit.Framework;
using IPGMMS.Abstract;
using IPGMMS.Models;
using IPGMMS.Controllers;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using IPGMMS.DAL;
using System.Data.Entity;
using System.Linq;
using IPGMMS.DAL.Repositories;
using IPGMMS.ViewModels;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;

namespace IPGMMS.Tests.DependencyTests
{

    [TestFixture]
    public class ManageTests
    {
        private Mock<IMemberRepository> memberMock;
        private Mock<IContactRepository> contactMock;
        private Mock<IPrincipal> mockPrincipal;
        private Mock<ControllerContext> mockContext;

        [SetUp]
        public void SetupManageMock()
        {

            memberMock = new Mock<IMemberRepository>();
            contactMock = new Mock<IContactRepository>();
            mockPrincipal = new Mock<IPrincipal>();
            mockContext = new Mock<ControllerContext>();

            contactMock.Setup(m => m.Find(1))
            .Returns(
                new ContactInfo
                {
                    ID = 1,
                    StreetAddress = "111 SW St",
                    City = "Monmouth",
                    StateName = "OR",
                    Country = "USA",
                    PostalCode = "97361",
                    PhoneNumber = "5555555555",
                    Email = "Some@mail.com",
                });


            memberMock.Setup(m => m.FindByIdentityID("userName"))
                .Returns(
                new Member
                {
                    ID = 1,
                    Membership_Number = "0101010",
                    FirstName = "Bill",
                    LastName = "Saw",
                    Identity_ID = "userName",
                    Contacts = new List<Contact>
                    {
                        new Contact
                            { ID = 1, ContactInfo = new ContactInfo { ID = 1, Country = "US" }, ContactType = new ContactType { ContactType1 = "Mailing" } },
                        new Contact
                            { ID = 2, ContactInfo = new ContactInfo { ID = 2, Country = "US" }, ContactType = new ContactType { ContactType1 = "Listing" } }
                    }
                });

                        memberMock.Setup(m => m.FindByIdentityID("userName2"))
                .Returns(
                new Member
                {
                    ID = 2,
                    Membership_Number = "0202020",
                    FirstName = "Jane",
                    LastName = "Doe",
                    Identity_ID = "userName2",
                    Contacts = new List<Contact>
                    {
                        new Contact
                            { ID = 3, ContactInfo = new ContactInfo { ID = 3, Country = "US" }, ContactType = new ContactType { ContactType1 = "Listing" } },
                        new Contact
                            { ID = 4, ContactInfo = new ContactInfo { ID = 4, Country = "US" }, ContactType = new ContactType { ContactType1 = "Mailing" } }
                    }
                });
        }

        [Test]
        // Test if UpdateMyInfo() finds the proper member id from User.
        public void Manage_Test_UpdateMyInfo_Valid()
        {
            // This is the Identity Name
            var identity = new GenericIdentity("userName", "");
            // This is the Identity ID
            var nameidentifierClaim = new Claim(ClaimTypes.NameIdentifier, "userName");
            identity.AddClaim(nameidentifierClaim);
            mockPrincipal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            mockPrincipal.Setup(x => x.Identity).Returns(identity);
            mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            // This is needed to get the User into the Mock environment.
            mockContext.SetupGet(x => x.HttpContext.User).Returns(mockPrincipal.Object);

            var controller = new ManageController(memberMock.Object, contactMock.Object);
            controller.ControllerContext = mockContext.Object;

            var result = controller.UpdateMyInfo() as ViewResult;
            var member = (Member)result.ViewData.Model;

            Assert.AreEqual(member.ID, 1);
            Assert.AreEqual(member.Identity_ID, "userName");
        }

        [Test]
        // Tests if UpdateContact returns valid contactinfo object with the
        // mailing info first.
        public void Manage_Test_UpdateContact_Valid()
        {
            // This is the Identity Name
            var identity = new GenericIdentity("userName", "");
            // This is the Identity ID
            var nameidentifierClaim = new Claim(ClaimTypes.NameIdentifier, "userName");
            identity.AddClaim(nameidentifierClaim);
            mockPrincipal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            mockPrincipal.Setup(x => x.Identity).Returns(identity);
            mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            // This is needed to get the User into the Mock environment.
            mockContext.SetupGet(x => x.HttpContext.User).Returns(mockPrincipal.Object);

            var controller = new ManageController(memberMock.Object, contactMock.Object);
            controller.ControllerContext = mockContext.Object;

            var result = controller.UpdateContact("ListingInfo") as ViewResult;
            var contact = (ContactInfo)result.ViewData.Model;

            Assert.AreEqual(contact.ID, 2);
            Assert.AreEqual(contact.Country, "US");

        }

        [Test]
        // Tests if UpdateContact returns valid contactinfo object with the 
        // listing info first
        public void Manage_Test_UpdateContact_ContactInfo_Reveresed_Valid()
        {
            // This is the Identity Name
            var identity = new GenericIdentity("userName2", "");
            // This is the Identity ID
            var nameidentifierClaim = new Claim(ClaimTypes.NameIdentifier, "userName2");
            identity.AddClaim(nameidentifierClaim);
            mockPrincipal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            mockPrincipal.Setup(x => x.Identity).Returns(identity);
            mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            // This is needed to get the User into the Mock environment.
            mockContext.SetupGet(x => x.HttpContext.User).Returns(mockPrincipal.Object);

            var controller = new ManageController(memberMock.Object, contactMock.Object);
            controller.ControllerContext = mockContext.Object;

            var result = controller.UpdateContact("ListingInfo") as ViewResult;
            var contact = (ContactInfo)result.ViewData.Model;

            Assert.AreEqual(contact.ID, 3);
            Assert.AreEqual(contact.Country, "US");

        }

        //[Test]
        // Tests if contact ID is null, should return to home/index page
        //public void Manage_Test_UpdateContact_Invalid()
        //{
        //    var controller = new ManageController(memberMock.Object, contactMock.Object);

        //    // Need the (int?) conversion to resolve ambiguity of UpdateContact method overload
        //    // There's also a post method that takes a ContactInfo object.
        //    // This just tests the get method.
        //    var result = controller.UpdateContact((int?)null);

        //    // We make sure that it is of the right type.
        //    Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());

        //    // Cast it as that type for proper object values.
        //    RedirectToRouteResult resultCast = (RedirectToRouteResult)result;

        //    // RouteValues is a dictionary, check the key.
        //    // "Index" is what we want it to be. RouteValues["action"] is the key
        //    Assert.AreEqual("Index", resultCast.RouteValues["action"].ToString());
        //    Assert.AreEqual("Home", resultCast.RouteValues["controller"].ToString());
        //}

        //[Test]
        //// Test to check if UpdateContact returns to Manage Index after update.
        //public void Manage_Test_UpdateContact_Successful()
        //{
        //    var controller = new ManageController(memberMock.Object, contactMock.Object);
        //    var result = controller.UpdateContact(1) as ViewResult;
        //    var contact = (ContactInfo)result.ViewData.Model;

        //    var result2 = controller.UpdateContact(contact);

        //    // Make sure that it is of the right type.
        //    Assert.That(result2, Is.InstanceOf<RedirectToRouteResult>());

        //    // Cast it as that type for proper object values
        //    RedirectToRouteResult result2Cast = (RedirectToRouteResult)result2;

        //    // RouteValues is a dictionary, check the key.
        //    // "Index" is what we want it to be. RouteValues["action"] is the key
        //    Assert.AreEqual("Index", result2Cast.RouteValues["action"].ToString());
        //}
    }
}
