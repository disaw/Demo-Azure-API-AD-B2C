using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistency;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Functions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Functions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Functions
{
    public class Startup : FunctionsStartup
    {
        public IConfiguration _configuration { get; set; }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<ProductDBContext>(options => options.UseInMemoryDatabase("ProductDB"));
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
