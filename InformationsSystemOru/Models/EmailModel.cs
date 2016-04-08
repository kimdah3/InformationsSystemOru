﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InformationsSystemOru.Models
{
    public class EmailModel
    {
        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }
        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required, Display(Name = "Email to"), EmailAddress]
        public string ToEmail { get; set; }
        [Required]
        public string Message { get; set; }
    }
}