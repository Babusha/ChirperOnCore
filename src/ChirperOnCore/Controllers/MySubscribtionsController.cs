using System;
using System.Collections.Generic;
using System.Linq;
using ChirperOnCore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity.Extensions;

namespace ChirperOnCore.Controllers
{
    public class MySubscribtionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MySubscribtions
        public ActionResult Index()
        {
            return View(new MySubscribtionList(db.Users.Find(User.Identity.Name)));
        }
    }
}