using System;
using Moq;
using NUnit.Framework;
using IPGMMS.Models;
using System.Collections.Generic;
using IPGMMS.DAL;
using System.Data.Entity;
using System.Linq;
using IPGMMS.DAL.Repositories;
using IPGMMS.Abstract;

namespace IPGMMS.Tests.DependencyTests
{
    [TestFixture]
    public class ContactTests
    {
        private Mock<DbSet<ContactInfo>> dbSetMock;
        private Mock<IPGMMS_Context> dbMock;

        [SetUp]
        public void SetupContactMock()
        {
            // setup things in dbContext to test
            var data = new List<ContactInfo>
            {
               new ContactInfo { ID = 1, StreetAddress = "123 Galaxy Way", City = "Mars Town", StateName = "OR", PostalCode = "97306"},
               new ContactInfo { ID = 2, StreetAddress = "57438", City = "Yondu", StateName = "WA", PostalCode = "45867"},
               new ContactInfo { ID = 3, StreetAddress = "4567 Guardian Lane", City = "Marvel", StateName = "OR", PostalCode = "97209"},

            }.AsQueryable();

            dbSetMock = new Mock<DbSet<ContactInfo>>();
            dbSetMock.As<IQueryable<ContactInfo>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.As<IQueryable<ContactInfo>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.As<IQueryable<ContactInfo>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.As<IQueryable<ContactInfo>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            dbMock = new Mock<IPGMMS_Context>();
            dbMock.Setup(d => d.ContactInfoes).Returns(dbSetMock.Object);

        }

        //This test verifies that the mapURL is formatted properly from the ContactInfo model when
        //there is no address. It should format a string that ends in q=, - this will render a map 
        //of the world when there is no listing information for the member.
        [Test]
        public void Verify_mapURL_NoAddress()
        {
            ContactInfo contact = new ContactInfo();
            string mapURL_noAddress = contact.mapURL;

            string mapURL_ShouldBe = "https://www.google.com/maps/embed/v1/search?key=AIzaSyB75sLTv_4MR8DnNb8PptRe9Acvh9vNzqI&q=,";

            Assert.AreEqual(mapURL_noAddress, mapURL_ShouldBe);
        }
        //This test verifies that the mapURL is formatted properly from the ContactInfo model.
        [Test]
        public void Verify_mapURL_Correct()
        {
            ContactInfo contact = new ContactInfo { ID = 1, StreetAddress = "123 Galaxy Way", City = "Mars Town", StateName = "OR", PostalCode = "97306" };
            string mapURL = contact.mapURL;

            string correctMapURL = "https://www.google.com/maps/embed/v1/search?key=AIzaSyB75sLTv_4MR8DnNb8PptRe9Acvh9vNzqI&q=123+Galaxy+Way,Mars+Town+OR+97306";

            Assert.AreEqual(mapURL, correctMapURL);
        }

        [Test]
        public void Find_Contact_Info()
        {
            ContactInfo contact = new ContactInfo { ID = 1, StreetAddress = "123 Galaxy Way", City = "Mars Town", StateName = "OR", PostalCode = "97306" };
            dbMock.Setup(c => c.ContactInfoes.Find(1)).Returns(contact);

            EFContactRepository repo = new EFContactRepository(dbMock.Object);

            ContactInfo contactInfo = repo.Find(1);

            Assert.AreEqual(contactInfo.StreetAddress, "123 Galaxy Way");
        }

        [Test]
        public void Contact_Info_Not_Found()
        {
            dbMock.Setup(c => c.ContactInfoes.Find(10)).Returns((ContactInfo)null);
            EFContactRepository repo = new EFContactRepository(dbMock.Object);

            ContactInfo contactInfo = repo.Find(10);

            Assert.IsNull(contactInfo);
        }
    }
}
