using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeGrades.Core.Interfaces
{
    public interface IRepositoryAsync<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(string id);

        Task<List<TEntity>> ListAllAsync();

        Task<List<TEntity>> ListAsync(ISpecification<TEntity> spec);

        Task<TEntity> AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}