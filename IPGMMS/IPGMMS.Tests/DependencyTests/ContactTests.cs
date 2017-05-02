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
        private Mock<DbSet<Contact>> dbSetMock1;
        private Mock<DbSet<Member>> dbSetMock2;
        private Mock<IPGMMS_Context> dbMock;

        [SetUp]
        public void SetupContactMock()
        {
            //Set up ContactInfos to test***********************************************************************************
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

            //Set up Contacts to test************************************************************************************
            var contacts = new List<Contact>
            {
                new Contact { ID = 1, Member_ID = 1, ContactInfo_ID = 1, ContactType_ID = 2 },
                new Contact { ID = 2, Member_ID = 2,  ContactInfo_ID = 2, ContactType_ID = 1 },
                new Contact { ID = 3, Member_ID = 3, ContactInfo_ID = 3, ContactType_ID = 2 },
            }.AsQueryable();

            dbSetMock1 = new Mock<DbSet<Contact>>();
            dbSetMock1.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(contacts.Provider);
            dbSetMock1.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(contacts.Expression);
            dbSetMock1.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(contacts.ElementType);
            dbSetMock1.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(() => contacts.GetEnumerator());

            //Set up Members to test***************************************************************************************
            var members = new List<Member>
            {
                new Member {ID = 1, FirstName = "Thor", LastName = "Smith", Membership_Number = "03-456" },
                new Member {ID = 2, FirstName = "Star-Lord", LastName = "Johnson", Membership_Number = "04-567"},
                new Member {ID = 3, FirstName = "Groot", LastName = "Root", Membership_Number = "05-678"},
            }.AsQueryable();

            dbSetMock2 = new Mock<DbSet<Member>>();
            dbSetMock2.As<IQueryable<Member>>().Setup(m => m.Provider).Returns(members.Provider);
            dbSetMock2.As<IQueryable<Member>>().Setup(m => m.Expression).Returns(members.Expression);
            dbSetMock2.As<IQueryable<Member>>().Setup(m => m.ElementType).Returns(members.ElementType);
            dbSetMock2.As<IQueryable<Member>>().Setup(m => m.GetEnumerator()).Returns(() => members.GetEnumerator());

            //Wrap it all up*************************************************************************************************
            dbMock = new Mock<IPGMMS_Context>();
            dbMock.Setup(d => d.ContactInfoes).Returns(dbSetMock.Object);
            dbMock.Setup(d => d.Contacts).Returns(dbSetMock1.Object);
            dbMock.Setup(d => d.Members).Returns(dbSetMock2.Object);
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
            Console.WriteLine(mapURL);
            string correctMapURL = "https://www.google.com/maps/embed/v1/search?key=AIzaSyB75sLTv_4MR8DnNb8PptRe9Acvh9vNzqI&q=123+Galaxy+Way,Mars+Town+OR+97306+";

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

        [Test]
        public void Test_Get_MemberID_From_ContactInfo()
        {
            ContactInfo conInfo = new ContactInfo { ID = 1, StreetAddress = "123 Galaxy Way", City = "Mars Town", StateName = "OR", PostalCode = "97306" };
            
            EFContactRepository repo = new EFContactRepository(dbMock.Object);

            int memberNumber = repo.getMemberID(conInfo);
       
            Assert.AreEqual(1, memberNumber);    
        }

        [Test]
        public void Test_Does_Not_Retrieve_Incorrect_MemberID()
        {
            ContactInfo conInfo = new ContactInfo { ID = 1, StreetAddress = "123 Galaxy Way", City = "Mars Town", StateName = "OR", PostalCode = "97306" };

            EFContactRepository repo = new EFContactRepository(dbMock.Object);

            int memberNumber = repo.getMemberID(conInfo);

            Assert.AreNotEqual(memberNumber, 2);
        }
    }
}
