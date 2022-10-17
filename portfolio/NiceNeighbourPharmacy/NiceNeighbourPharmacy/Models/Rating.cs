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

        [Range(0.00, 10.00, ErrorMessage = "Rating Score can be empty, otherwise 0 to 10")]
        public decimal? RatingScore { get; set; }

        public string RatingComment { get; set; }

        public string RatingStatus { get; set; }

        public int OrderDetailId { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
    }
}
