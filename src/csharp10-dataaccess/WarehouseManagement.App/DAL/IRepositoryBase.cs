namespace WarehouseManagement.EF.App.DAL;

internal interface IRepositoryBase<TEntity> where TEntity : class
{
    IEnumerable<TEntity> All { get; }
    TEntity Find(int key);
    void Add(params TEntity[] obj);
    void Update(params TEntity[] obj);
    void Delete(params TEntity[] obj);
}
