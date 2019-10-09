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

        public async Task<SaveCategoryResponse> SaveAsync(Category category){
            try{
                await categoryRepository.AddAsync(category);
                await unitOfWork.CompleteAsync();
                return new SaveCategoryResponse(category);
            }catch(Exception e){
                return new SaveCategoryResponse($"An error occured when saving the category: {e.Message}");
            }
        }

        public async Task<SaveCategoryResponse> UpdateAsync(int id, Category category){
            var existingCategory = await categoryRepository.FindByIdAsync(id);
            if(existingCategory == null){
                return new SaveCategoryResponse("Category not found");
            }

            existingCategory.Name = category.Name;

            try
            {
                categoryRepository.Update(existingCategory);
                await unitOfWork.CompleteAsync();

                return new SaveCategoryResponse(existingCategory);
            }
            catch (Exception e)
            {
                return new SaveCategoryResponse($"An error occured while updating the category: {e.Message}");
            }
        }
    }
}