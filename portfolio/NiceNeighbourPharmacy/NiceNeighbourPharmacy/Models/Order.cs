namespace NiceNeighbourPharmacy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrdersMedicines = new HashSet<OrdersMedicine>();
        }

        public int Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TotalPrice { get; set; }

        [Required]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime CollectDateTime { get; set; }

        public int CustomerId { get; set; }

        public int? PharmacistId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Pharmacist Pharmacist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdersMedicine> OrdersMedicines { get; set; }
    }
}
