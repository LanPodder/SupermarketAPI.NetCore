using Microsoft.AspNetCore.Mvc;

namespace Supermarket.Controllers
{
    [Route("/api/[controller]")]
    public class CategoriesController
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetAllAsync(){
            var categories = await categoryService.ListAsync();
            return categories;
        }
    }
}