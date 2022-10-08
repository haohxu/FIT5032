using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace FIT5032_SimpleAngularApp.Models
{
    public class UnitContext : DbContext
    {
        public UnitContext(DbContextOptions<UnitContext> options) : base(options)
        {
            
        }

        public DbSet<Unit> Units { get; set; } = null!;
    }
}
