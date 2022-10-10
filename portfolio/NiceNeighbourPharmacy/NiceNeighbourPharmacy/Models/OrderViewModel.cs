using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceNeighbourPharmacy.Models
{
    public class OrderViewModel
    {
    }

    // Start - Add customized view model
    public class ConfirmOrderViewModel 
    {
        [Required]
        [Display(Name = "Collect Date and Time")]
        public DateTime CollectDateTime { get; set; }

        public IEnumerable<TrolleyItem> TrolleyItems { get; set; }

        //[Required]
        //[Display(Name = "Code")]
        //public string Code { get; set; }
        //public string ReturnUrl { get; set; }

        //[Display(Name = "Remember this browser?")]
        //public bool RememberBrowser { get; set; }

        //public bool RememberMe { get; set; }
    }
    // End -----
}