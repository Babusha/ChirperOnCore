using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChirperOnCore.Models;

namespace ChirperOnCore.Models
{
    public class Feed
    {
        public ApplicationUser User;

        public List<Post> Posts; 
        public Feed(ApplicationUser user)
        {
            User = user;
            Posts = new List<Post>();
            try
            {
                foreach (var userSub in user.Subscriptions)
                {
                    foreach (var post in userSub.Posts)
                    {
                        Posts.Add(post);
                    }
                }
                foreach (var post in user.Posts)
                {
                    Posts.Add(post);
                }
                //user.Subscriptions.ToList().ForEach(userSub => userSub.Posts.ToList().ForEach(post => Posts.Add(post)));
                //user.Posts.ToList().ForEach(post => Posts.Add(post));
                Posts = Posts.OrderByDescending(post => post.CreateDateTime).ToList();
            }
            catch (NullReferenceException)
            {
            }
        }
    }
}
