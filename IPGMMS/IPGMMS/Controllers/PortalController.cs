using IPGMMS.Abstract;
using IPGMMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Diagnostics;
using IPGMMS.ViewModels;

namespace IPGMMS.Controllers
{
    [AdminAuthorize(Roles = "Admin")]
    public class PortalController : MController
    {
        private IMemberRepository memberRepo;
        private IPortalRepository portalRepo;

        public PortalController(IPortalRepository pRepo, IMemberRepository mRepo)
        {
            portalRepo = pRepo;
            memberRepo = mRepo;
        }


        // GET: Portal
        public ActionResult Index()
        {
                var user = User.Identity;
                ViewBag.Name = user.Name;

            return View();
        }


        public ActionResult ListMembers(int? page, string sortOrder, string searchString)
        {
            
            var members = memberRepo.GetAllMembers;

            //This section is to check the search string and return a member list that
            //has only the information that was searched for 
            //Credit for filtering and some paging code goes to: 
            //docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
            if (!String.IsNullOrEmpty(searchString))
            {
                members = members.Where(s => s.LastName.ToLower().Contains(searchString.ToLower())
                                       || s.FirstName.ToLower().Contains(searchString.ToLower()));
            }

            // It will switch the stored variable so it will use the other
            // next time.
            ViewBag.lName = String.IsNullOrEmpty(sortOrder) ? "l_name" : "";
            ViewBag.mem_lvl = sortOrder == "mem_lvl" ? "lvl_desc" : "mem_lvl";
            ViewBag.userName = sortOrder == "user_name" ? "username_desc" : "user_name";
            ViewBag.mem_num = sortOrder == "mem_num" ? "num_desc" : "mem_num";
            ViewBag.date_start = sortOrder == "date_start" ? "start_desc" : "date_start";
            ViewBag.date_end = sortOrder == "date_end" ? "end_desc" : "date_end";
            ViewBag.f_name = sortOrder == "f_name" ? "fname_desc" : "f_name";
            ViewBag.m_name = sortOrder == "m_name" ? "mname_desc" : "m_name";
            ViewBag.b_name = sortOrder == "b_name" ? "bname_desc" : "b_name";
            ViewBag.website = sortOrder == "website" ? "website_desc" : "website";

            // Initialize the variable that will become the sort option
            Func<Member, object> sorting;

            // Check each of the four dictionaries for sort params
            // Dictionary is at the bottom of file
            if (!String.IsNullOrEmpty(sortOrder))
            {
                if (sortBy.TryGetValue(sortOrder, out sorting))
                {
                    members = members.OrderBy(sorting);
                }
                else if (sortByDesc.TryGetValue(sortOrder, out sorting))
                {
                    members = members.OrderByDescending(sorting);
                }
                else // Shouldn't need this but will catch any non-match
                {
                    members = members.OrderByDescending(m => m.LastName);
                }
            }
            else // Catch if sortOrder is null or empty
            {
                members = members.OrderByDescending(m => m.LastName);
            }

            int pageSize = 5; //the number of items that can appear on each page.
            int startPage = (page ?? 1);

            return View("ListMembers", members.ToList().ToPagedList(startPage,pageSize));
        }

        public ActionResult DetailMember()
        {
            return View("DetailMember");
        }

        // GET: Addmember()
        public ActionResult AddMember()
        {
            MemberCreate createMember = new MemberCreate();
            createMember.Levels = memberRepo.GetLevels;
            return PartialView("_AddMember", createMember);
        }

        // POST: AddMember()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMember(MemberCreate infos)
        {
            if(ModelState.IsValid)
            {
                Debug.WriteLine("Says it's valid but not really");
                return View("Index");
            }
            infos.Levels = memberRepo.GetLevels;
            return PartialView("_AddMember", infos);
        }

        public ActionResult UpdateMember()
        {
            return View("UpdateMember");
        }

        public ActionResult ListTests()
        {
            return View("ListTests");
        }

        public ActionResult AddTest()
        {
            return View("AddTest");
        }

        public ActionResult ListCertifications()
        {
            return View("ListCertifications");
        }

        public ActionResult DetailCertification()
        {
            return View("DetailCertification");
        }

        public ActionResult AddCertification()
        {
            return View("AddCertification");
        }

        public ActionResult UpdateCertification()
        {
            return View("UpdateCertification");
        }

        // Two dictionaries, one for ascending, one for descending
        Dictionary<String, Func<Member, object>> sortBy = new Dictionary<String, Func<Member, object>>()
            {
                { "mem_lvl", m => m.MemberLevel1.MLevel },
                { "user_name", m => m.UserName },
                { "mem_num", m => m.Membership_Number },
                { "date_start", m => m.Membership_SignupDate },
                { "date_end", m => m.Membership_ExpirationDate },
                { "f_name", m => m.FirstName },
                { "m_name", m => m.MiddleName },
                { "l_name", m => m.LastName },
                { "b_name", m => m.BusinessName },
                { "website", m => m.Website },
            };

        Dictionary<String, Func<Member, object>> sortByDesc = new Dictionary<String, Func<Member, object>>()
            {
                { "lvl_desc", m => m.MemberLevel1.MLevel },
                { "username_desc", m => m.UserName },
                { "num_desc", m => m.Membership_Number },
                { "start_desc", m => m.Membership_SignupDate },
                { "end_desc", m => m.Membership_ExpirationDate },
                { "fname_desc", m => m.FirstName },
                { "mname_desc", m => m.MiddleName },
                { "lname_desc", m => m.LastName },
                { "bname_desc", m => m.BusinessName },
                { "website_desc", m => m.Website },
            };
    }
}