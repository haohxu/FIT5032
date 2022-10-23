using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace NiceNeighbourPharmacy.Models
{
    public class SendEmailViewModel
    {
    }

    public class SendGroupEmailViewModel 
    {
        //[Display(Name = "Email address")]
        //[Required(ErrorMessage = "Please enter an email address.")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        //public string ToEmail { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Please enter a subject.")]
        [StringLength(200)]
        public string Subject { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Please enter the contents")]
        [StringLength(800)]
        public string Contents { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}