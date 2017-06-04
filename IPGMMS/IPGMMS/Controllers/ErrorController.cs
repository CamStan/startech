using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPGMMS.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Get: NotFound() 
        /// Used to indicate a 404 error or another error that is not dealt
        /// with specifically.
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        /// <summary>
        /// Get: ServerError()
        /// Used when a 500 error is triggered.
        /// </summary>
        /// <returns></returns>
        public ActionResult ServerError()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}