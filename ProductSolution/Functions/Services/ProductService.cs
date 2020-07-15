using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Functions.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Read()
        {
            return await _productRepository.Read();
        }

        public async Task<Product> Read(string id)
        {
            return await _productRepository.Read(id);
        }

        public async Task Create(Product product)
        {
            await _productRepository.Create(product);
        }

        public async Task Update(Product product)
        {
            await _productRepository.Update(product);
        }

        public async Task Delete(string id)
        {
            await _productRepository.Delete(id);
        }

        public bool ProductExists(string id)
        {
            return _productRepository.ProductExists(id);
        }

        public async Task<IEnumerable<Product>> FilterProducts(Filter filter, string value)
        {
            return await _productRepository.FilterProducts(filter, value);
        }
    }
}
