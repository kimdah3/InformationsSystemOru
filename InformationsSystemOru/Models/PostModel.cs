using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformationsSystemOru.Models
{
    public class PostModel
    {
        public int PostId { get; set; }

        [Display(Name = "Text")]
        [Required(ErrorMessage = "Write Something")]
        [AllowHtml]
        public string Text { get; set; }

        public DateTime DatePosted { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Enter a Title")]
        public string Title { get; set; } //max 50

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Enter a Category")]
        public string Category { get; set; } //rätt? max 10 char

        public int Type { get; set; }
        public int PostingUserId { get; set; }
        public string PostingUsersName { get; set; }
        public string FileUrl { get; set; }
        public string Filename { get; set; }
        public List<Comment> Comments { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }
    }
}