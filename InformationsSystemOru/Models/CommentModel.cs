using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationsSystemOru.Models
{
    public class CommentModel
    {
        public string Authorname { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}