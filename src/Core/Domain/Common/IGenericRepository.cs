using System.Linq.Expressions;

namespace Domain.Common;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IList<TEntity>> GetAll();
    Task<IList<TEntity>> FindByCondition(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> GetById(int id);
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(int id);
}
