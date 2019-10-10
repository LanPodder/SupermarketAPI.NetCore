using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;

namespace Supermarket.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository){
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> ListAsync(){
            return await productRepository.ListAsync();
        }
    }
}