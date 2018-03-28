using System.Collections.Generic;

namespace CollegeGrades.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(string id);

        TEntity GetSingleBySpec(ISpecification<TEntity> spec);

        IEnumerable<TEntity> ListAll();

        IEnumerable<TEntity> List(ISpecification<TEntity> spec);

        TEntity Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}