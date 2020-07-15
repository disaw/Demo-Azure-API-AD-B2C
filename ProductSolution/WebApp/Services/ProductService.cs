using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IApiService _api;
        private readonly IConfiguration _config;
        private readonly string _url;

        public ProductService(IApiService api, IConfiguration config)
        {
            _api = api;
            _config = config;
            _url = _config.GetValue<string>("ApiUrl");
        }

        public async Task<IEnumerable<Product>> Read()
        {
            return await _api.Get<IEnumerable<Product>>(_url);
        }

        public async Task<Product> Read(string id)
        {
            return await _api.Get<Product>(_url, id);
        }

        public async Task Create(Product product)
        {
            await _api.Post<Product>(product, _url);
        }

        public async Task Update(Product product)
        {
            await _api.Put<Product>(product, _url, product.Id);
        }

        public async Task Delete(string id)
        {
            await _api.Delete(_url, id);
        }

        public async Task<bool> ProductExists(string id)
        {
            return await _api.Get<bool>(_url, id);
        }

        public async Task<IEnumerable<Product>> FilterProducts(string filter, string value)
        {
            return await _api.Get<IEnumerable<Product>>(_url, value);
        }
    }
}
