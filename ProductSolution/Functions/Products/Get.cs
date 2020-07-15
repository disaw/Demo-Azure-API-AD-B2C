using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Functions.Services;
using Domain.Models;
using System.Collections.Generic;

namespace Functions.Products
{
    public class Get
    {
        private readonly IProductService _productService;

        public Get(IProductService productService)
        {
            _productService = productService;
        }

        [FunctionName("Get")]
        public async Task<ActionResult<IEnumerable<Product>>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", 
            Route = "products")] 
            HttpRequest req, ILogger log)
        {
            log.LogInformation($"products Get triggered.");

            var products = await _productService.Read();

            return new OkObjectResult(products);
        }
    }
}
