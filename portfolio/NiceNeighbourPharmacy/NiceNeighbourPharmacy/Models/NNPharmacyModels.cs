using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace NiceNeighbourPharmacy.Models
{
    public partial class NNPharmacyModels : DbContext
    {
        public NNPharmacyModels()
            : base("name=NNPharmacyModels")
        {
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<TrolleyItem> TrolleyItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .Property(e => e.Latitude)
                .HasPrecision(11, 8);

            modelBuilder.Entity<Location>()
                .Property(e => e.Longitude)
                .HasPrecision(11, 8);

            modelBuilder.Entity<Medicine>()
                .Property(e => e.Price)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Medicine>()
                .Property(e => e.AvgRatings)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Medicine>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Medicine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medicine>()
                .HasMany(e => e.TrolleyItems)
                .WithRequired(e => e.Medicine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.OrderDetail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.TotalPrice)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rating>()
                .Property(e => e.RatingScore)
                .HasPrecision(7, 2);

            modelBuilder.Entity<TrolleyItem>()
                .Property(e => e.Price)
                .HasPrecision(7, 2);
        }
    }
}
