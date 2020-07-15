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
    public class Delete
    {
        private readonly IProductService _productService;

        public Delete(IProductService productService)
        {
            _productService = productService;
        }

        [FunctionName("Delete")]
        public async Task<ActionResult<Product>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", 
            Route = "products/{id}")] 
            HttpRequest req, string id, ILogger log)
        {
            log.LogInformation($"Delete Product: {id} triggered.");

            if (!_productService.ProductExists(id))
            {
                return new NotFoundObjectResult($"Product: {id} was not found.");
            }

            await _productService.Delete(id);

            return new OkObjectResult($"Product: {id} was deleted successfully.");
        }
    }
}
