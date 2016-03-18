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
            var user = db.Users.FirstOrDefault(userQuery => userQuery.UserName.Equals(User.Identity.Name));
            var feed = new Feed(user);
            return View(feed);
        }
    }
}