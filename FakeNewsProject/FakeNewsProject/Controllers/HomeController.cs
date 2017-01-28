using FakeNewsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

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
            Debug.WriteLine("We're at the post!");
            Debug.WriteLine(newPost.ID);
            Debug.WriteLine(newPost.PostDate);

            if (ModelState.IsValid)
            {
                Debug.WriteLine("We are saving!");
                db.Stories.Add(newPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Debug.WriteLine("We missed the save.");
            return View(newPost);
        }
    }
}