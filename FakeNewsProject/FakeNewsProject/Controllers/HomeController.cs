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
                .FirstOrDefault()).ToList();
            return View(pared);
        }

        /// <summary>
        /// HttpGet method
        /// Shows story creation page. Will need to set a way to get user's ID
        /// from log in. Story model requires the user's ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            Story postSetup = new Story();
            postSetup.UserID = 1;
            postSetup.PostDate = DateTime.Now;
            return View(postSetup);
        }

        [HttpPost]
        public ActionResult Create(Story newPost)
        {
            if (ModelState.IsValid)
            {
                db.Stories.Add(newPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newPost);
        }
    }
}