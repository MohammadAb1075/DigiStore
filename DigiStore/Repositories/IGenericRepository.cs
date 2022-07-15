using DigiStore.Utilities;
using System.Linq.Expressions;

namespace DigiStore.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync(int count);
        Task CommitAsync();

        //Task<PaginationResult<T>> GetAsQueryable(Expression<Func<T, bool>> filter = null,
        //                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //                                         string includeProperties = "",
        //                                         int? page = null,
        //                                         int? limit = null);

    }
}
