using Microsoft.EntityFrameworkCore;
using Domain.Models;
using System.Linq;

namespace Infrastructure.Persistency
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {
           AddSampleData(); //For demonstration purpose 
        }

        public DbSet<Product> Products { get; set; }

        private void AddSampleData()
        {
            if (!Products.Any())
            {
                var productOne = new Product
                {
                    Id = "1",
                    Description = "Dell G5 15",
                    Model = "Laptop",
                    Brand = "Dell"
                };

                var productTwo = new Product()
                {
                    Id = "2",
                    Description = "Macbook Pro 15",
                    Model = "Laptop",
                    Brand = "Apple"
                };

                Products.Add(productOne);
                Products.Add(productTwo);
                SaveChangesAsync();
            }
        }
    }
}
