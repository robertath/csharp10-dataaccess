using WarehouseManagement.Infra.Data;

namespace WarehouseManagement.Infra;

public class RepositoryCustomer
    : RepositoryBase<Customer>
{
    public RepositoryCustomer(SqlServerContext context)
        : base(context)
    {
    }

    public override Customer Update(Customer entity)
    {
        Customer toUpdate = Get(entity.Id);
        toUpdate.Name = entity.Name;
        toUpdate.Address = entity.Address;
        toUpdate.PostalCode = entity.PostalCode;
        toUpdate.Country = entity.Country;
        toUpdate.PhoneNumber = entity.PhoneNumber;

        return base.Update(toUpdate);
    }
    public override IEnumerable<Customer> All()
    {
        var all = context.Set<Customer>().ToList().OrderBy(i=> i.Country).ThenBy(i=> i.Name);

        return all;
    }
}