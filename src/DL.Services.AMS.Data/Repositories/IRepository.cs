using System.Threading.Tasks;

namespace DL.Services.AMS.Data.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Get(int id);
        Task<T> Add(T model);
        Task SaveChanges();
        Task Remove(T model);
    }
}
