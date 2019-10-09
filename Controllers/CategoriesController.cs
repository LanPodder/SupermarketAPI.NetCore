using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services;
using Supermarket.API.Resources;
using Supermarket.API.Extensions;

namespace Supermarket.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryResource>> GetAllAsync(){
            Console.WriteLine("Executing GET Request");
            var categories = await categoryService.ListAsync();
            var resources = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource){
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrorMessages());
            }
            var category = mapper.Map<SaveCategoryResource, Category>(resource);
            var result = await categoryService.SaveAsync(category);

            if(!result.Success){
                return BadRequest(result.Message);
            }
            var categoryResource = mapper.Map<Category, CategoryResource>(result.Category);

            return Ok(categoryResource);
        }
    }
}