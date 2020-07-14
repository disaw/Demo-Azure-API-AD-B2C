﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Interfaces;

namespace WebAPI.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productRepository.Read();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _productRepository.Read(id);

            if (product == null)
            {
                return NotFound($"Product: {id} was not found.");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            if (_productRepository.ProductExists(product.Id))
            {
                return Conflict($"Product with an Id: {product.Id} already exists.");
            }

            await _productRepository.Create(product);

            return Ok($"Product: {product.Id} was created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest($"Product: {id} was not found.");
            }

            if (!_productRepository.ProductExists(product.Id))
            {
                return Conflict($"Invalid product Id: {id}.");
            }

            await _productRepository.Update(product);

            return Ok($"Product: {id} was updated successfully.");
        }        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!_productRepository.ProductExists(id))
            {
                return NotFound($"Product: {id} was not found.");
            }

            await _productRepository.Delete(id);

            return Ok($"Product: {id} was deleted successfully.");
        }
    }
}
