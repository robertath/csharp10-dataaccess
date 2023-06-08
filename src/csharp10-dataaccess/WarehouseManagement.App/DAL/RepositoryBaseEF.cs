namespace WarehouseManagement.EF.App.DAL;

internal class RepositoryBaseEF<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    private readonly SqlServerContext _context;

    public RepositoryBaseEF(SqlServerContext context)
    {
        _context = context;
    }

    public IEnumerable<TEntity> All => _context.Set<TEntity>().ToList();

    public void Add(params TEntity[] obj)
    {
        _context.Set<TEntity>().UpdateRange(obj);
        _context.SaveChanges();
    }

    public void Delete(params TEntity[] obj)
    {
        _context.Set<TEntity>().RemoveRange(obj);
        _context.SaveChanges();
    }

    public TEntity Find(int key) => _context.Find<TEntity>(key)!;

    public void Update(params TEntity[] obj)
    {
        _context.Set<TEntity>().AddRange(obj);
        _context.SaveChanges();
    }
}
