using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeGrades.Core.Interfaces
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<List<T>> ListAllAsync();

        //Task<List<T>> ListAsync(ISpecification<T> spec);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}