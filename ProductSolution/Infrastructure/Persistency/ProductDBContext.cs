using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Persistency
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
