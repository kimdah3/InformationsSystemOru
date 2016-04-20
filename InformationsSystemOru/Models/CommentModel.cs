using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationsSystemOru.Models
{
    public class CommentModel
    {
        public string Authorname { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public User User { get; set; }
    }
}