using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistency;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.Common;
using System;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDBContext _productDBContext;

        public ProductRepository(ProductDBContext productDBContext)
        {
            _productDBContext = productDBContext;
        }

        public async Task<IEnumerable<Product>> Read()
        {
            return await _productDBContext.Products.ToListAsync();
        }

        public async Task<Product> Read(string id)
        {
            return await _productDBContext.Products.FindAsync(id);
        }

        public async Task Create(Product product)
        {
            _productDBContext.Products.Add(product);

            await _productDBContext.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            _productDBContext.Entry(product).State = EntityState.Modified;

            await _productDBContext.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var product = await _productDBContext.Products.FindAsync(id);

            _productDBContext.Products.Remove(product);

            await _productDBContext.SaveChangesAsync();
        }

        public bool ProductExists(string id)
        {
            return _productDBContext.Products.Any(e => e.Id == id);
        }
    }
}
