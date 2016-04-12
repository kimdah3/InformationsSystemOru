using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using System.Web;

namespace InformationsSystemOru.Models
{ //validering
    public class BlogModel
    {
        public List<Post> AllPostsForUser { get; set; }

        [Display(Name = "Text")]
        [Required(ErrorMessage = "Write Something")]
        public string Text { get; set; }


        [Display(Name = "Title")]
        [Required(ErrorMessage = "Enter a Title")]
        public string Title { get; set; } //max 50


        public DateTime DatePosted { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Enter a Category")]
        public string Category { get; set; } //rätt? max 10 char

        public int UserId { get; set; }

        public int Type { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }
    }
}
