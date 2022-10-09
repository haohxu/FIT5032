namespace NiceNeighbourPharmacy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TrolleyItem
    {
        public int Id { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public int MedicineId { get; set; }

        [Required]
        [StringLength(128)]
        public string CustomerId { get; set; }

        public virtual Medicine Medicine { get; set; }
    }
}
