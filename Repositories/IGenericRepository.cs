using System.Linq.Expressions;

namespace emlak_son.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task InsertAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}
