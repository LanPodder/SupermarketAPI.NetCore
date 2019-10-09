using System.Threading.Tasks;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Persistance.Contexts;

namespace Supermarket.API.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context){
            this.context = context;
        }

        public async Task CompleteAsync(){
            await context.SaveChangesAsync();
        }
    }
}