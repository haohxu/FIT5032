namespace NiceNeighbourPharmacy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Medicine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medicine()
        {
            OrderDetails = new HashSet<OrderDetail>();
            TrolleyItems = new HashSet<TrolleyItem>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        
        public string Category { get; set; }
        [Required]
        [Range(0.001, double.MaxValue, ErrorMessage = "Price must be positive decimal")]
        public decimal? Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "stock must be non-negative integer")]
        public int? NumberOfStock { get; set; }

        public decimal? AvgRatings { get; set; }

        public string Notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrolleyItem> TrolleyItems { get; set; }
    }
}
