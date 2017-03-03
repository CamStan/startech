using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPGMMS.Controllers
{
    public class PortalController : Controller
    {
        // GET: Portal
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListMembers()
        {
            return View();
        }

        public ActionResult AddMember()
        {
            return View();
        }

        public ActionResult ListTests()
        {
            return View();
        }

        public ActionResult AddTest()
        {
            return View();
        }

        public ActionResult ListCertifications()
        {
            return View();
        }

        public ActionResult AddCertification()
        {
            return View();
        }
    }
}