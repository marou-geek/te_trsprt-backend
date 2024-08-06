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
        public DbSet<Car> Cars { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<UserPlant> UserPlants { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<UserPlant>(entity =>
            {
                entity.HasKey(up => up.Id);

                entity.HasOne(up => up.User)
                      .WithMany(u => u.UserPlants)
                      .HasForeignKey(up => up.UserId);

                entity.HasOne(up => up.Plant)
                      .WithMany(p => p.UserPlants)
                      .HasForeignKey(up => up.PlantId);
            });
        }
    }
}
