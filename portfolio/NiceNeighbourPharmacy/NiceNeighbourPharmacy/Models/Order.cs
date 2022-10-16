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
            Events = new HashSet<Event>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CollectDateTime { get; set; }

        [Required]
        [StringLength(128)]
        public string CustomerId { get; set; }

        [StringLength(128)]
        public string PharmacistId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
