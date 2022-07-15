using DigiStore.Data;
using DigiStore.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DigiStore.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        DigiStoreContext _dbContext;
        public GenericRepository(DigiStoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            //_dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            //await CommitAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var item = await _dbContext.Set<T>().FindAsync(id);
            return item;
        }
        public async Task<List<T>> GetAllAsync(int count)
        {
            var result = _dbContext.Set<T>();
            if (count > 0)
            {
                result.Take(count);
            }
            return await result.ToListAsync();
        }
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        //public Task<PaginationResult<T>> GetAsQueryable(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int? page = null, int? limit = null)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
