using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;

namespace InformationsSystemOru.Models
{ //validering
    public class BlogModel
    {
        public List<Post> AllPostsForUser { get; set; }

        [Display(Name = "Text")]
        [RegularExpression(@"[a-zA-ZäöåÄÖÅ]+", ErrorMessage = "Text must contain letters (A - Ö)")]
        [Required(ErrorMessage = "Write Something")]
        public string Text { get; set; }


        [Display(Name = "Title")]
        [RegularExpression(@"[a-zA-ZäöåÄÖÅ]+", ErrorMessage = "Title must contain letters (A - Ö)")]
        [Required(ErrorMessage = "Enter a Title")]
        public string Title { get; set; } //max 50


        public DateTime DatePosted { get; set; }

        [Display(Name = "Category")]
        [RegularExpression(@"[a-zA-ZäöåÄÖÅ]+", ErrorMessage = "Category must contain letters (A - Ö)")]
        [Required(ErrorMessage = "Enter a Category")]
        public string Category { get; set; } //rätt? max 10 char

        public int UserId { get; set; }

        public int Type { get; set; }
    }
}
