using WarehouseManagement.Infra.Data;

namespace WarehouseManagement.Infra;

public class RepositoryItem : RepositoryBase<Item>
{
    public RepositoryItem(SqlServerContext context)
        : base(context)
    {
    }

    public override Item Update(Item entity)
    {
        Item toUpdate = Get(entity.Id);
        toUpdate.Price = entity.Price;
        toUpdate.Name = entity.Name;

        return base.Update(toUpdate);
    }
}

