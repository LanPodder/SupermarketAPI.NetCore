using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services.Communication;

namespace Supermarket.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Category>> ListAsync(){
            return await categoryRepository.ListAsync();
        }

        public async Task<CategoryResponse> SaveAsync(Category category){
            try{
                await categoryRepository.AddAsync(category);
                await unitOfWork.CompleteAsync();
                return new CategoryResponse(category);
            }catch(Exception e){
                return new CategoryResponse($"An error occured when saving the category: {e.Message}");
            }
        }

        public async Task<CategoryResponse> UpdateAsync(int id, Category category){
            var existingCategory = await categoryRepository.FindByIdAsync(id);
            if(existingCategory == null){
                return new CategoryResponse("Category not found");
            }

            existingCategory.Name = category.Name;

            try
            {
                categoryRepository.Update(existingCategory);
                await unitOfWork.CompleteAsync();

                return new CategoryResponse(existingCategory);
            }
            catch (Exception e)
            {
                return new CategoryResponse($"An error occured while updating the category: {e.Message}");
            }
        }

        public async Task<CategoryResponse> DeleteAsync(int id){
            var existingCategory = await categoryRepository.FindByIdAsync(id);

            if(existingCategory == null){
                return new CategoryResponse("Category not found");
            }

            try
            {
                categoryRepository.Remove(existingCategory);
                await unitOfWork.CompleteAsync();
                return new CategoryResponse(existingCategory);
            }
            catch (Exception e)
            {
                return new CategoryResponse($"An error occured while deleting the category: {e.Message}");
            }
        }
    }
}