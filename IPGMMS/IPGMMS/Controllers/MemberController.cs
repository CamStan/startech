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

        /// <summary>
        /// GET: Members
        /// Displays all members sorted by last name, 3 at a time on a page with paging.
        /// </summary>
        /// <param name="page">The page number</param>
        /// <returns></returns>
        public ActionResult Index(int? page)
        {
            var members = memberRepo.GetAllMembers;

            int pageSize = 3;
            double pages = Math.Ceiling((double)members.Count() / pageSize);

            int pageNum = page ?? 1;

            ViewBag.Pages = pages;

            var membersPaged = members.Skip(pageSize * (pageNum - 1)).Take(pageSize);

            return View(membersPaged);
        }

        // GET: Member
        public ActionResult Details(int? ID)
        {
            if (!ID.HasValue)
            {
                ID = 5;
            }
            Member memb = memberRepo.Find(ID);
            return View(memb);
        }
    }
}