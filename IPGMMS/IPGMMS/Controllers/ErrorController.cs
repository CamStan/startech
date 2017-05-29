﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPGMMS.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult ServerError()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}