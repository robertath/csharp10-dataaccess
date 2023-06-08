using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WarehouseManagement.Infra.Data;

namespace WarehouseManagement.Infra;

public class RepositoryOrder
    : RepositoryBase<Order>
{
    public RepositoryOrder(SqlServerContext context)
        : base(context)
    {
    }

    public override IEnumerable<Order>
        Find(Expression<Func<Order, bool>> predicate)
    {
        return context.Order
            .Include(order => order.LineItems)
            .ThenInclude(lineItem => lineItem.Item)
            .Where(predicate)
            .ToList();
    }

    public override Order Update(Order entity)
    {
        Order toUpdate = context.Order
            .Include(order => order.LineItems)
            .ThenInclude(lineItem => lineItem.Item)
            .Single(order => order.Id == entity.Id);

        toUpdate.CreatedAt = entity.CreatedAt;
        toUpdate.LineItems = entity.LineItems;

        return base.Update(toUpdate);
    }
}