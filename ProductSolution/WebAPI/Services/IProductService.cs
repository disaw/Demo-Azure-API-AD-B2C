using Domain.Enums;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> Read();

        Task<Product> Read(string id);

        Task Create(Product product);

        Task Update(Product product);

        Task Delete(string id);

        bool ProductExists(string id);

        Task<IEnumerable<Product>> FilterProducts(Filter filter, string value);
    }
}
