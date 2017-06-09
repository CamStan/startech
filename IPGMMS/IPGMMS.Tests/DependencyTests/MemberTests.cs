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
using IPGMMS.ViewModels;
using System.Diagnostics;

namespace IPGMMS.Tests.DependencyTests
{
    [TestFixture]
    public class MemberTests
    {
        private Mock<IMemberRepository> memberMock;
        private Mock<IContactRepository> contactMock;
        private Mock<DbSet<Member>> dbSetMock;
        private Mock<IPGMMS_Context> dbMock;

        [SetUp]
        public void SetupMemberMock()
        {
            // setup things in memberRepo to test
            memberMock = new Mock<IMemberRepository>();
            memberMock.Setup(m => m.GetAllMembers)
                .Returns(
                new Member[]
                {
                    new Member {ID = 1, FirstName = "Tom", LastName = "Solomon"},
                    new Member {ID = 2, FirstName = "Dick", LastName = "Solomon" },
                    new Member {ID = 3, FirstName = "Harry", LastName = "Solomon" },
                    new Member {ID = 4, FirstName = "Liz", LastName = "Lemon"},
                    new Member {ID = 5, FirstName = "Jenna", LastName = "Maroney" },
                    new Member {ID = 6, FirstName = "Tracy", LastName = "Jordan" },
                    new Member {ID = 7, FirstName = "Diana", LastName = "Prince"},
                    new Member {ID = 8, FirstName = "Clark", LastName = "Kent" },
                    new Member {ID = 9, FirstName = "Bruce", LastName = "wayne" },
                    new Member {ID = 10, FirstName = "Sally", LastName = "Solomon"},
                    new Member {ID = 11, FirstName = "Mary", LastName = "Albright" }
                });
            

            // Wayne m.Find
            memberMock.Setup(m => m.Find(5))
                .Returns(
                    new Member
                    {
                        ID = 5,
                        FirstName = "Tom",
                        LastName = "Solomon",
                        MemberLevel = 2,
                        MemberLevel1 = new MemberLevel { MLevel = "IPG Member" }
                    });

            // setup things in contactRepo to test
            contactMock = new Mock<IContactRepository>();
            

            // Wayne m.ListingInfoFromMID
            contactMock.Setup(m => m.ListingInfoFromMID(5))
                .Returns(
                new ContactInfo
                {
                    StateName = "Colorado",
                    Country = "USA"
                });

            // setup things in dbContext to test
            var data = new List<Member>
            {
                new Member {ID = 1, FirstName = "Wolverine", Membership_Number = "0100123", Identity_ID = "ABC123",Membership_ExpirationDate=new DateTime(2016,6,7),MemberLevel=9 },
                new Member {ID = 2, FirstName = "Storm", Membership_Number = "0200345", Identity_ID = "DEF456",Membership_ExpirationDate=new DateTime(2017,6,17),MemberLevel=9 },
                new Member {ID = 3, FirstName = "Rogue", Membership_Number = "0300456", Identity_ID = "GHI789",Membership_ExpirationDate=new DateTime(2018,1,2),MemberLevel=1 }

            }.AsQueryable();

            dbSetMock = new Mock<DbSet<Member>>();
            dbSetMock.As<IQueryable<Member>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.As<IQueryable<Member>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.As<IQueryable<Member>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.As<IQueryable<Member>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            dbMock = new Mock<IPGMMS_Context>();
            dbMock.Setup(d => d.Members).Returns(dbSetMock.Object);
        }

        [Test]
        public void TestCorrectPaging()
        {
            MemberController controller = new MemberController(memberMock.Object, contactMock.Object);

            var mem = (ViewResult)controller.Index(2);
            List<Member> m = (List<Member>)mem.ViewData.Model;
            Member[] members = m.ToArray();

            Assert.IsTrue(members.Length == 2);
            Assert.AreEqual(members[0].FirstName, "Sally");
            Assert.AreEqual(members[1].FirstName, "Mary");
        }

        [Test]
        public void TestIncorrectPaging()
        {
            MemberController controller = new MemberController(memberMock.Object, contactMock.Object);

            var mem = (ViewResult)controller.Index(-1);
            List<Member> m = (List<Member>)mem.Model;
            Member[] members = m.ToArray();

            Assert.IsTrue(members.Length == 9);
            Assert.AreEqual(members[0].FirstName, "Tom");
            Assert.AreEqual(members[1].FirstName, "Dick");
            Assert.AreEqual(members[2].FirstName, "Harry");
            Assert.AreEqual(members[3].FirstName, "Liz");
            Assert.AreEqual(members[4].FirstName, "Jenna");
            Assert.AreEqual(members[5].FirstName, "Tracy");
            Assert.AreEqual(members[6].FirstName, "Diana");
            Assert.AreEqual(members[7].FirstName, "Clark");
            Assert.AreEqual(members[8].FirstName, "Bruce");
        }

        [Test]
        public void TestCorrectIPG_Number()
        {
            EFMemberRepository repo = new EFMemberRepository(dbMock.Object);

            Member mem = repo.FindByIPG_ID("0200345");

            Assert.AreEqual(mem.FirstName, "Storm");
        }

        [Test]
        public void TestIncorrectIPG_Number()
        {
            EFMemberRepository repo = new EFMemberRepository(dbMock.Object);

            Member mem = repo.FindByIPG_ID("0400321");

            Assert.IsNull(mem);
        }

        [Test]
        public void TestCorrectIdentity_Number()
        {
            EFMemberRepository repo = new EFMemberRepository(dbMock.Object);

            Member mem = repo.FindByIdentityID("GHI789");

            Assert.AreEqual(mem.FirstName, "Rogue");
        }

        [Test]
        public void TestIncorrectIdentity_Number()
        {
            EFMemberRepository repo = new EFMemberRepository(dbMock.Object);

            Member mem = repo.FindByIdentityID("JKL321");

            Assert.IsNull(mem);
        }

        // Wayne Member Details test
        [Test]
        public void TestCorrectDetails()
        {
            var controller = new MemberController(memberMock.Object, contactMock.Object);
            var result = controller.Details(5) as ViewResult;
            var memberDetails = (MemberDetails)result.ViewData.Model;
            Assert.AreEqual(memberDetails.FullName, "Tom Solomon");
            Assert.AreEqual(memberDetails.Contact.Country, "USA");
        }

        [Test]
        // If MemberID is invalid, default to a default profile.
        public void Test_Invalid_MemberID_Details()
        {
            var controller = new MemberController(memberMock.Object, contactMock.Object);
            var result = controller.Details(1) as ViewResult;
            var memberDetails = (MemberDetails)result.ViewData.Model;
            Assert.AreEqual(memberDetails.FullName, "Tom Solomon");
        }
        [Test]
        // If MemberID is null, default to a default profile.
        public void Test_Null_MemberID_Details()
        {
            var controller = new MemberController(memberMock.Object, contactMock.Object);
            var result = controller.Details(null) as ViewResult;
            var memberDetails = (MemberDetails)result.ViewData.Model;
            Assert.AreEqual(memberDetails.FullName, "Tom Solomon");
        }

        /*
        [Test]
        public void TestMemberNumberUpdate()
        {
            EFMemberRepository repo = new EFMemberRepository(dbMock.Object);

            Member mem = repo.Find(1);


        }*/
        /// <summary>
        /// Tests expired members in database mock object
        /// </summary>
        [Test]
        public void TestExpiredMemberReport()
        {
            EFMemberRepository repo = new EFMemberRepository(dbMock.Object);

            var ExpiredMembersReport = repo.ExpiredMembers.ToList();
            Console.WriteLine(ExpiredMembersReport);
            Assert.AreEqual(ExpiredMembersReport.Count,1);
        }
        /// <summary>
        /// Tests expiring members in database mock object
        /// </summary>
        [Test]
        public void TestExpiringMemberReport()
        {
            EFMemberRepository repo = new EFMemberRepository(dbMock.Object);

            var ExpiringMembersReport = repo.ExpiringMembers.ToList();
            Console.WriteLine(ExpiringMembersReport);
            Assert.AreEqual(ExpiringMembersReport.Count, 1);
        }
        /// <summary>
        /// Tests new members in database mock object
        /// </summary>
        [Test]
        public void TestNewMemberReport()
        {
            EFMemberRepository repo = new EFMemberRepository(dbMock.Object);

            var NewMembersReport = repo.NewMembers.ToList();
            
            Assert.AreEqual(NewMembersReport.Count, 2);
        }
    }
}
