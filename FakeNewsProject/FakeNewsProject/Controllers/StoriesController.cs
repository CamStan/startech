using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FakeNewsProject.Models;
using FakeNewsProject.ViewModels;

namespace FakeNewsProject.Controllers
{
    public class StoriesController : Controller
    {
        private FakeNewsContext db = new FakeNewsContext();

        // GET: Stories
        public ActionResult Index()
        {
            var stories = db.Stories.Include(s => s.User);
            return View(stories.ToList());
        }

        // GET: Stories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
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
            Story postSetup = db.Stories.Create();
            postSetup.UserID = 1;
            postSetup.PostDate = DateTime.Now;

            var tags = db.Tags.OrderBy(t => t.Name).ToList();

            // create a list of viewmodels to associate a boolean with each tag
            var tagOptions = new List<TagSelect>();
            foreach (Tag t in tags)
            {
                tagOptions.Add(new TagSelect { TagName = t.Name, TagID = t.ID});
            }

            TagStories ts = new TagStories { TheStory = postSetup, TheTags = tagOptions };
            return View(ts);
        }

        [HttpPost]
        public ActionResult Create(TagStories newPost)
        {
            if (ModelState.IsValid)
            {
                db.Stories.Add(newPost.TheStory);
                // create a link between story and tag for all selected tags
                foreach(var tag in newPost.TheTags)
                {
                    if (tag.IsSelected)
                    {
                        // not how to do this, but don't care at this point
                        db.StoryTags.Add(new StoryTag { StoryID = db.Stories.Count() + 1, TagID = tag.TagID });
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newPost);
        }

        // GET: Stories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "FName", story.UserID);
            return View(story);
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Title,Body,Summary,PostDate")] Story story)
        {
            if (ModelState.IsValid)
            {
                db.Entry(story).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "FName", story.UserID);
            return View(story);
        }

        // GET: Stories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public ActionResult Save(int? id, int? storyId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (db.Users.Find(id) == null)
            {
                return RedirectToAction("Details");
            }
            Favorite fav = new Favorite();
            fav.UserID = (int)id;
            fav.StoryID = (int)storyId;
            db.Favorites.Add(fav);
            db.SaveChanges();
            return RedirectToAction("Details","Users", id);

        }
    }
}
