using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Enums;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productService.Read();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _productService.Read(id);

            if (product == null)
            {
                return NotFound($"Product: {id} was not found.");
            }

            return Ok(product);
        }        

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            if (_productService.ProductExists(product.Id))
            {
                return Conflict($"Product with an Id: {product.Id} already exists.");
            }

            await _productService.Create(product);

            return Ok($"Product: {product.Id} was created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest($"Product: {id} was not found.");
            }

            if (!_productService.ProductExists(product.Id))
            {
                return Conflict($"Invalid product Id: {id}.");
            }

            await _productService.Update(product);

            return Ok($"Product: {id} was updated successfully.");
        }        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!_productService.ProductExists(id))
            {
                return NotFound($"Product: {id} was not found.");
            }

            await _productService.Delete(id);

            return Ok($"Product: {id} was deleted successfully.");
        }

        [HttpGet("{filter}/{value}")]
        public async Task<ActionResult<Product>> Get(string filter, string value)
        {
            var filterType = GetFilter(filter);

            if (filterType == Filter.Invalid)
            {
                return NotFound($"Invalid Filter.");
            }

            var product = await _productService.FilterProducts(filterType, value);

            if (product == null)
            {
                return NotFound($"No products found.");
            }

            return Ok(product);
        }

        private Filter GetFilter(string filter)
        {
            switch (filter.Trim().ToLower())
            {
                case "description": return Filter.Description;
                case "model": return Filter.Model;
                case "brand": return Filter.Brand;
                default: return Filter.Invalid;
            }
        }
    }
}
