using System.Linq.Expressions;

namespace WarehouseManagement.Infra;

public interface IRepositoryBase<T>
{
    T Add(T entity);
    T Update(T entity);
    T Get(Guid id);
    IEnumerable<T> All();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    void SaveChanges();
}
