using System;
using Moq;
using NUnit.Framework;
using IPGMMS.Abstract;
using IPGMMS.Models;
using IPGMMS.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using IPGMMS.DAL;
using System.Data.Entity;
using System.Linq;
using IPGMMS.DAL.Repositories;

namespace IPGMMS.Tests.DependencyTests
{
    class PortalMemberTests
    {
        private Mock<IMemberRepository> memberMock;
        private Mock<IContactRepository> contactMock;
        private Mock<DbSet<Member>> dbSetMock;
        private Mock<DbSet<ContactInfo>> dbSetConMock;
        private Mock<DbSet<Contact>> dbSetBridgeMock;
        private Mock<IPGMMS_Context> dbMock;

        [SetUp]
        public void SetupMemberMock()
        {
            // setup things in memberRepo to test
            memberMock = new Mock<IMemberRepository>();

            // setup things in contactRepo to test
            contactMock = new Mock<IContactRepository>();
            contactMock.Setup(c => c.MailingInfoFromMID(1)).Returns(new ContactInfo { ID = 1, Country = "USA", City = "Salem" });
            contactMock.Setup(c => c.ListingInfoFromMID(2)).Returns(new ContactInfo { ID = 2, Country = "UK", City = "London" });

            // setup things in dbContext to test
            var data = new List<Member>
            {
                new Member {ID = 1, FirstName = "Wolverine", Membership_Number = "0111123", Identity_ID = "ABC123" },
                new Member {ID = 2, FirstName = "Storm", Membership_Number = "0211345", Identity_ID = "DEF456" },
                new Member {ID = 3, FirstName = "Rogue", Membership_Number = "0311456", Identity_ID = "GHI789" }
            }.AsQueryable();

            dbSetMock = new Mock<DbSet<Member>>();
            dbSetMock.As<IQueryable<Member>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.As<IQueryable<Member>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.As<IQueryable<Member>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.As<IQueryable<Member>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var cData = new List<ContactInfo>
            {
                new ContactInfo {ID = 1, Country = "USA", City = "Salem" },
                new ContactInfo { ID = 2, Country = "UK" , City = "London"}
            }.AsQueryable();

            dbSetConMock = new Mock<DbSet<ContactInfo>>();
            dbSetConMock.As<IQueryable<ContactInfo>>().Setup(m => m.Provider).Returns(cData.Provider);
            dbSetConMock.As<IQueryable<ContactInfo>>().Setup(m => m.Expression).Returns(cData.Expression);
            dbSetConMock.As<IQueryable<ContactInfo>>().Setup(m => m.ElementType).Returns(cData.ElementType);
            dbSetConMock.As<IQueryable<ContactInfo>>().Setup(m => m.GetEnumerator()).Returns(() => cData.GetEnumerator());


            var bridge = new List<Contact>
            {
                new Contact { ID = 1, ContactInfo_ID = 1, Member_ID = 1, ContactType_ID = 1},
                new Contact {ID = 2, ContactInfo_ID = 2, Member_ID = 2 , ContactType_ID = 2}
            }.AsQueryable();

            dbSetBridgeMock = new Mock<DbSet<Contact>>();
            dbSetBridgeMock.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(bridge.Provider);
            dbSetBridgeMock.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(bridge.Expression);
            dbSetBridgeMock.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(bridge.ElementType);
            dbSetBridgeMock.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(() => bridge.GetEnumerator());


            dbMock = new Mock<IPGMMS_Context>();
            dbMock.Setup(d => d.Members).Returns(dbSetMock.Object);
            dbMock.Setup(e => e.ContactInfoes).Returns(dbSetConMock.Object);
            dbMock.Setup(f => f.Contacts).Returns(dbSetBridgeMock.Object);
        }

        [Test]
        public void TestNewMemberNumber_MemberHasNumber()
        {
            EFMemberRepository repo = new EFMemberRepository(dbMock.Object);

            string s = repo.setMemberNumber(
                new Member { ID = 1, FirstName = "Wolverine", Membership_Number = "0100123", Identity_ID = "ABC123" },
                new ContactInfo { ID = 1, Country = "USA", City = "Salem" });
            s = s.Substring(0, s.Length - 1);
            Assert.AreEqual("0111123", s);
        }

        [Test]
        public void TestNewMemberNumber_MemberDoesNotHaveNumber()
        {
            EFMemberRepository repo = new EFMemberRepository(dbMock.Object);

            string s = repo.setMemberNumber(
                new Member { ID = 1, FirstName = "Wolverine", Membership_Number = null, Identity_ID = "ABC123" },
                new ContactInfo { ID = 1, Country = "USA", City = "Salem" });
            s = s.Substring(0, s.Length - 1);
            Assert.AreEqual("0111123", s);
        }

        [Test]
        public void TestUpdateMemberMailing_Get()
        {
            PortalController controller = new PortalController(memberMock.Object, contactMock.Object);

            var contact = (ViewResult)controller.UpdateMemberMailing(1);

            ContactInfo c = (ContactInfo)contact.ViewData.Model;

            Assert.AreEqual("Salem", c.City);
        }


        [Test]
        public void TestUpdateMemberMailing_Get_Model()
        {
            PortalController controller = new PortalController(memberMock.Object, contactMock.Object);

            var contact = (ViewResult)controller.UpdateMemberMailing(1);


            Assert.IsNotNull(contact.Model);
        }


        [Test]
        public void TestUpdateMemberMailing_Get_Bad()
        {
            PortalController controller = new PortalController(memberMock.Object, contactMock.Object);

            var contact = (ViewResult)controller.UpdateMemberMailing(3);


            Assert.IsNull(contact.Model);
        }

        [Test]
        public void TestUpdateMemberListing_Get()
        {
            PortalController controller = new PortalController(memberMock.Object, contactMock.Object);

            var contact = (ViewResult)controller.UpdateMemberListing(2);

            ContactInfo c = (ContactInfo)contact.ViewData.Model;

            Assert.AreEqual("London", c.City);
        }


        [Test]
        public void TestUpdateMemberListing_Get_Model()
        {
            PortalController controller = new PortalController(memberMock.Object, contactMock.Object);

            var contact = (ViewResult)controller.UpdateMemberListing(2);


            Assert.IsNotNull(contact.Model);
        }

        [Test]
        public void TestUpdateMemberListing_Get_Bad()
        {
            PortalController controller = new PortalController(memberMock.Object, contactMock.Object);

            var contact = (ViewResult)controller.UpdateMemberListing(3);


            Assert.IsNull(contact.Model);
        }


    }
}
