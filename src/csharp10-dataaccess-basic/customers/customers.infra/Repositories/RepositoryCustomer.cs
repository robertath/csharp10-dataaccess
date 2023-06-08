using customers.infra.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customers.infra.Repositories;

public class RepositoryCustomer : RepositoryBase<Customer>
{
    public RepositoryCustomer(SqlServerContext context) : base(context)
    {
    }

    public async override Task<Customer> Update(Customer entity)
    {
        Customer toUpdate = await Get(entity.Id);
        toUpdate.PersonId = entity.PersonId;
        toUpdate.Person = entity.Person;        
        toUpdate.Subscription = entity.Subscription;        

        return await base.Update(toUpdate);
    }

    public async override Task<IEnumerable<Customer>> All()
    {
        var list = await context.Set<Customer>().ToListAsync();

        return list
            .OrderBy(i => i.Person.FirstName)
            .ThenBy(i => i.Person.LastName);
    }
}
