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


        public ActionResult ListMembers()
        {
            var members = memberRepo.GetAllMembers;
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