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
    public class Update
    {
        private readonly IProductService _productService;

        public Update(IProductService productService)
        {
            _productService = productService;
        }

        [FunctionName("Update")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put",
            Route = "products/{id}")] 
            HttpRequest req, string id, ILogger log)
        {
            log.LogInformation($"Update Product: {id} triggered.");

            var bodyString = await req.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(bodyString);

            if (id != product.Id)
            {
                return new BadRequestObjectResult($"Product: {id} was not found.");
            }

            if (!_productService.ProductExists(product.Id))
            {
                return new ConflictObjectResult($"Invalid product Id: {id}.");
            }

            await _productService.Update(product);

            return new OkObjectResult($"Product: {id} was updated successfully.");
        }
    }
}
