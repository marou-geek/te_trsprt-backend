using Microsoft.EntityFrameworkCore;
using TE_trsprt_remake.Models;

    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        
    }
