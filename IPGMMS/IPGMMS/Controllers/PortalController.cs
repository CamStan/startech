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
    [Authorize(Roles = "Admin")]
    public class PortalController : Controller
    {
        private IPortalRepository repo;

        public PortalController(IPortalRepository portalRepo)
        {
            repo = portalRepo;
        }

        // Check if admin
        //public Boolean isAdminUser()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var user = User.Identity;
        //        ApplicationDbContext context = new ApplicationDbContext();
        //        var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
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
            //if(User.Identity.IsAuthenticated)
            //{
            var user = User.Identity;
            ViewBag.Name = user.Name;
            //    ViewBag.displayAdmin = "No";

            //    if(isAdminUser())
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
    }
}