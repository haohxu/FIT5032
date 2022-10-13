using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Xml.Linq;

namespace NiceNeighbourPharmacy.Models
{
    public class SendEmailViewModel : DbContext
    {
        // Your context has been configured to use a 'SendEmailViewModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'NiceNeighbourPharmacy.Models.SendEmailViewModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SendEmailViewModel' 
        // connection string in the application configuration file.
        public SendEmailViewModel()
            : base("name=SendEmailViewModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
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