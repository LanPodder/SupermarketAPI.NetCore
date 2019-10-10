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
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context){}

        public async Task<IEnumerable<Product>> ListAsync(){
            return await context.Products.Include(p=>p.Category).ToListAsync();
        }

        public async Task AddAsync(Product product){
            await context.Products.AddAsync(product);
        }
    }
}