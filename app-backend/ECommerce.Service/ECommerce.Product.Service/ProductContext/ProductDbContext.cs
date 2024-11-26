using ECommerce.Product.Service.Model.User;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Product.Service.ProductContext
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserEntityTypeConfig().Configure(modelBuilder.Entity<UserModel>());
        }

        // DbSets
        public DbSet<UserModel> ECommerceUsers {  get; set; }
    }
}
