using Microsoft.EntityFrameworkCore;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Car> Cars { get; set; }




        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            
        }
    }
}
