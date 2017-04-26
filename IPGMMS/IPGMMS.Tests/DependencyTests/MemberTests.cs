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
                    new Member {ID = 1, FirstName = "Tom", LastName = "Solomon" },
                    new Member {ID = 2, FirstName = "Dick", LastName = "Solomon" },
                    new Member {ID = 3, FirstName = "Harry", LastName = "Solomon" },
                    new Member {ID = 4, FirstName = "Sally", LastName = "Solomon" },
                    new Member {ID = 5, FirstName = "Mary", LastName = "Albright" }
                });

            // setup things in contactRepo to test
            contactMock = new Mock<IContactRepository>();

            // setup things in dbContext to test
            var data = new List<Member>
            {
                new Member {ID = 1, FirstName = "Wolverine", Membership_Number = "01-123", Identity_ID = "ABC123" },
                new Member {ID = 1, FirstName = "Storm", Membership_Number = "02-345", Identity_ID = "DEF456" },
                new Member {ID = 1, FirstName = "Rogue", Membership_Number = "03-456", Identity_ID = "GHI789" }
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

            Assert.IsTrue(members.Length == 3);
            Assert.AreEqual(members[0].FirstName, "Tom");
            Assert.AreEqual(members[1].FirstName, "Dick");
            Assert.AreEqual(members[2].FirstName, "Harry");
        }

        [Test]
        public void TestCorrectIPG_Number()
        {
            EFMemberRepository repo = new EFMemberRepository(dbMock.Object);

            Member mem = repo.FindByIPG_ID("02-345");

            Assert.AreEqual(mem.FirstName, "Storm");
        }

        [Test]
        public void TestIncorrectIPG_Number()
        {
            EFMemberRepository repo = new EFMemberRepository(dbMock.Object);

            Member mem = repo.FindByIPG_ID("04-321");

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
    }
}
