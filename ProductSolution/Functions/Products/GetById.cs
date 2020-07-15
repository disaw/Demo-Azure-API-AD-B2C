using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Functions.Services;
using Domain.Models;

namespace Functions.Products
{
    public class GetById
    {
        private readonly IProductService _productService;

        public GetById(IProductService productService)
        {
            _productService = productService;
        }

        [FunctionName("GetById")]
        public async Task<ActionResult<Product>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", 
            Route = "products/{id}")] 
            HttpRequest req, string id, ILogger log)
        {
            log.LogInformation($"products Get by Id: {id} triggered.");

            var products = await _productService.Read(id);

            if (products == null)
            {
                return new NotFoundObjectResult($"Product: {id} was not found.");
            }

            return new OkObjectResult(products);
        }
    }
}
