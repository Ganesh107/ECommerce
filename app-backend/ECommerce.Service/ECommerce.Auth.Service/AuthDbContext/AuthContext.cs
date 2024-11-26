using ECommerce.Auth.Service.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Auth.Service.AuthDbContext
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthRequest>().ToTable("User", schema: "ecom",
                t => t.ExcludeFromMigrations()).HasNoKey();
            modelBuilder.Entity<AuthRequest>().Ignore(u => u.Password);
        }
        
        // DbSet
        public DbSet<AuthRequest> Users { get; set; }
    }
}
