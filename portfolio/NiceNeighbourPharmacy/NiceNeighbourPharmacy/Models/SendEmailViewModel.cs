using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
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
        
        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the contents")]
        public string Contents { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}