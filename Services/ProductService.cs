using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services.Communication;

namespace Supermarket.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork){
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> ListAsync(){
            return await productRepository.ListAsync();
        }

        public async Task<ProductResponse> SaveAsync(Product product){
            var existingCategory = await productRepository.FindCategoryByIdAsync(product.CategoryId);
            if(existingCategory == null){
                return new ProductResponse("Category not found");
            }
            try{
                await productRepository.AddAsync(product);
                await unitOfWork.CompleteAsync();
                return new ProductResponse(product);
            }catch(Exception e){
                return new ProductResponse($"An error occured when saving the product: {e.Message}");
            }
        }
    }
}