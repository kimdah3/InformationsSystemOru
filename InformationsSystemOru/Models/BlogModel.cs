using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using System.Web;
using System.Web.Mvc;

namespace InformationsSystemOru.Models
{ 
    public class BlogModel
    {
        public List<PostModel> AllPosts { get; set; }
        public PostModel NewPost { get; set; }
        public List<PostType> PostTypes { get; set; }
        public int loggedInUserId { get; set; }
        public List<string> CategoryList { get; set; } 
    }
}
