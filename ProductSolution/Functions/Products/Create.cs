using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Functions.Services;
using Domain.Models;
using Newtonsoft.Json;

namespace Functions.Products
{
    public class Create
    {
        private readonly IProductService _productService;

        public Create(IProductService productService)
        {
            _productService = productService;
        }

        [FunctionName("Create")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", 
            Route = "products")] 
            HttpRequest req, ILogger log)
        {
            var bodyString = await req.ReadAsStringAsync();
            var product =  JsonConvert.DeserializeObject<Product>(bodyString);

            log.LogInformation($"Create Product: {product.Id} triggered.");

            if (_productService.ProductExists(product.Id))
            {
                return new ConflictObjectResult($"Product with an Id: {product.Id} already exists.");
            }

            await _productService.Create(product);

            return new OkObjectResult($"Product: {product.Id} was created successfully.");
        }
    }
}
