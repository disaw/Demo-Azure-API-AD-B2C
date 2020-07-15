using Domain.Enums;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> Read();

        Task<Product> Read(string id);

        Task Create(Product product);

        Task Update(Product product);

        Task Delete(string id);

        Task<bool> ProductExists(string id);

        Task<IEnumerable<Product>> FilterProducts(string filter, string value);
    }
}
