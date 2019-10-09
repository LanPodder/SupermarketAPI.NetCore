using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API.Persistence.Repositories
{
    public class CategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context){
        }

        public async Task<IEnumerable<Category>> ListAsync(){
            return await context.Categories.ToListAsync();
        }
    }
}