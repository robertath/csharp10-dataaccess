using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace customers.infra.Repositories;

public interface IRepositoryBase<T>
{
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(T entity);
    Task<IEnumerable<T>> All();
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    Task<T> Get(Guid id);
}

public abstract class RepositoryBase<T> : IRepositoryBase<T>
    where T : class
{
    protected readonly SqlServerContext context;

    protected RepositoryBase(SqlServerContext context)
    {
        this.context = context;
    }

    public async virtual Task<T> Add(T entity)
    {
        var addedEntity = context.Set<T>().Add(entity).Entity;
        return addedEntity;
    }

    public virtual async Task<IEnumerable<T>> All() => context.Set<T>().ToList();

    public async virtual Task<T> Delete(T entity)
    {
        var deletedEntity = context.Set<T>().Remove(entity).Entity;
        return deletedEntity;
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>()
            .Where(predicate)
            .ToList();
    }

    public async Task<T> Get(Guid id)
    {
        return await context.FindAsync<T>(id);
    }

    public async virtual Task<T> Update(T entity)
    {
        var deletedEntity = context.Set<T>().Update(entity).Entity;
        return deletedEntity;
    }
}
