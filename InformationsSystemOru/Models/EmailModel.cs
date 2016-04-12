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
        [Required, Display(Name = "Email to"), EmailAddress]
        public string ToEmail { get; set; }
        [Required]
        public string Message { get; set; }
    }
}