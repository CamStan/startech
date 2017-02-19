using IPGMMS.Abstract;
using IPGMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPGMMS.Controllers
{
    public class MemberController : Controller
    {
        private IMemberRepository memberRepo;

        public MemberController(IMemberRepository repo)
        {
            memberRepo = repo;
        }

        // GET: Member
        public ActionResult Index()
        {
            IEnumerable<Member> members = memberRepo.GetAllMembers();
            return View(members);
        }
    }
}