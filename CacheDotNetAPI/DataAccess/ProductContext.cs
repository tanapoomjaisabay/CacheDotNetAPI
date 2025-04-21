using CacheDotNetAPI.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CacheDotNetAPI.DataAccess
{
    public class ProductContext : DbContext, IProductDataSet
    {
        public DbSet<ProductEntity> productEntity => Set<ProductEntity>();

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var customeAuthenBuilder = builder.Entity<ProductEntity>();
            customeAuthenBuilder.ToTable("product_master", "dbo");
            customeAuthenBuilder.HasKey(x => new { x.idKey });
            customeAuthenBuilder.Property(x => x.idKey).ValueGeneratedOnAdd();

        }
    }
}
