using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        public Task<TEntity?> GetByIdAsync(TId id);
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task AddAsync(TEntity entity);
        public Task UpdateAsync(TEntity entity);
        public Task DeleteAsync(TEntity entity);
        public Task<int> SaveChangesAsync();
    }
}
