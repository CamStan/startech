using IPGMMS.Abstract;
using IPGMMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        

        //Check if admin
        //public Boolean isAdminUser()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var user = User.Identity;
        //        var s = UserManager.GetRoles(user.GetUserId());
        //        if (s[0].ToString() == "Admin")
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return false;
        //}

        // GET: Portal
        public ActionResult Index()
        {
            //if (User.Identity.IsAuthenticated)
            //{
                var user = User.Identity;
                ViewBag.Name = user.Name;
                //ViewBag.displayAdmin = "No";

            //    if (isAdminUser())
            //    {
            //        ViewBag.displayAdmin = "Yes";
            //    }
            //    return View();
            //}
            //else
            //{
            //    ViewBag.Name = "Not Logged In";
            //}
            return View();
        }


        public ActionResult ListMembers(string sortOrder)
        {
            
            var members = memberRepo.GetAllMembers;

            // Viewbag variables to keep track of which way to sort. 
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

            // Really large switch, probably longer than it should be for performance reasons.
            switch (sortOrder)
            {
                case "mem_lvl":
                    members = members.OrderBy(m => m.MemberLevel1.MLevel);
                    break;
                case "lvl_desc":
                    members = members.OrderByDescending(m => m.MemberLevel1.MLevel);
                    break;
                case "user_name":
                    members = members.OrderBy(m => m.UserName);
                    break;
                case "username_desc":
                    members = members.OrderByDescending(m => m.UserName);
                    break;
                case "mem_num":
                    members = members.OrderBy(m => m.Membership_Number);
                    break;
                case "num_desc":
                    members = members.OrderByDescending(m => m.Membership_Number);
                    break;
                case "date_start":
                    members = members.OrderBy(m => m.Membership_SignupDate);
                    break;
                case "start_desc":
                    members = members.OrderByDescending(m => m.Membership_SignupDate);
                    break;
                case "date_end":
                    members = members.OrderBy(m => m.Membership_ExpirationDate);
                    break;
                case "end_desc":
                    members = members.OrderByDescending(m => m.Membership_ExpirationDate);
                    break;
                case "f_name":
                    members = members.OrderBy(m => m.FirstName);
                    break;
                case "fname_desc":
                    members = members.OrderByDescending(m => m.FirstName);
                    break;
                case "m_name":
                    members = members.OrderBy(m => m.MiddleName);
                    break;
                case "mname_desc":
                    members = members.OrderByDescending(m => m.MiddleName);
                    break;
                case "l_name":
                    members = members.OrderBy(m => m.LastName);
                    break;
                case "b_name":
                    members = members.OrderBy(m => m.BusinessName);
                    break;
                case "bname_desc":
                    members = members.OrderByDescending(m => m.BusinessName);
                    break;
                case "website":
                    members = members.OrderBy(m => m.Website);
                    break;
                case "website_desc":
                    members = members.OrderByDescending(m => m.Website);
                    break;
                default:
                    members = members.OrderByDescending(m => m.LastName);
                    break;
            }
            return PartialView("_ListMembers",members);
        }

        public ActionResult DetailMember()
        {
            return PartialView("_DetailMember");
        }

        public ActionResult AddMember()
        {
            return PartialView("_AddMember");
        }

        public ActionResult UpdateMember()
        {
            return PartialView("_UpdateMember");
        }

        public ActionResult ListTests()
        {
            return PartialView("_ListTests");
        }

        public ActionResult AddTest()
        {
            return PartialView("_AddTest");
        }

        public ActionResult ListCertifications()
        {
            return PartialView("_ListCertifications");
        }

        public ActionResult DetailCertification()
        {
            return PartialView("_DetailCertification");
        }

        public ActionResult AddCertification()
        {
            return PartialView("_AddCertification");
        }

        public ActionResult UpdateCertification()
        {
            return PartialView("_UpdateCertification");
        }
    }
}