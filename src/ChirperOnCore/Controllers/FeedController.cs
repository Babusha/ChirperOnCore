using System;
using System.Collections.Generic;
using System.Linq;
using ChirperOnCore.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity.Extensions;

namespace ChirperOnCore.Controllers
{
    [Authorize]
    public class FeedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Feed
        public ActionResult Index()
        {
            var feed = new Feed(db.Users.Find(User.Identity.Name));
            return View(feed);
        }
    }
}