namespace NiceNeighbourPharmacy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class File
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Category { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
    }
}
