using customers.infra.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customers.infra.Repositories;

public class RepositoryUser : RepositoryBase<User>
{
    public RepositoryUser(SqlServerContext context) : base(context)
    {
    }

    public async override Task<IEnumerable<User>> All()
    {
        var list = await context.Set<User>().ToListAsync();
        return list.OrderBy(i=> i.Person.FirstName).ThenBy(i=> i.Person.LastName);
    }
}
