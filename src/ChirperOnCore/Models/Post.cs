using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ChirperOnCore.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChirperOnCore.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? LastEditedDateTime { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public Post RepostFrom { get; set; }
    }
}