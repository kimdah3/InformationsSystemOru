using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationsSystemOru.Models
{
    public class BlogModel
    {
        public string Text { get; set; }
        public string Title { get; set; } //max 50
        public DateTime DatePosted { get; set; }
        public string Category { get; set; } //rätt? max 10 char
        public int UserId { get; set; }
        public int Type { get; set; }
    }
}
