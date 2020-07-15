using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IApiService _api;
        //private const string url = "https://localhost:44399/products/";
        private const string url = "http://localhost:7777/api/products/";

        public ProductService(IApiService api)
        {
            _api = api;
        }

        public async Task<IEnumerable<Product>> Read()
        {
            return await _api.Get<IEnumerable<Product>>(url);
        }

        public async Task<Product> Read(string id)
        {
            return await _api.Get<Product>(url, id);
        }

        public async Task Create(Product product)
        {
            await _api.Post<Product>(product, url);
        }

        public async Task Update(Product product)
        {
            await _api.Put<Product>(product, url, product.Id);
        }

        public async Task Delete(string id)
        {
            await _api.Delete(url, id);
        }

        public async Task<bool> ProductExists(string id)
        {
            return await _api.Get<bool>(url, id);
        }

        public async Task<IEnumerable<Product>> FilterProducts(Filter filter, string value)
        {
            return await _api.Get<IEnumerable<Product>>(url, value);
        }
    }
}
