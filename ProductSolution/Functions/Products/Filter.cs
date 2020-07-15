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
    public class Filter
    {
        private readonly IProductService _productService;

        public Filter(IProductService productService)
        {
            _productService = productService;
        }

        [FunctionName("Filter")]
        public async Task<ActionResult<Product>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", 
            Route = "products/{filter}/{value}")] 
            HttpRequest req, string filter, string value, ILogger log)
        {
            log.LogInformation($"Filter Products by: {filter}, value: {filter} triggered.");

            var filterType = GetFilter(filter);

            if (filterType == Domain.Enums.Filter.Invalid)
            {
                return new NotFoundObjectResult($"Invalid Filter.");
            }

            var product = await _productService.FilterProducts(filterType, value);

            if (product == null)
            {
                return new NotFoundObjectResult($"No products found.");
            }

            return new OkObjectResult(product);
        }

        private Domain.Enums.Filter GetFilter(string filter)
        {
            switch (filter.Trim().ToLower())
            {
                case "description": return Domain.Enums.Filter.Description;
                case "model": return Domain.Enums.Filter.Model;
                case "brand": return Domain.Enums.Filter.Brand;
                default: return Domain.Enums.Filter.Invalid;
            }
        }
    }
}
