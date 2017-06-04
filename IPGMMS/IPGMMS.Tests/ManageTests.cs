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

namespace IPGMMS.Tests.DependencyTests
{

    [TestFixture]
    public class ManageTests
    {
        private Mock<IMemberRepository> memberMock;
        private Mock<IContactRepository> contactMock;

        [SetUp]
        public void SetupManageMock()
        {

            memberMock = new Mock<IMemberRepository>();
            contactMock = new Mock<IContactRepository>();

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
            
        }

        //[Test]
        // Tests if UpdateContact returns valid contactinfo object
        //public void Manage_Test_UpdateContact_Valid()
        //{
        //    var controller = new ManageController(memberMock.Object, contactMock.Object);
        //    var result = controller.UpdateContact(1) as ViewResult;
        //    var contact = (ContactInfo)result.ViewData.Model;

        //    Assert.AreEqual(contact.ID, 1);
        //    Assert.AreEqual(contact.Country, "USA");

        //}

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
