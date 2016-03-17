using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using ChirperOnCore.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Extensions;

namespace ChirperOnCore.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index(string userId)
        {
            if (String.IsNullOrEmpty(userId))
                userId = User.Identity.Name;

           ViewBag.AreTheyMine = userId == User.Identity.Name;
           return View(db.Posts.Where(post => post.Author.Id.Equals(userId)).ToList());

        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Text")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreateDateTime = DateTime.Now;
                post.Author = db.Users.Find(User.Identity.Name);
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        public ActionResult Repost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.BadRequest);
            }
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (post.Author.Id.Equals(User.Identity.Name))
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var rePost = new Post()
                {
                    RepostFrom = post,
                    Author = db.Users.Find(User.Identity.Name),
                    Text = post.Text,
                    CreateDateTime = DateTime.Now
                };
                
                db.Posts.Add(rePost);
                db.SaveChanges();
                return RedirectToAction("Index","Feed");
            }
            return RedirectToAction("Index", "Feed");
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (!post.Author.Id.Equals(User.Identity.Name))
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.BadRequest);
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Text")] Post post)
        {
            var editPost = db.Posts.Find(post.Id);
            if (editPost == null)
            {
                return HttpNotFound();
            }
            if (!editPost.Author.Id.Equals(User.Identity.Name))
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                editPost.LastEditedDateTime = DateTime.Now;
                editPost.Text = post.Text;
                db.Entry(editPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Feed");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post deletePost = db.Posts.Find(id);

            db.Posts.Where(post => post.RepostFrom.Id.Equals(id)).ToList().ForEach(rePost => db.Posts.Remove(rePost));
            db.Posts.Remove(deletePost);
            db.SaveChanges();
            return RedirectToAction("Index", "Feed");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
