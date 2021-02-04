using DL.Services.AMS.Data.Context;
using DL.Services.AMS.Data.Models;
using System.Threading.Tasks;

namespace DL.Services.AMS.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : ModelBase
    {
        protected readonly AMSDbContext Context;

        public Repository(AMSDbContext context)
        {
            Context = context;
        }

        public async Task<T> Get(int id)
        {
            T model = await Context.Set<T>()
                .FindAsync(id);

            return model;
        }

        public async Task<T> Add(T model)
        {
            await Context.Set<T>()
                .AddAsync(model);
            await Context.SaveChangesAsync();

            return model;
        }

        public async Task Remove(T model)
        {
            Context.Set<T>().Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }
    }
}
