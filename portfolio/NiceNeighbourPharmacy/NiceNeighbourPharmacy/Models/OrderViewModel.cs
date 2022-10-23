using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        [DataType(DataType.DateTime)]
        [BookingNoOverlap]
        public DateTime CollectDateTime { get; set; }

        public IEnumerable<TrolleyItem> TrolleyItems { get; set; }
    }

    public class DetailedOrderViewModel
    {
        public string CustomerName { get; set; }
        public Order TheOrder { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
    // End -----

    public class BookingNoOverlapAttribute : ValidationAttribute
    {
        private NNPharmacyModels db = new NNPharmacyModels();

        public BookingNoOverlapAttribute() { }

        public string GetErrorMessage() => $"Collection Date and Time cannot overlap! Please choose another time.";

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var orderViewModel = (ConfirmOrderViewModel)validationContext.ObjectInstance;
            var inputDateTime = (DateTime)value;

            var allOrders2 = db.Orders.Where(t => t.CollectDateTime != null);
            var allOrders = allOrders2.Where(t => DbFunctions.TruncateTime(t.CollectDateTime) == inputDateTime.Date);
            System.Diagnostics.Debug.WriteLine("Hello");

            if (inputDateTime.Hour < 9 || inputDateTime.AddMinutes(14).Hour > 17)
                return new ValidationResult("Collect Time must between 9:00 and 17:00");

            if (inputDateTime.Minute != 0 && inputDateTime.Minute != 15 &&
                inputDateTime.Minute != 30 && inputDateTime.Minute != 45)
                return new ValidationResult("Please choose Minutes as 0, 15, 30, 45");
            
            foreach (var aOrder in allOrders)
            {
                System.Diagnostics.Debug.WriteLine(aOrder.CollectDateTime);
                DateTime tempDateTime = (DateTime)aOrder.CollectDateTime;

                if (tempDateTime.AddMinutes(-15) <= inputDateTime && inputDateTime <= tempDateTime.AddMinutes(14))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            System.Diagnostics.Debug.WriteLine("Hello2");


            return ValidationResult.Success;
        }
    }
}