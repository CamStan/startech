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
            var pared = db.Stories
                .GroupBy(genre => genre.StoryTags.FirstOrDefault().Tag.Name)
                .Select(grp => grp.OrderByDescending(x => x.PostDate)
                .FirstOrDefault()).OrderByDescending(y => y.PostDate).ToList();
            return View(pared);
        }

    }
}