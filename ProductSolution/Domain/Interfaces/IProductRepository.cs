using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> Read();

        Task<Product> Read(string id);

        Task Create(Product product);

        Task Update(Product product);

        Task Delete(string id);

        bool ProductExists(string id);
    }
}
