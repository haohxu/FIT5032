namespace NiceNeighbourPharmacy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rating
    {
        public int Id { get; set; }

        public decimal? RatingScore { get; set; }

        public string RatingComment { get; set; }

        public int RatingStatus { get; set; }

        public int OrderMedicineId { get; set; }

        public virtual OrdersMedicine OrdersMedicine { get; set; }
    }
}
