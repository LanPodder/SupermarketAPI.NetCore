using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Persistance.Contexts;
using Supermarket.API.Persistance.Repositories;

namespace Supermarket.API.Persistance.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context){
        }

        public async Task<IEnumerable<Category>> ListAsync(){
            return await context.Categories.ToListAsync();
        }

        public async Task AddAsync(Category category){
            await context.Categories.AddAsync(category);
        }

        public async Task<Category> FindByIdAsync(int id){
            return await context.Categories.FindAsync(id);
        }

        public void Update(Category category){
            context.Categories.Update(category);
        }

        public void Remove(Category category){
            context.Categories.Remove(category);
        }
    }
}