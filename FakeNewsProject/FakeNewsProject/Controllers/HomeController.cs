using FakeNewsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FakeNewsProject.Controllers
{
    public class HomeController : Controller
    {
        FakeNewsContext db = new FakeNewsContext();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.Stories.OrderByDescending(s => s.PostDate).ToList());
        }
    }
}