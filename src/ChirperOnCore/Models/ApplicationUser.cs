using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ChirperOnCore.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChirperOnCore.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public string SubscribersUserId { get; set; }

        [ForeignKey("SubscribersUserId")]
        public virtual ICollection<ApplicationUser> Subscriptions { get; set; }
    }
}
