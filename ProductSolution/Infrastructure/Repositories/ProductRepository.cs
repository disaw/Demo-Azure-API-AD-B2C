using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistency;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

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

        public async Task<IEnumerable<Product>> FilterProducts(Filter filter, string value)
        {
            var filterValue = value.Trim().ToLower();

             switch (filter)
            {
                case Filter.Description :
                    return await _productDBContext.Products.Where(p => p.Description.ToLower().Contains(filterValue)).ToListAsync();
                case Filter.Model :
                    return await _productDBContext.Products.Where(p => p.Model.ToLower().Contains(filterValue)).ToListAsync();
                case Filter.Brand :
                    return await _productDBContext.Products.Where(p => p.Brand.ToLower().Contains(filterValue)).ToListAsync();
                default: return null;
            }
        }
    }
}
