namespace NiceNeighbourPharmacy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ShoppingCartRecord
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int MedicineId { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Medicine Medicine { get; set; }
    }
}
