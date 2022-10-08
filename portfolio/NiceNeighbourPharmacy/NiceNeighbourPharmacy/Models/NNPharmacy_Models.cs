using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace NiceNeighbourPharmacy.Models
{
    public partial class NNPharmacy_Models : DbContext
    {
        public NNPharmacy_Models()
            : base("name=NNPharmacy_Models")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrdersMedicine> OrdersMedicines { get; set; }
        public virtual DbSet<Pharmacist> Pharmacists { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<ShoppingCartRecord> ShoppingCartRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ShoppingCartRecords)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medicine>()
                .Property(e => e.Price)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Medicine>()
                .HasMany(e => e.OrdersMedicines)
                .WithRequired(e => e.Medicine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medicine>()
                .HasMany(e => e.ShoppingCartRecords)
                .WithRequired(e => e.Medicine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.TotalPrice)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrdersMedicines)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrdersMedicine>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.OrdersMedicine)
                .HasForeignKey(e => e.OrderMedicineId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rating>()
                .Property(e => e.RatingScore)
                .HasPrecision(3, 2);
        }
    }
}
