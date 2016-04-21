using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InformationsSystemOru.Models
{
    public class EmailModel
    {
        
        public string FromName { get; set; }
        public string FromEmail { get; set; }
     
        public string ToEmail { get; set; }
        [Required]
        public string Message { get; set; }
        public string ReciverId { get; set; }
    }
}