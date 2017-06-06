using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPGMMS.Controllers
{
    /// <summary>
    /// Most pages in this controller are HTML information pages,
    /// Keep action methods in an appropriate controller.
    /// </summary>
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        
        /// <summary>
        /// Point to this method for any links that currently do not have a 
        /// page associated with them. 
        /// </summary>
        /// <returns></returns>
        public ActionResult UnderConstruction()
        {
            return View();
        }
        
        public ActionResult StarTechCredits()
        {
            return View();
        }
    }
}